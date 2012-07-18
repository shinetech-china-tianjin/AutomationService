using System;
using System.Collections.Generic;
using System.Net;
using Quartz;
using Quartz.Impl;
using AMicroblogAPI;
using AMicroblogAPI.Common;

namespace AutoDial
{
    class Program
    {
        private static OAuthAccessToken _token = WeiboLogin();

        static void Main(string[] args) {
            var sf = new StdSchedulerFactory();
            var scheduler = sf.GetScheduler();

            if (Properties.Settings.Default.EnableEmail) {
                BuildEmailJob(scheduler);
            }
            if (Properties.Settings.Default.EnableWeibo) {
                BuildWeiboJob(scheduler);
            }

            scheduler.Start();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            scheduler.Shutdown();
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
                    Util.Logger.Fatal("Login failure.", e);
                    continue;
                }
                break;
            }
            return user;
        }
    }
}
