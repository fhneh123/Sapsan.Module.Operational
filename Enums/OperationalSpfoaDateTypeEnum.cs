using Sapsan2.Contracts.Attributes;

namespace Sapsan.Modules.Operational.Enums
{
	public enum OperationalSpfoaDateTypeEnum
	{
		[DisplayName("Прогнозная дата начала")]
		ПрогнозНачала,

		[DisplayName("Прогнозная дата окончания")]
		ПрогнозОкнчания,

		[DisplayName("Фактическая дата начала")]
		ФактНачала,

		[DisplayName("Фактическая дата окончания")]
		ФактОкончания
	}
}