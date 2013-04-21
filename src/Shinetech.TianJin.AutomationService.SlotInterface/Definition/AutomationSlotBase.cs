using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinetech.TianJin.AutomationService.SlotInterface
{
    public abstract class AutomationSlotBase : IAutomationSlot
    {
        public SchedulePlan SchedulePlan
        {
            get { throw new NotImplementedException(); }
        }

        public ActivationDescription ActivationDescription
        {
            get { throw new NotImplementedException(); }
        }

        public bool SlotIsValidated()
        {
            throw new NotImplementedException();
        }
    }
}
