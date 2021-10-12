using System;
using System.ComponentModel.DataAnnotations;

namespace Sapsan.Modules.Operational.Presenters.Data
{
	public class OperationalWorkFullInfoPresenterData : OperationalWorkPresenterData
	{
		[Display(Name = "Id", Order = -1)]
		public Guid Id { get; set; }

		[Display(Name = "Название", Order = 2)]
		public string Name { get; set; }

		[Display(Name = "OperationalId", Order = -1)]
		public Guid OperationalId { get; set; }

		[Display(Name = "Оперативка", Order = 1)]
		public string OperationalName { get; set; }

		[Display(Name = "Порядковый\nномер", Order = 3)]
		public int OrderNum { get; set; }

		[Display(Name = "Дата\nсоздания", Order = 4)]
		public DateTime DtCreate { get; set; }

		[Display(Name = "Дата\nудаления", Order = 5)]
		public DateTime? DtDelete { get; set; }

		[Display(Name = "FromOperationalWorkId", Order = -1)]
		public Guid? FromOperationalWorkId { get; set; }
	}
}
