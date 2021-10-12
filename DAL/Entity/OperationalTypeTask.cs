using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sapsan.Modules.Operational.DAL.Entity
{
	[Table("modules.operational_type_task")]
	public class OperationalTypeTask
	{
		[Key] public Guid Id { get; set; }
		public string Name { get; set; }
		public int OrderNum { get; set; }
		public DateTime DtCreate { get; set; }
		public DateTime? DtDelete { get; set; }
	}
}