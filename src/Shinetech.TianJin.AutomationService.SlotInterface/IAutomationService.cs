using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shinetech.TianJin.AutomationService.SlotInterface
{
    public interface IAutomationService
    {
        void Initialize();
        void Terminate();
		
		ServiceStatus ServiceStatus{ get; }
    }
}
