﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinetech.TianJin.AutomationService.SlotInterface
{
    public interface IAutomationSlot
    {
        SchedulePlan SchedulePlan { get; }
        ActivationDescription ActivationDescription { get; }

        bool SlotIsValidated();
    }
}
