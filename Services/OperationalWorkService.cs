using System;
using System.Collections.Generic;
using System.Linq;
using Sapsan.Modules.Operational.DAL.Entity;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan.Modules.ProjectScheduler.Services;
using Sapsan2.Contracts.Exceptions;
using Sapsan2.Core.Service.Contracts;
using Sapsan2.Core.Transactions;
using TES.Fx.Data.Ef;

namespace Sapsan.Modules.Operational.Services
{
	public class OperationalWorkService : IOperationalWorkService
	{
		private readonly IEntityRepository<OperationalWork> _operationalWorkRepository;
		private readonly IEntityRepository<Operationals> _operationalRepository;
		private readonly IEntityRepository<OperationalLine> _operationalLineRepository;
		private readonly ILogService _logService;
		private readonly IOperationalWorkLineService _operationalWorkLineService;
		private readonly TaskService _taskService;

		public OperationalWorkService(
			IEntityRepository<OperationalWork> operationalWorkRepository,
			IEntityRepository<Operationals> operationalRepository,
			IEntityRepository<OperationalLine> operationalLineRepository,
			ILogService logService,
			IOperationalWorkLineService operationalWorkLineService,
			TaskService taskService)
		{
			_operationalWorkRepository = operationalWorkRepository;
			_operationalRepository = operationalRepository;
			_operationalLineRepository = operationalLineRepository;
			_logService = logService;
			_operationalWorkLineService = operationalWorkLineService;
			_taskService = taskService;
		}

		public IQueryable<OperationalWorkData> GetListWithDeleted()
		{
			return _operationalWorkRepository.GetAll().Select(x => new OperationalWorkData
			{
				Id = x.Id,
				Name = x.Name,
				OperationalId = x.OperationalId,
				OrderNum = x.OrderNum,
				DtCreate = x.DtCreate,
				DtDelete = x.DtDelete,
				ComplianceWorkVisible = x.СomplianceWorkVisible,
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
			});
		}

		public IQueryable<OperationalWorkData> GetList()
		{
			return GetListWithDeleted().Where(x => x.DtDelete.HasValue == false);
		}

