using System.Collections.Generic;
using System.Linq;
using Sapsan.Modules.Operational.Presenters.Contracts;
using Sapsan.Modules.Operational.Presenters.Data;
using Sapsan.Modules.Operational.Services.Contracts;

namespace Sapsan.Modules.Operational.Presenters
{
	public class OperationalWorkPresenter : IOperationalWorkPresenter
	{
		private readonly IOperationalWorkService _operationalWorkService;
		private readonly IOperationalService _operationalService;

		public OperationalWorkPresenter(
			IOperationalWorkService operationalWorkService,
			IOperationalService operationalService)
		{
			_operationalWorkService = operationalWorkService;
			_operationalService = operationalService;
		}

		public List<OperationalWorkFullInfoPresenterData> GetData()
		{
			return (from x in _operationalWorkService.GetListWithDeleted()
					join y in _operationalService.GetListWithDeleted() on x.OperationalId equals y.Id
					orderby y.OrderNum, x.OrderNum
					select new OperationalWorkFullInfoPresenterData
					{
						OperationalName = y.Name,
						Id = x.Id,
						Name = x.Name,
						OperationalId = x.OperationalId,
						OrderNum = x.OrderNum,
						DtCreate = x.DtCreate,
						DtDelete = x.DtDelete,
						СomplianceWorkVisible = x.ComplianceWorkVisible,
						ComplianceWorkAllWorkVisible = x.ComplianceWorkAllWorkVisible,
						DtPlanStartVisible = x.DtPlanStartVisible,
						DtPlanEndVisible = x.DtPlanEndVisible,
						DtForecastStartVisible = x.DtForecastStartVisible,
						DtForecastEndVisible = x.DtForecastEndVisible,
						DtFactStartVisible = x.DtFactStartVisible,
						DtFactEndVisible = x.DtFactEndVisible,
						ResponsibleUserVisible = x.ResponsibleUserVisible,
						CommentVisible = x.CommentVisible,
						StatusVisible = x.StatusVisible,
						PlanSmetaMainVisible = x.PlanSmetaMainVisible,
						PlanSmetaFixedVisible = x.PlanSmetaFixedVisible,
						PlanSmeta3DScanVisible = x.PlanSmeta3DScanVisible,
						PlanSmetaGeoradScanVisible = x.PlanSmetaGeoradScanVisible,
						PlanSmetaResponsibleUserVisible = x.PlanSmetaResponsibleUserVisible,
						PlanSmetaDtFactStartVisible = x.PlanSmetaDtFactStartVisible,
						PlanSmetaDtFactEndVisible = x.PlanSmetaDtFactEndVisible,
						ExecuteSmetaMainVisible = x.ExecuteSmetaMainVisible,
						ExecuteSmetaFixedVisible = x.ExecuteSmetaFixedVisible,
						ExecuteSmeta3DScanVisible = x.ExecuteSmeta3DScanVisible,
						ExecuteSmetaGeoradScanVisible = x.ExecuteSmetaGeoradScanVisible,
						ExecuteSmetaResponsibleUserVisible = x.ExecuteSmetaResponsibleUserVisible,
						ExecuteSmetaDtFactStartVisible = x.ExecuteSmetaDtFactStartVisible,
						ExecuteSmetaDtFactEndVisible = x.ExecuteSmetaDtFactEndVisible,
						ExecuteSmetaAdditionalVisible = x.ExecuteSmetaAdditionalVisible,
						MainSpecialistVisible = x.MainSpecialistVisible,
						DtActPassingStripSurveyVisible = x.DtActPassingStripSurveyVisible,
						DtActPassingRapperVisible = x.DtActPassingRapperVisible,
						DtTopoplanVisible = x.DtTopoplanVisible,
						DtLoadInParallelProjectionVisible = x.DtLoadInParallelProjectionVisible,
						DtProfilAndStatementVisible = x.DtProfilAndStatementVisible,
						DtTransferProfileIgiAndIgmiVisible = x.DtTransferProfileIgiAndIgmiVisible,
						DtUnloadingReportFromSapsanVisible = x.DtUnloadingReportFromSapsanVisible,
						DtNormocontrolVisible = x.DtNormocontrolVisible,
						ReasonVisible = x.ReasonVisible,
						HasRemarkVisible = x.HasRemarkVisible,
						DtKameralIgdiEndVisible = x.DtKameralIgdiEndVisible,
						DtForecastEndIgdiVisible = x.DtForecastEndIgdiVisible,
						VodotokCountVisible = x.VodotokCountVisible,
						CalculationsCountVisible = x.CalculationsCountVisible,
						DtSigningVisible = x.DtSigningVisible,
						PvoCountVisible = x.PvoCountVisible,
						SurvayFieldVisible = x.SurvayFieldVisible,
						SurvayTrackVisible = x.SurvayTrackVisible,
						TZVisible = x.TZVisible,
						DtEndVisible = x.DtEndVisible,
						TfoVisible = x.TfoVisible,
						ResponsibleUser2Visible = x.ResponsibleUser2Visible,
						DtPlanCompilationVisible = x.DtPlanCompilationVisible,
						DtSendCompilationVisible = x.DtSendCompilationVisible,
						DtSendVisible = x.DtSendVisible,
						FonVisible = x.FonVisible,
						PHHVisible = x.PHHVisible,
						DtIssuePrescriptionGOVisible = x.DtIssuePrescriptionGOVisible,
						PGRVisible = x.PGRVisible,
						IGSVisible = x.IGSVisible,
						PchvOaVisible = x.PchvOaVisible,
						PchvBakVisible = x.PchvBakVisible,
						PchvPrzVisible = x.PchvPrzVisible,
						AhSHVisible = x.AhSHVisible,
						AhPrVisible = x.AhPrVisible,
						PchvZsoVisible = x.PchvZsoVisible,
						PovVodVisible = x.PovVodVisible,
						PodzVodIpvsVisible = x.PodzVodIpvsVisible,
						PodzVodIgsVisible = x.PodzVodIgsVisible,
						DonVisible = x.DonVisible,
						AvVisible = x.AvVisible,
						GraSVisible = x.GraSVisible,
						RadVisible = x.RadVisible,
						ErnVisible = x.ErnVisible,
						FfVisible = x.FfVisible,
						ProtocolRadiationVisible = x.ProtocolRadiationVisible,
						DtForecastEndKameralIgdiVisible = x.DtForecastEndKameralIgdiVisible,
						DtForecastKameralIgiVisible = x.DtForecastKameralIgiVisible,
						DtForecastCameralIgmiVisible = x.DtForecastCameralIgmiVisible,
						HasProtocolFieldSurvayVisible = x.HasProtocolFieldSurvayVisible,
						HasGraficPartVisible = x.HasGraficPartVisible,
						FromOperationalWorkId = x.FromOperationalWorkId,
						VetstanciaVisible = x.VetstanciaVisible,
						DistrictsVisible = x.DistrictsVisible,
						PlanSmetaCountSkvazhinVisible = x.PlanSmetaCountSkvazhinVisible,
						PlanSmetaPmVisible = x.PlanSmetaPmVisible,
						PlanSmetaCountMonolitVisible = x.PlanSmetaCountMonolitVisible,
						ExecuteSmetaCountSkvazhinVisible = x.ExecuteSmetaCountSkvazhinVisible,
						ExecuteSmetaPmVisible = x.ExecuteSmetaPmVisible,
						ExecuteSmetaCountMonolitVisible = x.ExecuteSmetaCountMonolitVisible
					}).ToList();
		}
	}
}