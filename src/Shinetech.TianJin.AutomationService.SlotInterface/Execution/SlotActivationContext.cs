using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinetech.TianJin.AutomationService.SlotInterface
{
    public class SlotActivationContext
    {
		public DateTime ActivationTime { get; private set; }
		public HostInfomation HostInfomation { get; private set; }
		public RunningStatus RunningStatus { get; private set; }
    }
}
