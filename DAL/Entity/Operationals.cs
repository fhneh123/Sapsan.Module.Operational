using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sapsan.Modules.Operational.DAL.Entity
{
	[Table("modules.operational")]
	public class Operationals
	{
		[Key] public Guid Id { get; set; }
		public string Name { get; set; }
		public int OrderNum { get; set; }
		public Guid ComplianceGroupId { get; set; }
		public DateTime DtCreate { get; set; }
		public DateTime? DtDelete { get; set; }
	}
}