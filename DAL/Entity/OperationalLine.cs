using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sapsan.Modules.Operational.DAL.Entity
{
	[Table("modules.operational_line")]
	public class OperationalLine
	{
		[Key] public Guid Id { get; set; }
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