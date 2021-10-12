using System;

namespace Sapsan.Modules.Operational.Services.Data
{
	public class OperationalLineData
	{
		public Guid Id { get; set; }
		public Guid OperationalId { get; set; }
		public Guid ContractId { get; set; }
		public Guid PlanId { get; set; }
		public string Priority { get; set; }
		public Guid ComplianceGroupId { get; set; }
		public Guid TypeTaskId { get; set; }
		public Guid SubdivisionId { get; set; }
		public DateTime DtCreate { get; set; }
		public DateTime? DtDelete { get; set; }
	}
}