using System;

namespace Sapsan.Modules.Operational.Services.Data
{
	public class OperationalData
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int OrderNum { get; set; }
		public Guid ComplianceGroupId { get; set; }
		public DateTime DtCreate { get; set; }
		public DateTime? DtDelete { get; set; }
	}
}