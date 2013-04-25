using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinetech.TianJin.AutomationService.SlotInterface
{
    public class SlotActivationContext
    {
		DateTime ActivationTime { get; }
		HostInfomation HostInfomation { get; }
		RunningStatus RunningStatus { get; }
    }
}
