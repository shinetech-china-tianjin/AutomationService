using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using AMicroblogAPI;
using AMicroblogAPI.DataContract;
using Quartz;

namespace AutoDial
{
    public class SendIpByWeibo : JobBase, IJob
    {
        private static int AddressWeiboHashCode { get; set; }

        public void Execute(IJobExecutionContext context) {
            ExecuteWapper(context);
        }

        protected override void ExecuteExcetion(Exception e) {
            Util.Logger.Debug("Post status failure.", e);
        }

        protected override void ExecuteCore(string addresses) {
            if (String.IsNullOrEmpty(addresses)) {
                return;
            }
            if (addresses.GetHashCode() != AddressWeiboHashCode) {
                AddressWeiboHashCode = addresses.GetHashCode();
                try {
                    Console.WriteLine("Address:" + addresses);
                    var status = PrepareStatus(addresses.Split('|'));
                    AMicroblog.PostStatus(status);
                } catch (Exception e) {
                    ExecuteExcetion(e);
                }
            }
        }

        private UpdateStatusInfo PrepareStatus(IEnumerable<string> addresses) {
            var sb = new StringBuilder();
            sb.Append(String.Format("{0}:", DateTime.Now));
            addresses.ToList().ForEach(add => sb.AppendFormat(",{0}", add));
            return new UpdateStatusInfo {
                Status = "New VPN IP address:" + sb.ToString().Trim(',')
            };
        }
    }
}