using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Quartz;

namespace Shinetech.TianJin.AutoDialVpn.Core
{
    public class SendIpByEmail : JobBase, IJob
    {
        private static int AddressEmailHashCode { get; set; }

        public void Execute(IJobExecutionContext context) {
            ExecuteWapper(context);
        }

        protected override void ExecuteCore(string addresses) {
            if(String.IsNullOrEmpty(addresses)) {
                return;
            }
            if (addresses.GetHashCode() != AddressEmailHashCode) {
                AddressEmailHashCode = addresses.GetHashCode();
                try {
                    Console.WriteLine("Address:" + addresses);

                    var client = InitializeSmtpClient();
                    var message = PrepareEmailMessage(addresses.Split('|'));
                    client.Send(message);
                } catch (Exception e) {
                    ExecuteExcetion(e);
                }
            }
        }

        private MailMessage PrepareEmailMessage(IEnumerable<string> addresses) {
            var settings = Properties.Settings.Default;
            var mail = new MailMessage();
            mail.From = new MailAddress(settings.EmailFrom);
            Properties.Settings.Default.EmailTargets.Split('&')
                .Select(add => new MailAddress(add))
                .ToList()
                .ForEach(mail.To.Add);
            mail.Subject = "New VPN IP address.";
            var sb = new StringBuilder();
            addresses.ToList().ForEach(add => sb.Append(add + "\n"));
            mail.Body = "New VPN IP address:" + sb.ToString().Trim(',');

            return mail;
        }

        private SmtpClient InitializeSmtpClient() {
            var smtpClient = new SmtpClient {
                Host = Properties.Settings.Default.EmailHost,
                Credentials = new NetworkCredential(
                    Properties.Settings.Default.EmailUsername,
                    Properties.Settings.Default.EmailPassword)
            };

            return smtpClient;
        }
    }
}