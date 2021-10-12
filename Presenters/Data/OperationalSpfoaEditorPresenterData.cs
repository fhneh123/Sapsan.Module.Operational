using System;
using System.ComponentModel.DataAnnotations;

namespace Sapsan.Modules.Operational.Presenters.Data
{
	public class OperationalSpfoaEditorPresenterData
	{
		[Display(Name = "Id", Order = -1)] public Guid Id { get; set; }

		[Display(Name = "OperatioalId", Order = -1)]
		public Guid OperatioalId { get; set; }

		[Display(Name = "Оперативка", Order = 1)]
		public string OperatioalName { get; set; }

		[Display(Name = "OperatioalWorkId", Order = -1)]
		public Guid OperatioalWorkId { get; set; }

		[Display(Name = "Работа", Order = 2)] public string OperatioalWorkName { get; set; }

		[Display(Name = "OperationalStatusId", Order = -1)]
		public Guid? OperationalStatusId { get; set; }

		[Display(Name = "Статус в оперативке", Order = 3)]
		public string OperationalStatusName { get; set; }

		[Display(Name = "OperationalDateTypeId", Order = -1)]
		public int OperationalDateTypeId { get; set; }

		[Display(Name = "Тип даты", Order = 4)]
		public string OperationalDateType { get; set; }

		[Display(Name = "SpfoaStatusId", Order = -1)]
		public Guid SpfoaStatusId { get; set; }

		[Display(Name = "Статус в СПФОА", Order = 5)]
		public string SpfoaStatus { get; set; }

		[Display(Name = "Порядковый номер", Order = 6)]
		public int OrderNum { get; set; }

		[Display(Name = "DtCreate", Order = -1)]
		public DateTime DtCreate { get; set; }

		[Display(Name = "DtDelete", Order = -1)]
		public DateTime? DtDelete { get; set; }
	}
}