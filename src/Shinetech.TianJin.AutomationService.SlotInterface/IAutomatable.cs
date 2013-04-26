using Shinetech.TianJin.AutomationService.SlotInterface;

namespace Shinetech.TianJin.AutomationService.SlotInterface
{
	public interface IAutomatable
	{	
		SlotActivationContext ActivationContext { get; }
	}
}