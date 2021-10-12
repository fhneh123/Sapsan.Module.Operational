using System;

namespace Sapsan.Modules.Operational.Services.Data
{
	public class OperationalSpfoaData
	{
		public Guid Id { get; set; }
		public Guid ContractId { get; set; }
		public Guid PlanId { get; set; }
		public int? StageNumber { get; set; }
		public Guid TypeTaskId { get; set; }
		public Guid SubdivisionId { get; set; }
		public Guid? ExecuteStatusId { get; set; }
		public string RemarkKoii { get; set; }
		public DateTime? DtActirovania { get; set; }
		public DateTime DtCreate { get; set; }
		public DateTime? DtDelete { get; set; }
	}
}