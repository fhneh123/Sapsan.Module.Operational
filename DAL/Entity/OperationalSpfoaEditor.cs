using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sapsan.Modules.Operational.DAL.Entity
{
	[Table("modules.operational_spfoa_editor")]
	public class OperationalSpfoaEditor
	{
		[Key] public Guid Id { get; set; }
		public Guid OperatioalWorkId { get; set; }
		public Guid? OperationalStatusId { get; set; }
		public int OperationalDateTypeId { get; set; }
		public Guid SpfoaStatusId { get; set; }
		public int OrderNum { get; set; }
		public DateTime DtCreate { get; set; }
		public DateTime? DtDelete { get; set; }
	}
}