using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sapsan.Modules.Operational.DAL.Entity
{
	[Table("modules.operational_work")]
	public class OperationalWork
	{
		[Key] public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid OperationalId { get; set; }
		public int OrderNum { get; set; }
		public DateTime DtCreate { get; set; }
		public DateTime? DtDelete { get; set; }
		public bool СomplianceWorkVisible { get; set; }
		public bool ComplianceWorkAllWorkVisible { get; set; }
		public bool DtPlanStartVisible { get; set; }
		public bool DtPlanEndVisible { get; set; }
		public bool DtForecastStartVisible { get; set; }
		public bool DtForecastEndVisible { get; set; }
		public bool DtFactStartVisible { get; set; }
		public bool DtFactEndVisible { get; set; }
		public bool ResponsibleUserVisible { get; set; }
		public bool CommentVisible { get; set; }
		public bool StatusVisible { get; set; }
		public bool PlanSmetaMainVisible { get; set; }
		public bool PlanSmetaFixedVisible { get; set; }
		public bool PlanSmeta3DScanVisible { get; set; }
		public bool PlanSmetaGeoradScanVisible { get; set; }
		public bool PlanSmetaResponsibleUserVisible { get; set; }
		public bool PlanSmetaDtFactStartVisible { get; set; }
		public bool PlanSmetaDtFactEndVisible { get; set; }
		public bool ExecuteSmetaMainVisible { get; set; }
		public bool ExecuteSmetaFixedVisible { get; set; }
		public bool ExecuteSmeta3DScanVisible { get; set; }
		public bool ExecuteSmetaGeoradScanVisible { get; set; }
		public bool ExecuteSmetaResponsibleUserVisible { get; set; }
		public bool ExecuteSmetaDtFactStartVisible { get; set; }
		public bool ExecuteSmetaDtFactEndVisible { get; set; }
		public bool ExecuteSmetaAdditionalVisible { get; set; }
		public bool MainSpecialistVisible { get; set; }
		public bool DtActPassingStripSurveyVisible { get; set; }
		public bool DtActPassingRapperVisible { get; set; }
		public bool DtTopoplanVisible { get; set; }
		public bool DtLoadInParallelProjectionVisible { get; set; }
		public bool DtProfilAndStatementVisible { get; set; }
		public bool DtTransferProfileIgiAndIgmiVisible { get; set; }
		public bool DtUnloadingReportFromSapsanVisible { get; set; }
		public bool DtNormocontrolVisible { get; set; }
		public bool ReasonVisible { get; set; }
		public bool HasRemarkVisible { get; set; }
		public bool DtKameralIgdiEndVisible { get; set; }
		public bool DtForecastEndIgdiVisible { get; set; }
		public bool VodotokCountVisible { get; set; }
		public bool CalculationsCountVisible { get; set; }
		public bool DtSigningVisible { get; set; }
		public bool PvoCountVisible { get; set; }
		public bool SurvayFieldVisible { get; set; }
		public bool SurvayTrackVisible { get; set; }
		public bool TZVisible { get; set; }
		public bool DtEndVisible { get; set; }
		public bool TfoVisible { get; set; }
		public bool ResponsibleUser2Visible { get; set; }
		public bool DtPlanCompilationVisible { get; set; }
		public bool DtSendCompilationVisible { get; set; }
		public bool DtSendVisible { get; set; }
		public bool FonVisible { get; set; }
		public bool PHHVisible { get; set; }
		public bool DtIssuePrescriptionGOVisible { get; set; }
		public bool PGRVisible { get; set; }
		public bool IGSVisible { get; set; }
		public bool PchvOaVisible { get; set; }
		public bool PchvBakVisible { get; set; }
		public bool PchvPrzVisible { get; set; }
		public bool AhSHVisible { get; set; }
		public bool AhPrVisible { get; set; }
		public bool PchvZsoVisible { get; set; }
		public bool PovVodVisible { get; set; }
		public bool PodzVodIpvsVisible { get; set; }
		public bool PodzVodIgsVisible { get; set; }
		public bool DonVisible { get; set; }
		public bool AvVisible { get; set; }
		public bool GraSVisible { get; set; }
		public bool RadVisible { get; set; }
		public bool ErnVisible { get; set; }
		public bool FfVisible { get; set; }
		public bool ProtocolRadiationVisible { get; set; }
		public bool DtForecastEndKameralIgdiVisible { get; set; }
		public bool DtForecastKameralIgiVisible { get; set; }
		public bool DtForecastCameralIgmiVisible { get; set; }
		public bool HasProtocolFieldSurvayVisible { get; set; }
		public bool HasGraficPartVisible { get; set; }
		public Guid? FromOperationalWorkId { get; set; }
		public bool VetstanciaVisible { get; set; }
		public bool DistrictsVisible { get; set; }
		public bool PlanSmetaCountSkvazhinVisible { get; set; }
		public bool PlanSmetaPmVisible { get; set; }
		public bool PlanSmetaCountMonolitVisible { get; set; }
		public bool ExecuteSmetaCountSkvazhinVisible { get; set; }
		public bool ExecuteSmetaPmVisible { get; set; }
		public bool ExecuteSmetaCountMonolitVisible { get; set; }
	}
}