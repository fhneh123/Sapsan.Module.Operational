using System;

namespace Sapsan.Modules.Operational.Services.Data
{
	public class OperationalSpfoaEditorData
	{
		public Guid Id { get; set; }
		public Guid OperatioalWorkId { get; set; }
		public Guid? OperationalStatusId { get; set; }
		public int OperationalDateTypeId { get; set; }
		public Guid SpfoaStatusId { get; set; }
		public int OrderNum { get; set; }
		public DateTime DtCreate { get; set; }
		public DateTime? DtDelete { get; set; }
	}
}