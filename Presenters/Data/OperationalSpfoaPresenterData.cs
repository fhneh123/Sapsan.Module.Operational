using System;

namespace Sapsan.Modules.Operational.Presenters.Data
{
	public class OperationalSpfoaPresenterData
	{
		public Guid Id { get; set; }
		public Guid ContractId { get; set; }
		public string ContractShifr { get; set; }
		public string Gip { get; set; }
		public string ContractName { get; set; }
		public string CustomerName { get; set; }
		public Guid PlanId { get; set; }
		public string PlanName { get; set; }
		public bool PlanIsMain { get; set; }
		public int? StageNumber { get; set; }
		public string TypeTask { get; set; }
		public Guid TypeTaskId { get; set; }
		public Guid SubdivisionId { get; set; }
		public string SubdivisionShortName { get; set; }
		public int? ExecutorTypeId { get; set; }
		public string ExecutorType { get; set; }
		public Guid? ExecuteStatusId { get; set; }
		public DateTime? DtReleaseIgdi { get; set; }
		public Guid? StatusIgdiId { get; set; }
		public string StatusIgdi { get; set; }
		public DateTime? DtReleaseIgi { get; set; }
		public Guid? StatusIgiId { get; set; }
		public string StatusIgi { get; set; }
		public DateTime? DtReleaseIgmi { get; set; }
		public Guid? StatusIgmiId { get; set; }
		public string StatusIgmi { get; set; }
		public DateTime? DtReleaseIei { get; set; }
		public Guid? StatusIeiId { get; set; }
		public string StatusIei { get; set; }
		public DateTime? DtSummariKoii { get; set; }
		public string StatusKoii { get; set; }
		public Guid? StatusKoiiId { get; set; }
		public string RemarkKoii { get; set; }
		public DateTime? DtActirovania { get; set; }
		public double? ExecutePriceIgdiFixed { get; set; }
		public double? ExecutePriceIgdiMain { get; set; }
		public double? ExecutePriceIgdi3DScan { get; set; }
		public double? ExecutePriceIgi { get; set; }
		public double? ExecutePriceIgiGeoradScan { get; set; }
		public double? ExecutePriceIgmi { get; set; }
		public double? ExecutePriceIei { get; set; }
		public double? ExecutePriceResult { get; set; }
		public double? PlanPriceIgdiFixed { get; set; }
		public double? PlanPriceIgdiMain { get; set; }
		public double? PlanPriceIgdi3DScan { get; set; }
		public double? PlanPriceIgi { get; set; }
		public double? PlanPriceIgiGeoradScan { get; set; }
		public double? PlanPriceIgmi { get; set; }
		public double? PlanPriceIei { get; set; }
		public double? PlanPriceResult { get; set; }
		public double? Economy { get; set; }
		public DateTime DtCreate { get; set; }
		public DateTime? DtDelete { get; set; }
	}
}