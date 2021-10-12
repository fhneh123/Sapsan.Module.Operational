using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sapsan.Modules.Operational.DAL.Entity
{
	[Table("modules.operational_entity_status")]
	public class OperationalEntityStatus
	{
		[Key] public Guid Id { get; set; }
		public Guid? OperationalWorkId { get; set; }
		public string Name { get; set; }
		public string EntityId { get; set; }
		public string Color { get; set; }
		public int OrderNum { get; set; }
		public DateTime DtCreate { get; set; }
		public DateTime? DtDelete { get; set; }
	}
}