using System;

namespace Sapsan.Modules.Operational.Presenters.Data
{
	public class OperationalLinePresenterData
	{
		public Guid Id { get; set; }

		public Guid OperationalId { get; set; }

		public Guid ContractId { get; set; }

		public Guid PlanId { get; set; }

		public string PlanName { get; set; }

		public bool PlanIsMain { get; set; }

		public string Priority { get; set; }

		public Guid? CustomerId { get; set; }

		public string CustomerName { get; set; }

		public string Shifr { get; set; }

		public string Name { get; set; }

		public int? StageNumber { get; set; }

		public string Gip { get; set; }

		public Guid ComplianceGroupId { get; set; }

		public string ComplianceGroupName { get; set; }

		public DateTime? DtPlanStart { get; set; }

		public DateTime? DtPlanEnd { get; set; }

		public DateTime? DtForecastStart { get; set; }

		public DateTime? DtForecastEnd { get; set; }

		public DateTime? DtFactStart { get; set; }

		public DateTime? DtFactEnd { get; set; }

		public string TaskType { get; set; }

		public Guid TaskTypeId { get; set; }

		public string SubdivisionShortName { get; set; }
		public Guid SubdivisionId { get; set; }

		public DateTime DtCreate { get; set; }

		public DateTime? DtDelete { get; set; }
	}
}