		private void Map(OperationalWorkData from, OperationalWork to)
		{
			to.Name = from.Name;
			to.OperationalId = from.OperationalId;
			to.OrderNum = from.OrderNum;
			to.DtDelete = from.DtDelete;
			to.СomplianceWorkVisible = from.ComplianceWorkVisible;
			to.ComplianceWorkAllWorkVisible = from.ComplianceWorkAllWorkVisible;
			to.DtPlanStartVisible = from.DtPlanStartVisible;
			to.DtPlanEndVisible = from.DtPlanEndVisible;
			to.DtForecastStartVisible = from.DtForecastStartVisible;
			to.DtForecastEndVisible = from.DtForecastEndVisible;
			to.DtFactStartVisible = from.DtFactStartVisible;
			to.DtFactEndVisible = from.DtFactEndVisible;
			to.ResponsibleUserVisible = from.ResponsibleUserVisible;
			to.CommentVisible = from.CommentVisible;
			to.StatusVisible = from.StatusVisible;
			to.PlanSmetaMainVisible = from.PlanSmetaMainVisible;
			to.PlanSmetaFixedVisible = from.PlanSmetaFixedVisible;
			to.PlanSmeta3DScanVisible = from.PlanSmeta3DScanVisible;
			to.PlanSmetaGeoradScanVisible = from.PlanSmetaGeoradScanVisible;
			to.PlanSmetaResponsibleUserVisible = from.PlanSmetaResponsibleUserVisible;
			to.PlanSmetaDtFactStartVisible = from.PlanSmetaDtFactStartVisible;
			to.PlanSmetaDtFactEndVisible = from.PlanSmetaDtFactEndVisible;
			to.ExecuteSmetaMainVisible = from.ExecuteSmetaMainVisible;
			to.ExecuteSmetaFixedVisible = from.ExecuteSmetaFixedVisible;
			to.ExecuteSmeta3DScanVisible = from.ExecuteSmeta3DScanVisible;
			to.ExecuteSmetaGeoradScanVisible = from.ExecuteSmetaGeoradScanVisible;
			to.ExecuteSmetaResponsibleUserVisible = from.ExecuteSmetaResponsibleUserVisible;
			to.ExecuteSmetaDtFactStartVisible = from.ExecuteSmetaDtFactStartVisible;
			to.ExecuteSmetaDtFactEndVisible = from.ExecuteSmetaDtFactEndVisible;
			to.ExecuteSmetaAdditionalVisible = from.ExecuteSmetaAdditionalVisible;
			to.MainSpecialistVisible = from.MainSpecialistVisible;
			to.DtActPassingStripSurveyVisible = from.DtActPassingStripSurveyVisible;
			to.DtActPassingRapperVisible = from.DtActPassingRapperVisible;
			to.DtTopoplanVisible = from.DtTopoplanVisible;
			to.DtLoadInParallelProjectionVisible = from.DtLoadInParallelProjectionVisible;
			to.DtProfilAndStatementVisible = from.DtProfilAndStatementVisible;
			to.DtTransferProfileIgiAndIgmiVisible = from.DtTransferProfileIgiAndIgmiVisible;
			to.DtUnloadingReportFromSapsanVisible = from.DtUnloadingReportFromSapsanVisible;
			to.DtNormocontrolVisible = from.DtNormocontrolVisible;
			to.ReasonVisible = from.ReasonVisible;
			to.HasRemarkVisible = from.HasRemarkVisible;
			to.DtKameralIgdiEndVisible = from.DtKameralIgdiEndVisible;
			to.DtForecastEndIgdiVisible = from.DtForecastEndIgdiVisible;
			to.VodotokCountVisible = from.VodotokCountVisible;
			to.CalculationsCountVisible = from.CalculationsCountVisible;
			to.DtSigningVisible = from.DtSigningVisible;
			to.PvoCountVisible = from.PvoCountVisible;
			to.SurvayFieldVisible = from.SurvayFieldVisible;
			to.SurvayTrackVisible = from.SurvayTrackVisible;
			to.TZVisible = from.TZVisible;
			to.DtEndVisible = from.DtEndVisible;
			to.TfoVisible = from.TfoVisible;
			to.ResponsibleUser2Visible = from.ResponsibleUser2Visible;
			to.DtPlanCompilationVisible = from.DtPlanCompilationVisible;
			to.DtSendCompilationVisible = from.DtSendCompilationVisible;
			to.DtSendVisible = from.DtSendVisible;
			to.FonVisible = from.FonVisible;
			to.PHHVisible = from.PHHVisible;
			to.DtIssuePrescriptionGOVisible = from.DtIssuePrescriptionGOVisible;
			to.PGRVisible = from.PGRVisible;
			to.IGSVisible = from.IGSVisible;
			to.PchvOaVisible = from.PchvOaVisible;
			to.PchvBakVisible = from.PchvBakVisible;
			to.PchvPrzVisible = from.PchvPrzVisible;
			to.AhSHVisible = from.AhSHVisible;
			to.AhPrVisible = from.AhPrVisible;
			to.PchvZsoVisible = from.PchvZsoVisible;
			to.PovVodVisible = from.PovVodVisible;
			to.PodzVodIpvsVisible = from.PodzVodIpvsVisible;
			to.PodzVodIgsVisible = from.PodzVodIgsVisible;
			to.DonVisible = from.DonVisible;
			to.AvVisible = from.AvVisible;
			to.GraSVisible = from.GraSVisible;
			to.RadVisible = from.RadVisible;
			to.ErnVisible = from.ErnVisible;
			to.FfVisible = from.FfVisible;
			to.ProtocolRadiationVisible = from.ProtocolRadiationVisible;
			to.DtForecastEndKameralIgdiVisible = from.DtForecastEndKameralIgdiVisible;
			to.DtForecastKameralIgiVisible = from.DtForecastKameralIgiVisible;
			to.DtForecastCameralIgmiVisible = from.DtForecastCameralIgmiVisible;
			to.HasProtocolFieldSurvayVisible = from.HasProtocolFieldSurvayVisible;
			to.HasGraficPartVisible = from.HasGraficPartVisible;
			to.FromOperationalWorkId = from.FromOperationalWorkId;
			to.VetstanciaVisible = from.VetstanciaVisible;
			to.DistrictsVisible = from.DistrictsVisible;
			to.PlanSmetaCountSkvazhinVisible = from.PlanSmetaCountSkvazhinVisible;
			to.PlanSmetaPmVisible = from.PlanSmetaPmVisible;
			to.PlanSmetaCountMonolitVisible = from.PlanSmetaCountMonolitVisible;
			to.ExecuteSmetaCountSkvazhinVisible = from.ExecuteSmetaCountSkvazhinVisible;
			to.ExecuteSmetaPmVisible = from.ExecuteSmetaPmVisible;
			to.ExecuteSmetaCountMonolitVisible = from.ExecuteSmetaCountMonolitVisible;
		}

		public Guid Add(OperationalWorkData entity)
		{
			var operationalWork = new OperationalWork();
			Map(entity, operationalWork);
			operationalWork.Id = Guid.NewGuid();
			operationalWork.DtCreate = DateTime.Now;

			using (var scope = new RequiredTransactionScope())
			{
				_operationalWorkRepository.Add(operationalWork);

				if (entity.FromOperationalWorkId.HasValue == false)
				{
					foreach (var operationalLineData in _operationalLineRepository.GetAll()
						.Where(x => x.OperationalId == operationalWork.OperationalId).ToList())
					{
						var complianceWorkId = operationalWork.СomplianceWorkVisible ? _taskService.GetList().FirstOrDefault(x => x.WorkId == operationalLineData.ComplianceGroupId
							&& x.Name == operationalWork.Name)?.Id : null;

						_operationalWorkLineService.Add(new OperationalWorkLineData
						{
							OperationalWorkId = operationalWork.Id,
							OperationalLineId = operationalLineData.Id,
							ComplianceWorkId = complianceWorkId
						});
					}
				}

				_logService.Log(
					operationalWork.Id,
					"Добавление работы",
					entity.Name,
					operationalWork.Id);

				scope.Complete();
			}

			return operationalWork.Id;
		}

