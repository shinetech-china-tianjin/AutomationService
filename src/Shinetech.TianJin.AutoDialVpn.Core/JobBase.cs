using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Quartz;
using System.Text;

namespace Shinetech.TianJin.AutoDialVpn.Core
{
    public abstract class JobBase
    {
        public void ExecuteWapper(IJobExecutionContext context) {
            Console.WriteLine(context.JobDetail.Key.Name + ": " + DateTime.Now);
            var addressString = GetAddressesString();
            ExecuteCore(addressString);
        }

        protected abstract void ExecuteCore(string addresses);

        protected virtual void ExecuteExcetion(Exception e) {
            LogUtil.WriteError(e.Message);
        }

        private string GetAddressesString() {
            var addresses = GetLocalInterfaceAddresses()
                .Where(add => !add.ToString().StartsWith("192"))
                .Select(add => add.ToString())
                .OrderBy(s => s);
            var sb = new StringBuilder();
            addresses.ToList().ForEach(add => sb.Append(add + "|"));
            return sb.ToString();
        }

        public IEnumerable<IPAddress> GetLocalInterfaceAddresses() {
            var hostName = Dns.GetHostName();
            var addresses = Dns.GetHostAddresses(hostName);
            var ipv4Address = addresses
                .Where(ip => !ip.IsIPv6LinkLocal)
                .OrderBy(add => add.GetHashCode());
            return ipv4Address;
        }
    }
}