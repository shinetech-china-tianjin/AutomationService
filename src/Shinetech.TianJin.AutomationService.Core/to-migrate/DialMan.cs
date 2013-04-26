using System;
using System.Collections.Generic;
using System.Net;
using Quartz;
using Quartz.Impl;
//using AMicroblogAPI;
//using AMicroblogAPI.Common;
using System.Diagnostics;

namespace Shinetech.TianJin.AutomationService.Core
{
    public class DialMan
    {
        private static IScheduler _scheduler;
        private static OAuthAccessToken _token = WeiboLogin();

        public static void Start() {
            Init();

            if (Properties.Settings.Default.EnableEmail) {
                BuildEmailJob(_scheduler);
            }
            if (Properties.Settings.Default.EnableWeibo) {
                BuildWeiboJob(_scheduler);
            }

            _scheduler.Start();
            LogUtil.WriteInfo("Start at:" + DateTime.Now.ToString());
        }

        public static void Stop() {
            if (_scheduler != null) {
                _scheduler.Shutdown();
                LogUtil.WriteInfo("End at:" + DateTime.Now.ToString());
            }
        }

        private static void Init() {
            var sf = new StdSchedulerFactory();
            _scheduler = sf.GetScheduler();
        }

        private static void BuildEmailJob(IScheduler scheduler) {
            var jobDetail = JobBuilder.Create<SendIpByEmail>()
                .WithIdentity("Send_IP_Email")
                .Build();
            var trigger = TriggerBuilder.Create()
                .WithSimpleSchedule(ssb =>
                    ssb.WithIntervalInMinutes(Properties.Settings.Default.EmailIntervalByMinute)
                                               .RepeatForever())
                .Build();

            scheduler.ScheduleJob(jobDetail, trigger);
        }

        private static void BuildWeiboJob(IScheduler scheduler) {
            var jobDetail = JobBuilder.Create<SendIpByWeibo>()
                .WithIdentity("Send_IP_Weibo")
                .Build();
            var trigger = TriggerBuilder.Create()
                .WithSimpleSchedule(ssb =>
                    ssb.WithIntervalInMinutes(Properties.Settings.Default.WeiboIntervalByMinute)
                                               .RepeatForever())
                .Build();

            scheduler.ScheduleJob(jobDetail, trigger);
        }

        private static OAuthAccessToken WeiboLogin() {
            OAuthAccessToken user = null;
            for (int i = 0; i < 10; i++) {
                try {
                    user = AMicroblog.Login(Properties.Settings.Default.WeiboUsername, Properties.Settings.Default.WeiboPassword);
                } catch (Exception e) {
                    LogUtil.WriteError(e.Message);
                    continue;
                }
                break;
            }
            return user;
        }
    }
}