		public void Update(Guid id, OperationalWorkData entity)
		{
			var operationalWork = _operationalWorkRepository.GetAll().FirstOrDefault(x => x.Id == id);
			if (operationalWork != null)
			{
				if (operationalWork.Name == entity.Name
					&& operationalWork.OperationalId == entity.OperationalId
					&& operationalWork.OrderNum == entity.OrderNum
					&& operationalWork.СomplianceWorkVisible == entity.ComplianceWorkVisible
					&& operationalWork.ComplianceWorkAllWorkVisible == entity.ComplianceWorkAllWorkVisible
					&& operationalWork.DtPlanStartVisible == entity.DtPlanStartVisible
					&& operationalWork.DtPlanEndVisible == entity.DtPlanEndVisible
					&& operationalWork.DtForecastStartVisible == entity.DtForecastStartVisible
					&& operationalWork.DtForecastEndVisible == entity.DtForecastEndVisible
					&& operationalWork.DtFactStartVisible == entity.DtFactStartVisible
					&& operationalWork.DtFactEndVisible == entity.DtFactEndVisible
					&& operationalWork.ResponsibleUserVisible == entity.ResponsibleUserVisible
					&& operationalWork.CommentVisible == entity.CommentVisible
					&& operationalWork.StatusVisible == entity.StatusVisible
					&& operationalWork.PlanSmetaMainVisible == entity.PlanSmetaMainVisible
					&& operationalWork.PlanSmetaFixedVisible == entity.PlanSmetaFixedVisible
					&& operationalWork.PlanSmeta3DScanVisible == entity.PlanSmeta3DScanVisible
					&& operationalWork.PlanSmetaGeoradScanVisible == entity.PlanSmetaGeoradScanVisible
					&& operationalWork.PlanSmetaResponsibleUserVisible == entity.PlanSmetaResponsibleUserVisible
					&& operationalWork.PlanSmetaDtFactStartVisible == entity.PlanSmetaDtFactStartVisible
					&& operationalWork.PlanSmetaDtFactEndVisible == entity.PlanSmetaDtFactEndVisible
					&& operationalWork.ExecuteSmetaMainVisible == entity.ExecuteSmetaMainVisible
					&& operationalWork.ExecuteSmetaFixedVisible == entity.ExecuteSmetaFixedVisible
					&& operationalWork.ExecuteSmeta3DScanVisible == entity.ExecuteSmeta3DScanVisible
					&& operationalWork.ExecuteSmetaGeoradScanVisible == entity.ExecuteSmetaGeoradScanVisible
					&& operationalWork.ExecuteSmetaResponsibleUserVisible == entity.ExecuteSmetaResponsibleUserVisible
					&& operationalWork.ExecuteSmetaDtFactStartVisible == entity.ExecuteSmetaDtFactStartVisible
					&& operationalWork.ExecuteSmetaDtFactEndVisible == entity.ExecuteSmetaDtFactEndVisible
					&& operationalWork.ExecuteSmetaAdditionalVisible == entity.ExecuteSmetaAdditionalVisible
					&& operationalWork.MainSpecialistVisible == entity.MainSpecialistVisible
					&& operationalWork.DtActPassingStripSurveyVisible == entity.DtActPassingStripSurveyVisible
					&& operationalWork.DtActPassingRapperVisible == entity.DtActPassingRapperVisible
					&& operationalWork.DtTopoplanVisible == entity.DtTopoplanVisible
					&& operationalWork.DtLoadInParallelProjectionVisible == entity.DtLoadInParallelProjectionVisible
					&& operationalWork.DtProfilAndStatementVisible == entity.DtProfilAndStatementVisible
					&& operationalWork.DtTransferProfileIgiAndIgmiVisible == entity.DtTransferProfileIgiAndIgmiVisible
					&& operationalWork.DtUnloadingReportFromSapsanVisible == entity.DtUnloadingReportFromSapsanVisible
					&& operationalWork.DtNormocontrolVisible == entity.DtNormocontrolVisible
					&& operationalWork.ReasonVisible == entity.ReasonVisible
					&& operationalWork.HasRemarkVisible == entity.HasRemarkVisible
					&& operationalWork.DtKameralIgdiEndVisible == entity.DtKameralIgdiEndVisible
					&& operationalWork.DtForecastEndIgdiVisible == entity.DtForecastEndIgdiVisible
					&& operationalWork.VodotokCountVisible == entity.VodotokCountVisible
					&& operationalWork.CalculationsCountVisible == entity.CalculationsCountVisible
					&& operationalWork.DtSigningVisible == entity.DtSigningVisible
					&& operationalWork.PvoCountVisible == entity.PvoCountVisible
					&& operationalWork.SurvayFieldVisible == entity.SurvayFieldVisible
					&& operationalWork.SurvayTrackVisible == entity.SurvayTrackVisible
					&& operationalWork.TZVisible == entity.TZVisible
					&& operationalWork.DtEndVisible == entity.DtEndVisible
					&& operationalWork.TfoVisible == entity.TfoVisible
					&& operationalWork.ResponsibleUser2Visible == entity.ResponsibleUser2Visible
					&& operationalWork.DtPlanCompilationVisible == entity.DtPlanCompilationVisible
					&& operationalWork.DtSendCompilationVisible == entity.DtSendCompilationVisible
					&& operationalWork.DtSendVisible == entity.DtSendVisible
					&& operationalWork.FonVisible == entity.FonVisible
					&& operationalWork.PHHVisible == entity.PHHVisible
					&& operationalWork.DtIssuePrescriptionGOVisible == entity.DtIssuePrescriptionGOVisible
					&& operationalWork.PGRVisible == entity.PGRVisible
					&& operationalWork.IGSVisible == entity.IGSVisible
					&& operationalWork.PchvOaVisible == entity.PchvOaVisible
					&& operationalWork.PchvBakVisible == entity.PchvBakVisible
					&& operationalWork.PchvPrzVisible == entity.PchvPrzVisible
					&& operationalWork.AhSHVisible == entity.AhSHVisible
					&& operationalWork.AhPrVisible == entity.AhPrVisible
					&& operationalWork.PchvZsoVisible == entity.PchvZsoVisible
					&& operationalWork.PovVodVisible == entity.PovVodVisible
					&& operationalWork.PodzVodIpvsVisible == entity.PodzVodIpvsVisible
					&& operationalWork.PodzVodIgsVisible == entity.PodzVodIgsVisible
					&& operationalWork.DonVisible == entity.DonVisible
					&& operationalWork.AvVisible == entity.AvVisible
					&& operationalWork.GraSVisible == entity.GraSVisible
					&& operationalWork.RadVisible == entity.RadVisible
					&& operationalWork.ErnVisible == entity.ErnVisible
					&& operationalWork.FfVisible == entity.FfVisible
					&& operationalWork.ProtocolRadiationVisible == entity.ProtocolRadiationVisible
					&& operationalWork.DtForecastEndKameralIgdiVisible == entity.DtForecastEndKameralIgdiVisible
					&& operationalWork.DtForecastKameralIgiVisible == entity.DtForecastKameralIgiVisible
					&& operationalWork.DtForecastCameralIgmiVisible == entity.DtForecastCameralIgmiVisible
					&& operationalWork.HasProtocolFieldSurvayVisible == entity.HasProtocolFieldSurvayVisible
					&& operationalWork.HasGraficPartVisible == entity.HasGraficPartVisible
					&& operationalWork.FromOperationalWorkId == entity.FromOperationalWorkId
					&& operationalWork.VetstanciaVisible == entity.VetstanciaVisible
					&& operationalWork.DistrictsVisible == entity.DistrictsVisible
					&& operationalWork.PlanSmetaCountSkvazhinVisible == entity.PlanSmetaCountSkvazhinVisible
					&& operationalWork.PlanSmetaPmVisible == entity.PlanSmetaPmVisible
					&& operationalWork.PlanSmetaCountMonolitVisible == entity.PlanSmetaCountMonolitVisible
					&& operationalWork.ExecuteSmetaCountSkvazhinVisible == entity.ExecuteSmetaCountSkvazhinVisible
					&& operationalWork.ExecuteSmetaPmVisible == entity.ExecuteSmetaPmVisible
					&& operationalWork.ExecuteSmetaCountMonolitVisible == entity.ExecuteSmetaCountMonolitVisible
				)
				{
					return;
				}

				var changeList = new List<string>();

				if (operationalWork.Name != entity.Name)
				{
					changeList.Add($"Наименование: {operationalWork.Name} -> {entity.Name}");
				}

				if (operationalWork.OrderNum != entity.OrderNum)
				{
					changeList.Add($"Порядковый номер: {operationalWork.OrderNum} -> {entity.OrderNum}");
				}

				if (operationalWork.OperationalId != entity.OperationalId)
				{
					changeList.Add(
						$@"Оперативка: {_operationalRepository.GetAll().Single(x => x.Id == operationalWork.OperationalId).Name} 
					-> {_operationalRepository.GetAll().Single(x => x.Id == entity.OperationalId).Name}");
				}

				if (operationalWork.СomplianceWorkVisible != entity.ComplianceWorkVisible)
				{
					changeList.Add(
						$"Соответствуеет работе: {operationalWork.СomplianceWorkVisible} -> {entity.ComplianceWorkVisible}");
				}

				if (operationalWork.ComplianceWorkAllWorkVisible != entity.ComplianceWorkAllWorkVisible)
				{
					changeList.Add(
						$"Соответствуеет работе всего ПГ: {operationalWork.ComplianceWorkAllWorkVisible} -> {entity.ComplianceWorkAllWorkVisible}");
				}

				if (operationalWork.DtPlanStartVisible != entity.DtPlanStartVisible)
				{
					changeList.Add(
						$"Плановая дата начала: {operationalWork.DtPlanStartVisible} -> {entity.DtPlanStartVisible}");
				}

				if (operationalWork.DtPlanEndVisible != entity.DtPlanEndVisible)
				{
					changeList.Add(
						$"Плановая дата окончания: {operationalWork.DtPlanEndVisible} -> {entity.DtPlanEndVisible}");
				}

				if (operationalWork.DtForecastStartVisible != entity.DtForecastStartVisible)
				{
					changeList.Add(
						$"Прогнозная дата начала: {operationalWork.DtForecastStartVisible} -> {entity.DtForecastStartVisible}");
				}

				if (operationalWork.DtForecastEndVisible != entity.DtForecastEndVisible)
				{
					changeList.Add(
						$"Прогнозная дата окончания: {operationalWork.DtForecastEndVisible} -> {entity.DtForecastEndVisible}");
				}

				if (operationalWork.DtFactStartVisible != entity.DtFactStartVisible)
				{
					changeList.Add(
						$"Фактическая дата начала: {operationalWork.DtFactStartVisible} -> {entity.DtFactStartVisible}");
				}

				if (operationalWork.DtFactEndVisible != entity.DtFactEndVisible)
				{
					changeList.Add(
						$"Фактическая дата окончания: {operationalWork.DtFactEndVisible} -> {entity.DtFactEndVisible}");
				}

				if (operationalWork.ResponsibleUserVisible != entity.ResponsibleUserVisible)
				{
					changeList.Add(
						$"Ответственный: {operationalWork.ResponsibleUserVisible} -> {entity.ResponsibleUserVisible}");
				}

				if (operationalWork.CommentVisible != entity.CommentVisible)
				{
					changeList.Add($"Комментарий: {operationalWork.CommentVisible} -> {entity.CommentVisible}");
				}

				if (operationalWork.StatusVisible != entity.StatusVisible)
				{
					changeList.Add($"Статус: {operationalWork.StatusVisible} -> {entity.StatusVisible}");
				}

				if (operationalWork.PlanSmetaMainVisible != entity.PlanSmetaMainVisible)
				{
					changeList.Add(
						$"Плановая смета.Основная: {operationalWork.PlanSmetaMainVisible} -> {entity.PlanSmetaMainVisible}");
				}

				if (operationalWork.PlanSmetaFixedVisible != entity.PlanSmetaFixedVisible)
				{
					changeList.Add(
						$"Плановая сместа.Закрепленная: {operationalWork.PlanSmetaFixedVisible} -> {entity.PlanSmetaFixedVisible}");
				}

				if (operationalWork.PlanSmeta3DScanVisible != entity.PlanSmeta3DScanVisible)
				{
					changeList.Add(
						$"Плановая сместа.3D сканирование: {operationalWork.PlanSmeta3DScanVisible} -> {entity.PlanSmeta3DScanVisible}");
				}

				if (operationalWork.PlanSmetaGeoradScanVisible != entity.PlanSmetaGeoradScanVisible)
				{
					changeList.Add(
						$"Плановая сместа.Георадарное сканирование: {operationalWork.PlanSmetaGeoradScanVisible} -> {entity.PlanSmetaGeoradScanVisible}");
				}

				if (operationalWork.PlanSmetaResponsibleUserVisible != entity.PlanSmetaResponsibleUserVisible)
				{
					changeList.Add(
						$"Плановая смета.Ответственный: {operationalWork.PlanSmetaResponsibleUserVisible} -> {entity.PlanSmetaResponsibleUserVisible}");
				}

				if (operationalWork.PlanSmetaDtFactStartVisible != entity.PlanSmetaDtFactStartVisible)
				{
					changeList.Add(
						$"Плановая смета.Фактическая дата начала: {operationalWork.PlanSmetaDtFactStartVisible} -> {entity.PlanSmetaDtFactStartVisible}");
				}

				if (operationalWork.PlanSmetaDtFactEndVisible != entity.PlanSmetaDtFactEndVisible)
				{
					changeList.Add(
						$"Плановая смета.Фактическая дана окончания: {operationalWork.PlanSmetaDtFactEndVisible} -> {entity.PlanSmetaDtFactEndVisible}");
				}

				if (operationalWork.ExecuteSmetaMainVisible != entity.ExecuteSmetaMainVisible)
				{
					changeList.Add(
						$"Исполнительная смета.Основная: {operationalWork.ExecuteSmetaMainVisible} -> {entity.ExecuteSmetaMainVisible}");
				}

				if (operationalWork.ExecuteSmetaFixedVisible != entity.ExecuteSmetaFixedVisible)
				{
					changeList.Add(
						$"Исполнительная смета. Закрепление: {operationalWork.ExecuteSmetaFixedVisible} -> {entity.ExecuteSmetaFixedVisible}");
				}

				if (operationalWork.ExecuteSmeta3DScanVisible != entity.ExecuteSmeta3DScanVisible)
				{
					changeList.Add(
						$"Исполнительная смета.3D сканирование: {operationalWork.ExecuteSmeta3DScanVisible} -> {entity.ExecuteSmeta3DScanVisible}");
				}

				if (operationalWork.ExecuteSmetaGeoradScanVisible != entity.ExecuteSmetaGeoradScanVisible)
				{
					changeList.Add(
						$"Исполнительная смета.Георадарное сканирование: {operationalWork.ExecuteSmetaGeoradScanVisible} -> {entity.ExecuteSmetaGeoradScanVisible}");
				}

				if (operationalWork.ExecuteSmetaResponsibleUserVisible != entity.ExecuteSmetaResponsibleUserVisible)
				{
					changeList.Add(
						$"Исполнительная смета.Ответственный: {operationalWork.ExecuteSmetaResponsibleUserVisible} -> {entity.ExecuteSmetaResponsibleUserVisible}");
				}

				if (operationalWork.ExecuteSmetaDtFactStartVisible != entity.ExecuteSmetaDtFactStartVisible)
				{
					changeList.Add(
						$"Исполнительная смета.Фактическая дата начала: {operationalWork.ExecuteSmetaDtFactStartVisible} -> {entity.ExecuteSmetaDtFactStartVisible}");
				}

				if (operationalWork.ExecuteSmetaDtFactEndVisible != entity.ExecuteSmetaDtFactEndVisible)
				{
					changeList.Add(
						$"Исполнительная смета.Фактическая дата окончания: {operationalWork.ExecuteSmetaDtFactEndVisible} -> {entity.ExecuteSmetaDtFactEndVisible}");
				}

				if (operationalWork.ExecuteSmetaAdditionalVisible != entity.ExecuteSmetaAdditionalVisible)
				{
					changeList.Add(
						$"Исполнительная смета.Дополнительная: {operationalWork.ExecuteSmetaAdditionalVisible} -> {entity.ExecuteSmetaAdditionalVisible}");
				}

				if (operationalWork.MainSpecialistVisible != entity.MainSpecialistVisible)
				{
					changeList.Add(
						$"Главный специалист: {operationalWork.MainSpecialistVisible} -> {entity.MainSpecialistVisible}");
				}

				if (operationalWork.DtActPassingStripSurveyVisible != entity.DtActPassingStripSurveyVisible)
				{
					changeList.Add(
						$"Акт сдачи полосовой съемки: {operationalWork.DtActPassingStripSurveyVisible} -> {entity.DtActPassingStripSurveyVisible}");
				}

				if (operationalWork.DtActPassingRapperVisible != entity.DtActPassingRapperVisible)
				{
					changeList.Add(
						$"Акт сдачи реперов: {operationalWork.DtActPassingRapperVisible} -> {entity.DtActPassingRapperVisible}");
				}

				if (operationalWork.DtTopoplanVisible != entity.DtTopoplanVisible)
				{
					changeList.Add($"Топопланы: {operationalWork.DtTopoplanVisible} -> {entity.DtTopoplanVisible}");
				}

				if (operationalWork.DtLoadInParallelProjectionVisible != entity.DtLoadInParallelProjectionVisible)
				{
					changeList.Add(
						$"Загрузка в параллел. проект: {operationalWork.DtLoadInParallelProjectionVisible} -> {entity.DtLoadInParallelProjectionVisible}");
				}

				if (operationalWork.DtProfilAndStatementVisible != entity.DtProfilAndStatementVisible)
				{
					changeList.Add(
						$"Профили и ведомости: {operationalWork.DtProfilAndStatementVisible} -> {entity.DtProfilAndStatementVisible}");
				}

				if (operationalWork.DtTransferProfileIgiAndIgmiVisible != entity.DtTransferProfileIgiAndIgmiVisible)
				{
					changeList.Add(
						$"Передача профилей ИГИ и ИГМИ: {operationalWork.DtTransferProfileIgiAndIgmiVisible} -> {entity.DtTransferProfileIgiAndIgmiVisible}");
				}

				if (operationalWork.DtUnloadingReportFromSapsanVisible != entity.DtUnloadingReportFromSapsanVisible)
				{
					changeList.Add(
						$"Выгрузка отчета в Сапсан2020: {operationalWork.DtUnloadingReportFromSapsanVisible} -> {entity.DtUnloadingReportFromSapsanVisible}");
				}

				if (operationalWork.DtNormocontrolVisible != entity.DtNormocontrolVisible)
				{
					changeList.Add(
						$"Нормоконтроль: {operationalWork.DtNormocontrolVisible} -> {entity.DtNormocontrolVisible}");
				}

				if (operationalWork.ReasonVisible != entity.ReasonVisible)
				{
					changeList.Add($"Причина: {operationalWork.ReasonVisible} -> {entity.ReasonVisible}");
				}

				if (operationalWork.HasRemarkVisible != entity.HasRemarkVisible)
				{
					changeList.Add(
						$"Наличие замечания: {operationalWork.HasRemarkVisible} -> {entity.HasRemarkVisible}");
				}

				if (operationalWork.DtKameralIgdiEndVisible != entity.DtKameralIgdiEndVisible)
				{
					changeList.Add(
						$"Окончание камеральных ИГДИ: {operationalWork.DtKameralIgdiEndVisible} -> {entity.DtKameralIgdiEndVisible}");
				}

				if (operationalWork.DtForecastEndIgdiVisible != entity.DtForecastEndIgdiVisible)
				{
					changeList.Add(
						$"Прогноз.срок окончания ИГДИ: {operationalWork.DtForecastEndIgdiVisible} -> {entity.DtForecastEndIgdiVisible}");
				}

				if (operationalWork.VodotokCountVisible != entity.VodotokCountVisible)
				{
					changeList.Add(
						$"Число водотоков: {operationalWork.VodotokCountVisible} -> {entity.VodotokCountVisible}");
				}

				if (operationalWork.CalculationsCountVisible != entity.CalculationsCountVisible)
				{
					changeList.Add(
						$"Количество расчетов: {operationalWork.CalculationsCountVisible} -> {entity.CalculationsCountVisible}");
				}

				if (operationalWork.DtSigningVisible != entity.DtSigningVisible)
				{
					changeList.Add($"Дата подписания: {operationalWork.DtSigningVisible} -> {entity.DtSigningVisible}");
				}

				if (operationalWork.PvoCountVisible != entity.PvoCountVisible)
				{
					changeList.Add($"ПВО, всего шт: {operationalWork.PvoCountVisible} -> {entity.PvoCountVisible}");
				}

				if (operationalWork.SurvayFieldVisible != entity.SurvayFieldVisible)
				{
					changeList.Add(
						$"Изыскания площадок, всего га: {operationalWork.SurvayFieldVisible} -> {entity.SurvayFieldVisible}");
				}

				if (operationalWork.SurvayTrackVisible != entity.SurvayTrackVisible)
				{
					changeList.Add(
						$"Изыскания трасс, всего км: {operationalWork.SurvayTrackVisible} -> {entity.SurvayTrackVisible}");
				}

				if (operationalWork.TZVisible != entity.TZVisible)
				{
					changeList.Add($"ТЗ: {operationalWork.TZVisible} -> {entity.TZVisible}");
				}

				if (operationalWork.DtEndVisible != entity.DtEndVisible)
				{
					changeList.Add($"Дата окончания: {operationalWork.DtEndVisible} -> {entity.DtEndVisible}");
				}

				if (operationalWork.TfoVisible != entity.TfoVisible)
				{
					changeList.Add($"ТФО: {operationalWork.TfoVisible} -> {entity.TfoVisible}");
				}

				if (operationalWork.ResponsibleUser2Visible != entity.ResponsibleUser2Visible)
				{
					changeList.Add(
						$"Ответственный2: {operationalWork.ResponsibleUser2Visible} -> {entity.ResponsibleUser2Visible}");
				}

				if (operationalWork.DtPlanCompilationVisible != entity.DtPlanCompilationVisible)
				{
					changeList.Add(
						$"Плановая дата составления: {operationalWork.DtPlanCompilationVisible} -> {entity.DtPlanCompilationVisible}");
				}

				if (operationalWork.DtSendCompilationVisible != entity.DtSendCompilationVisible)
				{
					changeList.Add(
						$"Дата отправки на согласование: {operationalWork.DtSendCompilationVisible} -> {entity.DtSendCompilationVisible}");
				}

				if (operationalWork.DtSendVisible != entity.DtSendVisible)
				{
					changeList.Add($"Дата отправки: {operationalWork.DtSendVisible} -> {entity.DtSendVisible}");
				}

				if (operationalWork.FonVisible != entity.FonVisible)
				{
					changeList.Add($"ФОН: {operationalWork.FonVisible} -> {entity.FonVisible}");
				}

				if (operationalWork.PHHVisible != entity.PHHVisible)
				{
					changeList.Add($"РХХ: {operationalWork.PHHVisible} -> {entity.PHHVisible}");
				}

				if (operationalWork.DtIssuePrescriptionGOVisible != entity.DtIssuePrescriptionGOVisible)
				{
					changeList.Add(
						$"Предписание ГО дата выдачи: {operationalWork.DtIssuePrescriptionGOVisible} -> {entity.DtIssuePrescriptionGOVisible}");
				}

				if (operationalWork.PGRVisible != entity.PGRVisible)
				{
					changeList.Add($"ПГР (ИГИ): {operationalWork.PGRVisible} -> {entity.PGRVisible}");
				}

				if (operationalWork.IGSVisible != entity.IGSVisible)
				{
					changeList.Add($"ИГС (ИГИ): {operationalWork.IGSVisible} -> {entity.IGSVisible}");
				}

				if (operationalWork.PchvOaVisible != entity.PchvOaVisible)
				{
					changeList.Add($"ПЧВ ОА: {operationalWork.PchvOaVisible} -> {entity.PchvOaVisible}");
				}

				if (operationalWork.PchvBakVisible != entity.PchvBakVisible)
				{
					changeList.Add($"ПЧВ БАК: {operationalWork.PchvBakVisible} -> {entity.PchvBakVisible}");
				}

				if (operationalWork.PchvPrzVisible != entity.PchvPrzVisible)
				{
					changeList.Add($"ПЧВ ПРЗ: {operationalWork.PchvPrzVisible} -> {entity.PchvPrzVisible}");
				}

				if (operationalWork.AhSHVisible != entity.AhSHVisible)
				{
					changeList.Add($"АХ Ш: {operationalWork.AhSHVisible} -> {entity.AhSHVisible}");
				}

				if (operationalWork.AhPrVisible != entity.AhPrVisible)
				{
					changeList.Add($"АХ ПР: {operationalWork.AhPrVisible} -> {entity.AhPrVisible}");
				}

				if (operationalWork.PchvZsoVisible != entity.PchvZsoVisible)
				{
					changeList.Add($"ПЧВ ЗСО: {operationalWork.PchvZsoVisible} -> {entity.PchvZsoVisible}");
				}

				if (operationalWork.PovVodVisible != entity.PovVodVisible)
				{
					changeList.Add($"ПОВ.ВОД: {operationalWork.PovVodVisible} -> {entity.PovVodVisible}");
				}

				if (operationalWork.PodzVodIpvsVisible != entity.PodzVodIpvsVisible)
				{
					changeList.Add(
						$"ПОДЗ.ВОД ИПВС: {operationalWork.PodzVodIpvsVisible} -> {entity.PodzVodIpvsVisible}");
				}

				if (operationalWork.PodzVodIgsVisible != entity.PodzVodIgsVisible)
				{
					changeList.Add($"ПОДЗ.ВОД ИГС: {operationalWork.PodzVodIgsVisible} -> {entity.PodzVodIgsVisible}");
				}

				if (operationalWork.DonVisible != entity.DonVisible)
				{
					changeList.Add($"ДОН: {operationalWork.DonVisible} -> {entity.DonVisible}");
				}

				if (operationalWork.AvVisible != entity.AvVisible)
				{
					changeList.Add($"АВ: {operationalWork.AvVisible} -> {entity.AvVisible}");
				}

				if (operationalWork.GraSVisible != entity.GraSVisible)
				{
					changeList.Add($"ГраС: {operationalWork.GraSVisible} -> {entity.GraSVisible}");
				}

				if (operationalWork.RadVisible != entity.RadVisible)
				{
					changeList.Add($"РАД: {operationalWork.RadVisible} -> {entity.RadVisible}");
				}

				if (operationalWork.ErnVisible != entity.ErnVisible)
				{
					changeList.Add($"ЕРН: {operationalWork.ErnVisible} -> {entity.ErnVisible}");
				}

				if (operationalWork.FfVisible != entity.FfVisible)
				{
					changeList.Add($"ФФ: {operationalWork.FfVisible} -> {entity.FfVisible}");
				}

				if (operationalWork.ProtocolRadiationVisible != entity.ProtocolRadiationVisible)
				{
					changeList.Add(
						$"Протокол радиации: {operationalWork.ProtocolRadiationVisible} -> {entity.ProtocolRadiationVisible}");
				}

				if (operationalWork.DtForecastEndKameralIgdiVisible != entity.DtForecastEndKameralIgdiVisible)
				{
					changeList.Add(
						$"Прогнозная дата окончания камеральных работ ИГДИ: {operationalWork.DtForecastEndKameralIgdiVisible} -> {entity.DtForecastEndKameralIgdiVisible}");
				}

				if (operationalWork.DtForecastKameralIgiVisible != entity.DtForecastKameralIgiVisible)
				{
					changeList.Add(
						$"Прогнозная дата окончания камеральных работ ИГИ: {operationalWork.DtForecastKameralIgiVisible} -> {entity.DtForecastKameralIgiVisible}");
				}

				if (operationalWork.DtForecastCameralIgmiVisible != entity.DtForecastCameralIgmiVisible)
				{
					changeList.Add(
						$"Прогнозная дата окончания камеральных работ ИГМИ: {operationalWork.DtForecastCameralIgmiVisible} -> {entity.DtForecastCameralIgmiVisible}");
				}

				if (operationalWork.HasProtocolFieldSurvayVisible != entity.HasProtocolFieldSurvayVisible)
				{
					changeList.Add(
						$"Наличие протоколов полевого обследования: {operationalWork.HasProtocolFieldSurvayVisible} -> {entity.HasProtocolFieldSurvayVisible}");
				}

				if (operationalWork.HasGraficPartVisible != entity.HasGraficPartVisible)
				{
					changeList.Add(
						$"Наличие графической части: {operationalWork.HasGraficPartVisible} -> {entity.HasGraficPartVisible}");
				}

				if (operationalWork.VetstanciaVisible != entity.VetstanciaVisible)
				{
					changeList.Add(
						$"Ветстанция: {operationalWork.VetstanciaVisible} -> {entity.VetstanciaVisible}");
				}

				if (operationalWork.DistrictsVisible != entity.DistrictsVisible)
				{
					changeList.Add(
						$"Районы: {operationalWork.DistrictsVisible} -> {entity.DistrictsVisible}");
				}

				if (operationalWork.PlanSmetaCountSkvazhinVisible != entity.PlanSmetaCountSkvazhinVisible)
				{
					changeList.Add(
						$"Плановая смета. Колич.скважин: {operationalWork.PlanSmetaCountSkvazhinVisible} -> {entity.PlanSmetaCountSkvazhinVisible}");
				}

				if (operationalWork.PlanSmetaPmVisible != entity.PlanSmetaPmVisible)
				{
					changeList.Add(
						$"Плановая смета. П.м.: {operationalWork.PlanSmetaPmVisible} -> {entity.PlanSmetaPmVisible}");
				}

				if (operationalWork.PlanSmetaCountMonolitVisible != entity.PlanSmetaCountMonolitVisible)
				{
					changeList.Add(
						$"Плановая смета. Количество монолитов: {operationalWork.PlanSmetaCountMonolitVisible} -> {entity.PlanSmetaCountMonolitVisible}");
				}

				if (operationalWork.ExecuteSmetaCountSkvazhinVisible != entity.ExecuteSmetaCountSkvazhinVisible)
				{
					changeList.Add(
						$"Исполнительная  смета. Колич.скважин: {operationalWork.ExecuteSmetaCountSkvazhinVisible} -> {entity.ExecuteSmetaCountSkvazhinVisible}");
				}

				if (operationalWork.ExecuteSmetaPmVisible != entity.ExecuteSmetaPmVisible)
				{
					changeList.Add(
						$"Исполнительная  смета. П.м.: {operationalWork.ExecuteSmetaPmVisible} -> {entity.ExecuteSmetaPmVisible}");
				}

				if (operationalWork.ExecuteSmetaCountMonolitVisible != entity.ExecuteSmetaCountMonolitVisible)
				{
					changeList.Add(
						$"Исполнительная  смета. Количество монолитов: {operationalWork.ExecuteSmetaCountMonolitVisible} -> {entity.ExecuteSmetaCountMonolitVisible}");
				}

				if (operationalWork.FromOperationalWorkId != entity.FromOperationalWorkId)
				{
					var from = (from w in _operationalWorkRepository.GetAll()
								join o in _operationalRepository.GetAll() on w.OperationalId equals o.Id
								where w.Id == operationalWork.FromOperationalWorkId
								select o.Name + w.Name).FirstOrDefault();
					var to = (from w in _operationalWorkRepository.GetAll()
							  join o in _operationalRepository.GetAll() on w.OperationalId equals o.Id
							  where w.Id == entity.FromOperationalWorkId
							  select o.Name + w.Name).FirstOrDefault();
					changeList.Add($"Взаимосвязь с работой: {from} -> {to}");
				}

				Map(entity, operationalWork);

				using (var scope = new RequiredTransactionScope())
				{
					_operationalWorkRepository.Update(operationalWork);

					_logService.Log(
						operationalWork.Id,
						"Изменение работы",
						string.Join("; ", changeList),
						operationalWork.Id);

					scope.Complete();
				}
			}
		}

		public void Delete(Guid id)
		{
			var operationalWork = _operationalWorkRepository.GetAll().FirstOrDefault(x => x.Id == id);
			if (operationalWork == null)
			{
				return;
			}

			if (operationalWork.FromOperationalWorkId.HasValue == false &&
				_operationalWorkRepository.GetAll()
					.Any(x => x.FromOperationalWorkId == id && x.DtDelete.HasValue == false))
			{
				throw new ClientException("Для удаления работы необходимо сначала удалить привязанные работы.");
			}

			using (var scope = new RequiredTransactionScope())
			{
				operationalWork.DtDelete = DateTime.Now;
				_operationalWorkRepository.Update(operationalWork);

				_logService.Log(
					operationalWork.Id,
					"Удаление работы",
					operationalWork.Name,
					operationalWork.Id);

				scope.Complete();
			}
		}
	}
}