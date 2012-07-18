using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Shinetech.TianJin.AutoDialVpn.Core;
using System.Threading;

namespace Shinetech.TianJin.AutoDialVpn
{
    partial class AutoDialVpnService : ServiceBase
    {
        public AutoDialVpnService() {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            DialMan.Start();
        }

        protected override void OnStop() {
            DialMan.Stop();
        }
    }
}
