using System;
using System.ComponentModel.DataAnnotations;

namespace Sapsan.Modules.Operational.Services.Data
{
	public class OperationalEntityStatusData
	{
		[Display(Name = "Id", Order = -1)] public Guid Id { get; set; }

		[Display(Name = "OperationalWorkId", Order = -1)]
		public Guid? OperationalWorkId { get; set; }

		[Display(Name = "Название", Order = 0)]
		public string Name { get; set; }

		[Display(Name = "ColumnName", Order = -1)]
		public string EntityId { get; set; }

		[Display(Name = "Цвет", Order = 1)] public string Color { get; set; }

		[Display(Name = "Порядковый номер", Order = 2)]
		public int OrderNum { get; set; }

		[Display(Name = "Дата создания", Order = 2)]
		public DateTime DtCreate { get; set; }

		[Display(Name = "Дата удаления", Order = 3)]
		public DateTime? DtDelete { get; set; }
	}
}