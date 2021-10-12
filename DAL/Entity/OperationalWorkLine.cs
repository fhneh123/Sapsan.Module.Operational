using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sapsan.Modules.Operational.DAL.Entity
{
	[Table("modules.operational_work_line")]
	public class OperationalWorkLine
	{
		[Key] public Guid Id { get; set; }
		public Guid OperationalWorkId { get; set; }
		public Guid OperationalLineId { get; set; }
		public Guid? ComplianceWorkId { get; set; }
		public Guid? ComplianceWorkAllWorkId { get; set; }
		public DateTime? DtPlanStart { get; set; }
		public DateTime? DtPlanEnd { get; set; }
		public DateTime? DtForecastStart { get; set; }
		public DateTime? DtForecastEnd { get; set; }
		public DateTime? DtFactStart { get; set; }
		public DateTime? DtFactEnd { get; set; }
		public Guid? ResponsibleUserId { get; set; }
		public string Comment { get; set; }
		public string Status { get; set; }
		public double? PlanSmetaMain { get; set; }
		public double? PlanSmetaFixed { get; set; }
		public double? PlanSmeta3DScan { get; set; }
		public double? PlanSmetaGeoradScan { get; set; }
		public Guid? PlanSmetaResponsibleUserId { get; set; }
		public DateTime? PlanSmetaDtFactStart { get; set; }
		public DateTime? PlanSmetaDtFactEnd { get; set; }
		public double? ExecuteSmetaMain { get; set; }
		public double? ExecuteSmetaFixed { get; set; }
		public double? ExecuteSmeta3DScan { get; set; }
		public double? ExecuteSmetaGeoradScan { get; set; }
		public Guid? ExecuteSmetaResponsibleUserId { get; set; }
		public DateTime? ExecuteSmetaDtFactStart { get; set; }
		public DateTime? ExecuteSmetaDtFactEnd { get; set; }
		public string ExecuteSmetaAdditional { get; set; }
		public Guid? MainSpecialistId { get; set; }
		public DateTime? DtActPassingStripSurvey { get; set; }
		public DateTime? DtActPassingRapper { get; set; }
		public DateTime? DtTopoplan { get; set; }
		public DateTime? DtLoadInParallelProjection { get; set; }
		public DateTime? DtProfilAndStatement { get; set; }
		public DateTime? DtTransferProfileIgiAndIgmi { get; set; }
		public DateTime? DtUnloadingReportFromSapsan { get; set; }
		public DateTime? DtNormocontrol { get; set; }
		public string Reason { get; set; }
		public bool HasRemark { get; set; }
		public DateTime? DtKameralIgdiEnd { get; set; }
		public DateTime? DtForecastEndIgdi { get; set; }
		public string VodotokCount { get; set; }
		public string CalculationsCount { get; set; }
		public DateTime? DtSigning { get; set; }
		public string PvoCount { get; set; }
		public string SurvayField { get; set; }
		public string SurvayTrack { get; set; }
		public DateTime? TZ { get; set; }
		public DateTime? DtEnd { get; set; }
		public string Tfo { get; set; }
		public Guid? ResponsibleUser2 { get; set; }
		public DateTime? DtPlanCompilation { get; set; }
		public DateTime? DtSendCompilation { get; set; }
		public DateTime? DtSend { get; set; }
		public string Fon { get; set; }
		public string PHH { get; set; }
		public DateTime? DtIssuePrescriptionGO { get; set; }
		public string PGR { get; set; }
		public string IGS { get; set; }
		public string PchvOa { get; set; }
		public string PchvBak { get; set; }
		public string PchvPrz { get; set; }
		public string AhSH { get; set; }
		public string AhPr { get; set; }
		public string PchvZso { get; set; }
		public string PovVod { get; set; }
		public string PodzVodIpvs { get; set; }
		public string PodzVodIgs { get; set; }
		public string Don { get; set; }
		public string Av { get; set; }
		public string GraS { get; set; }
		public string Rad { get; set; }
		public string Ern { get; set; }
		public string Ff { get; set; }
		public string ProtocolRadiation { get; set; }
		public DateTime? DtForecastEndKameralIgdi { get; set; }
		public DateTime? DtForecastKameralIgi { get; set; }
		public DateTime? DtForecastCameralIgmi { get; set; }
		public string HasProtocolFieldSurvay { get; set; }
		public string HasGraficPart { get; set; }
		public string Vetstancia { get; set; }
		public string Districts { get; set; }
		public double? PlanSmetaCountSkvazhin { get; set; }
		public double? PlanSmetaPm { get; set; }
		public double? PlanSmetaCountMonolit { get; set; }
		public double? ExecuteSmetaCountSkvazhin { get; set; }
		public double? ExecuteSmetaPm { get; set; }
		public double? ExecuteSmetaCountMonolit { get; set; }
	}
}