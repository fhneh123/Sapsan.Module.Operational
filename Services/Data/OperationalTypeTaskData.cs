using System;
using System.ComponentModel.DataAnnotations;

namespace Sapsan.Modules.Operational.Services.Data
{
	public class OperationalTypeTaskData
	{
		[Display(Name = "Id", Order = -1)] public Guid Id { get; set; }

		[Display(Name = "Название", Order = 0)]
		public string Name { get; set; }

		[Display(Name = "Порядковый номер", Order = 1)]
		public int OrderNum { get; set; }

		[Display(Name = "Дата создания", Order = 2)]
		public DateTime DtCreate { get; set; }

		[Display(Name = "Дата удаления", Order = 3)]
		public DateTime? DtDelete { get; set; }
	}
}