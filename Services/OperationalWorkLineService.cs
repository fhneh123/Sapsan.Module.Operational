using Sapsan.Modules.Operational.DAL.Entity;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan.Modules.ProjectScheduler.Services.Contracts;
using Sapsan2.Contracts.Exceptions;
using Sapsan2.Core.Service.Contracts;
using Sapsan2.Core.Transactions;
using System;
using System.Linq;
using TES.Fx.Data.Ef;

namespace Sapsan.Modules.Operational.Services
{
	public class OperationalWorkLineService : IOperationalWorkLineService
	{
		private readonly IEntityRepository<OperationalWorkLine> _operationalWorkLineRepository;
		private readonly IEntityRepository<OperationalWork> _operationalWorkRepository;
		private readonly IEntityRepository<OperationalLine> _operationalLineRepository;
		private readonly ITaskService _taskService;
		private readonly ILogService _logService;
		private readonly IUserService _userService;

		public OperationalWorkLineService(
			IEntityRepository<OperationalWorkLine> operationalWorkLineRepository,
			IEntityRepository<OperationalWork> operationalWorkRepository,
			IEntityRepository<OperationalLine> operationalLineRepository,
			ITaskService taskService,
			ILogService logService,
			IUserService userService)
		{
			_operationalWorkLineRepository = operationalWorkLineRepository;
			_operationalWorkRepository = operationalWorkRepository;
			_operationalLineRepository = operationalLineRepository;
			_taskService = taskService;
			_logService = logService;
			_userService = userService;
		}

		public IQueryable<OperationalWorkLineData> GetList()
		{
			return (from x in _operationalWorkLineRepository.GetAll()
					join t in _taskService.GetList() on x.ComplianceWorkId equals t.Id into ty
					from t in ty.DefaultIfEmpty()
					join t1 in _taskService.GetList() on x.ComplianceWorkAllWorkId equals t1.Id into t1y
					from t1 in t1y.DefaultIfEmpty()
					join w in _operationalWorkRepository.GetAll() on x.OperationalWorkId equals w.Id
					where w.DtDelete.HasValue == false
					select new OperationalWorkLineData
					{
						Id = x.Id,
						OperationalWorkId = x.OperationalWorkId,
						OperationalLineId = x.OperationalLineId,
						ComplianceWorkId = x.ComplianceWorkId,
						ComplianceWorkAllWorkId = x.ComplianceWorkAllWorkId,
						DtPlanStart = x.ComplianceWorkId.HasValue ? t.StartDate : x.ComplianceWorkAllWorkId.HasValue ? t1.StartDate : x.DtPlanStart,
						DtPlanEnd = x.ComplianceWorkId.HasValue ? t.FinishDate : x.ComplianceWorkAllWorkId.HasValue ? t1.FinishDate : x.DtPlanEnd,
						DtForecastStart = x.ComplianceWorkId.HasValue ? t.StartDateForecast : x.ComplianceWorkAllWorkId.HasValue ? t1.StartDateForecast : x.DtForecastStart,
						DtForecastEnd = x.ComplianceWorkId.HasValue ? t.FinishDateForecast : x.ComplianceWorkAllWorkId.HasValue ? t1.FinishDateForecast : x.DtForecastEnd,
						DtFactStart = x.ComplianceWorkId.HasValue ? t.StartDateFact : x.ComplianceWorkAllWorkId.HasValue ? t1.StartDateFact : x.DtFactStart,
						DtFactEnd = x.ComplianceWorkId.HasValue ? t.FinishDateFact : x.ComplianceWorkAllWorkId.HasValue ? t1.FinishDateFact : x.DtFactEnd,
						ResponsibleUserId = x.ResponsibleUserId,
						Comment = x.Comment,
						Status = x.Status,
						PlanSmetaMain = x.PlanSmetaMain,
						PlanSmetaFixed = x.PlanSmetaFixed,
						PlanSmeta3DScan = x.PlanSmeta3DScan,
						PlanSmetaGeoradScan = x.PlanSmetaGeoradScan,
						PlanSmetaResponsibleUserId = x.PlanSmetaResponsibleUserId,
						PlanSmetaDtFactStart = x.PlanSmetaDtFactStart,
						PlanSmetaDtFactEnd = x.PlanSmetaDtFactEnd,
						ExecuteSmetaMain = x.ExecuteSmetaMain,
						ExecuteSmetaFixed = x.ExecuteSmetaFixed,
						ExecuteSmeta3DScan = x.ExecuteSmeta3DScan,
						ExecuteSmetaGeoradScan = x.ExecuteSmetaGeoradScan,
						ExecuteSmetaResponsibleUserId = x.ExecuteSmetaResponsibleUserId,
						ExecuteSmetaDtFactStart = x.ExecuteSmetaDtFactStart,
						ExecuteSmetaDtFactEnd = x.ExecuteSmetaDtFactEnd,
						ExecuteSmetaAdditional = x.ExecuteSmetaAdditional,
						MainSpecialistId = x.MainSpecialistId,
						DtActPassingStripSurvey = x.DtActPassingStripSurvey,
						DtActPassingRapper = x.DtActPassingRapper,
						DtTopoplan = x.DtTopoplan,
						DtLoadInParallelProjection = x.DtLoadInParallelProjection,
						DtProfilAndStatement = x.DtProfilAndStatement,
						DtTransferProfileIgiAndIgmi = x.DtTransferProfileIgiAndIgmi,
						DtUnloadingReportFromSapsan = x.DtUnloadingReportFromSapsan,
						DtNormocontrol = x.DtNormocontrol,
						Reason = x.Reason,
						HasRemark = x.HasRemark,
						DtKameralIgdiEnd = x.DtKameralIgdiEnd,
						DtForecastEndIgdi = x.DtForecastEndIgdi,
						VodotokCount = x.VodotokCount,
						CalculationsCount = x.CalculationsCount,
						DtSigning = x.DtSigning,
						SurvayField = x.SurvayField,
						SurvayTrack = x.SurvayTrack,
						TZ = x.TZ,
						DtEnd = x.DtEnd,
						Tfo = x.Tfo,
						ResponsibleUser2 = x.ResponsibleUser2,
						DtPlanCompilation = x.DtPlanCompilation,
						DtSendCompilation = x.DtSendCompilation,
						DtSend = x.DtSend,
						Fon = x.Fon,
						PHH = x.PHH,
						DtIssuePrescriptionGO = x.DtIssuePrescriptionGO,
						PGR = x.PGR,
						IGS = x.IGS,
						PchvOa = x.PchvOa,
						PchvBak = x.PchvBak,
						PchvPrz = x.PchvPrz,
						AhSH = x.AhSH,
						AhPr = x.AhPr,
						PchvZso = x.PchvZso,
						PovVod = x.PovVod,
						PodzVodIpvs = x.PodzVodIpvs,
						PodzVodIgs = x.PodzVodIgs,
						Don = x.Don,
						Av = x.Av,
						GraS = x.GraS,
						Rad = x.Rad,
						Ern = x.Ern,
						Ff = x.Ff,
						ProtocolRadiation = x.ProtocolRadiation,
						DtForecastEndKameralIgdi = x.DtForecastEndKameralIgdi,
						DtForecastKameralIgi = x.DtForecastKameralIgi,
						DtForecastCameralIgmi = x.DtForecastCameralIgmi,
						HasProtocolFieldSurvay = x.HasProtocolFieldSurvay,
						Vetstancia = x.Vetstancia,
						Districts = x.Districts,
						PlanSmetaCountSkvazhin = x.PlanSmetaCountSkvazhin,
						PlanSmetaPm = x.PlanSmetaPm,
						PlanSmetaCountMonolit = x.PlanSmetaCountMonolit,
						ExecuteSmetaCountSkvazhin = x.ExecuteSmetaCountSkvazhin,
						ExecuteSmetaPm = x.ExecuteSmetaPm,
						ExecuteSmetaCountMonolit = x.ExecuteSmetaCountMonolit
					});
		}

		public void AddAll(Guid operationalLineId)
		{
			var operationalLineData = _operationalLineRepository.GetAll().Single(x => x.Id == operationalLineId);

			var operationalWorks = _operationalWorkRepository.GetAll().Where(x => x.OperationalId == operationalLineData.OperationalId
				&& x.FromOperationalWorkId.HasValue == false).ToList();

			foreach (var operationalWork in operationalWorks)
			{
				var complianceWorkId = operationalWork.СomplianceWorkVisible ? _taskService.GetList().FirstOrDefault(x => x.WorkId == operationalLineData.ComplianceGroupId
					   && x.Name == operationalWork.Name)?.Id : null;

				Add(new OperationalWorkLineData
				{
					OperationalWorkId = operationalWork.Id,
					OperationalLineId = operationalLineId,
					ComplianceWorkId = complianceWorkId
				});
			}
		}

		public void Add(OperationalWorkLineData entity)
		{
			_operationalWorkLineRepository.Add(new OperationalWorkLine
			{
				Id = Guid.NewGuid(),
				OperationalWorkId = entity.OperationalWorkId,
				OperationalLineId = entity.OperationalLineId,
				ComplianceWorkId = entity.ComplianceWorkId
			});
		}

		public void UpdateComplianceWork(Guid operationalLineId, Guid newComplianceGroupId)
		{
			var operationalWorkLines = _operationalWorkLineRepository.GetAll().Where(x => x.OperationalLineId == operationalLineId).ToList();
			using (var scope = new RequiredTransactionScope())
			{
				foreach (var work in operationalWorkLines)
				{
					if (work.ComplianceWorkId.HasValue)
					{
						var oldName = _taskService.GetList().FirstOrDefault(x => x.Id == work.ComplianceWorkId)?.Name;
						work.ComplianceWorkId = _taskService.GetList().FirstOrDefault(x => x.WorkId == newComplianceGroupId
							&& x.Name == oldName)?.Id;
					}
					else
					{
						var workName = _operationalWorkRepository.GetAll().Single(x => x.Id == work.OperationalWorkId).Name;
						work.ComplianceWorkId = _taskService.GetList().FirstOrDefault(x => x.WorkId == newComplianceGroupId
							&& x.Name == workName)?.Id;
					}

					_operationalWorkLineRepository.Update(work);
				}

				scope.Complete();
			}
		}

		public bool CheckComplianceWorkIsAddedInAnyWorkLine(Guid operationalLineId, Guid operationalWorkId, Guid? complianceWorkId)
		{
			if(complianceWorkId == null)
			{
				return false;
			}

			if(_operationalWorkLineRepository.GetAll().Any(x => x.OperationalLineId == operationalLineId
			&& x.OperationalWorkId != operationalWorkId && x.ComplianceWorkId == complianceWorkId))
			{
				return true;
			}

			return false;
		}

		public void Update(Guid operationalLineId, Guid operationalWorkId, string columnName, object value)
		{
			var operationalWorkLineData = GetList().FirstOrDefault(x => x.OperationalLineId == operationalLineId
				&& x.OperationalWorkId == operationalWorkId);
			var operationalWorkLine = _operationalWorkLineRepository.GetAll().FirstOrDefault(x => x.OperationalLineId == operationalLineId
				&& x.OperationalWorkId == operationalWorkId);

			if (operationalWorkLine == null)
			{
				return;
			}
			var changeList = string.Empty;
			switch (columnName)
			{
				case nameof(OperationalWorkLineData.ComplianceWorkId):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Соответствует работе: {_taskService.GetList().FirstOrDefault(x => x.Id == operationalWorkLine.ComplianceWorkId)?.Name} -> " +
						$"{_taskService.GetList().FirstOrDefault(x => x.Id == (Guid?)value)?.Name}";
					operationalWorkLine.ComplianceWorkId = (Guid?)value;
					break;
				case nameof(OperationalWorkLineData.ComplianceWorkAllWorkId):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Соответствует работе из всего ПГ: {_taskService.GetList().FirstOrDefault(x => x.Id == operationalWorkLine.ComplianceWorkAllWorkId)?.Name} -> " +
						$"{_taskService.GetList().FirstOrDefault(x => x.Id == (Guid?)value)?.Name}";
					operationalWorkLine.ComplianceWorkAllWorkId = (Guid?)value;
					break;
				case nameof(OperationalWorkLineData.DtPlanStart):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Плановая дата начала: {operationalWorkLineData.DtPlanStart} -> {(DateTime?)value}";
					if (operationalWorkLine.ComplianceWorkId.HasValue || operationalWorkLine.ComplianceWorkAllWorkId.HasValue)
					{
						var task = _taskService.GetList().Single(x => x.Id == operationalWorkLine.ComplianceWorkId || x.Id == operationalWorkLine.ComplianceWorkAllWorkId);
						task.StartDate = (DateTime)value;
						_taskService.Update(task.Id, task);
					}
					else
					{
						operationalWorkLine.DtForecastStart = (DateTime?)value;
					}

					break;
				case nameof(OperationalWorkLineData.DtPlanEnd):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Плановая дата окончания: {operationalWorkLineData.DtPlanEnd} -> {(DateTime?)value}";
					if (operationalWorkLine.ComplianceWorkId.HasValue || operationalWorkLine.ComplianceWorkAllWorkId.HasValue)
					{
						var task = _taskService.GetList().Single(x => x.Id == operationalWorkLine.ComplianceWorkId || x.Id == operationalWorkLine.ComplianceWorkAllWorkId);
						task.StartDate = (DateTime)value;
						_taskService.Update(task.Id, task);
					}
					else
					{
						operationalWorkLine.DtForecastStart = (DateTime?)value;
					}

					break;
				case nameof(OperationalWorkLineData.DtForecastStart):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Прогнозная дата начала: {operationalWorkLineData.DtForecastStart} -> {(DateTime?)value}";
					if (operationalWorkLine.ComplianceWorkId.HasValue || operationalWorkLine.ComplianceWorkAllWorkId.HasValue)
					{
						var task = _taskService.GetList().Single(x => x.Id == operationalWorkLine.ComplianceWorkId || x.Id == operationalWorkLine.ComplianceWorkAllWorkId);
						task.StartDateForecast = (DateTime?)value;
						_taskService.Update(task.Id, task);
					}
					else
					{
						operationalWorkLine.DtForecastStart = (DateTime?)value;
					}

					break;
				case nameof(OperationalWorkLineData.DtForecastEnd):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Прогнозная дата окончания: {operationalWorkLineData.DtForecastEnd} -> {(DateTime?)value}";
					if (operationalWorkLine.ComplianceWorkId.HasValue || operationalWorkLine.ComplianceWorkAllWorkId.HasValue)
					{
						var task = _taskService.GetList().Single(x => x.Id == operationalWorkLine.ComplianceWorkId || x.Id == operationalWorkLine.ComplianceWorkAllWorkId);
						task.FinishDateForecast = (DateTime?)value;
						_taskService.Update(task.Id, task);
					}
					else
					{
						operationalWorkLine.DtForecastEnd = (DateTime?)value;
					}
					break;
				case nameof(OperationalWorkLineData.DtFactStart):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Фактическая дата начала: {operationalWorkLineData.DtFactStart} -> {(DateTime?)value}";
					if (operationalWorkLine.ComplianceWorkId.HasValue || operationalWorkLine.ComplianceWorkAllWorkId.HasValue)
					{
						var task = _taskService.GetList().Single(x => x.Id == operationalWorkLine.ComplianceWorkId || x.Id == operationalWorkLine.ComplianceWorkAllWorkId);
						task.StartDateFact = (DateTime?)value;
						_taskService.Update(task.Id, task);
					}
					else
					{
						operationalWorkLine.DtFactStart = (DateTime?)value;
					}
					break;
				case nameof(OperationalWorkLineData.DtFactEnd):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Фактическая дата окончания: {operationalWorkLineData.DtFactEnd} -> {(DateTime?)value}";
					if (operationalWorkLine.ComplianceWorkId.HasValue || operationalWorkLine.ComplianceWorkAllWorkId.HasValue)
					{
						var task = _taskService.GetList().Single(x => x.Id == operationalWorkLine.ComplianceWorkId || x.Id == operationalWorkLine.ComplianceWorkAllWorkId);
						task.FinishDateFact = (DateTime?)value;
						_taskService.Update(task.Id, task);
					}
					else
					{
						operationalWorkLine.DtFactEnd = (DateTime?)value;
					}
					break;
				case nameof(OperationalWorkLineData.ResponsibleUserId):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Ответственный: {_userService.GetListWithDeleted().FirstOrDefault(x => x.Id == operationalWorkLine.ResponsibleUserId)?.Fio} -> " +
						$"{_userService.GetListWithDeleted().FirstOrDefault(x => x.Id == (Guid?)value)?.Fio}";
					operationalWorkLine.ResponsibleUserId = (Guid?)value;
					break;
				case nameof(OperationalWorkLineData.Comment):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Комментарий: {operationalWorkLine.Comment} -> {(string)value}";
					operationalWorkLine.Comment = (string)value;
					break;
				case nameof(OperationalWorkLineData.Status):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Статус: {operationalWorkLine.Status} -> {(string)value}";
					operationalWorkLine.Status = (string)value;
					break;
				case nameof(OperationalWorkLineData.PlanSmetaMain):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Плановая смета.Основная: {operationalWorkLine.PlanSmetaMain} -> {value}";
					operationalWorkLine.PlanSmetaMain = GetDoubleValue((string)value);
					break;
				case nameof(OperationalWorkLineData.PlanSmetaFixed):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Плановая смета.Закрепленная: {operationalWorkLine.PlanSmetaFixed} -> {value}";
					operationalWorkLine.PlanSmetaFixed = GetDoubleValue((string)value);
					break;
				case nameof(OperationalWorkLineData.PlanSmeta3DScan):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Плановая смета.3D сканирование: {operationalWorkLine.PlanSmeta3DScan} -> {value}";
					operationalWorkLine.PlanSmeta3DScan = GetDoubleValue((string)value);
					break;
				case nameof(OperationalWorkLineData.PlanSmetaGeoradScan):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Плановая смета.Георадарное сканирование: {operationalWorkLine.PlanSmetaGeoradScan} -> {value}";
					operationalWorkLine.PlanSmetaGeoradScan = GetDoubleValue((string)value);
					break;
				case nameof(OperationalWorkLineData.PlanSmetaCountSkvazhin):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Плановая смета. Колич.скважин: {operationalWorkLine.PlanSmetaCountSkvazhin} -> {value}";
					operationalWorkLine.PlanSmetaCountSkvazhin = GetDoubleValue((string)value);
					break;
				case nameof(OperationalWorkLineData.PlanSmetaPm):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Плановая смета. П.м.: {operationalWorkLine.PlanSmetaPm} -> {value}";
					operationalWorkLine.PlanSmetaPm = GetDoubleValue((string)value);
					break;
				case nameof(OperationalWorkLineData.PlanSmetaCountMonolit):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Плановая смета. Количество монолитов: {operationalWorkLine.PlanSmetaCountMonolit} -> {value}";
					operationalWorkLine.PlanSmetaCountMonolit = GetDoubleValue((string)value);
					break;
				case nameof(OperationalWorkLineData.PlanSmetaResponsibleUserId):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Плановая смета.Ответственный: {_userService.GetListWithDeleted().FirstOrDefault(x => x.Id == operationalWorkLine.PlanSmetaResponsibleUserId)?.Fio} -> " +
						$"{_userService.GetListWithDeleted().FirstOrDefault(x => x.Id == (Guid?)value)?.Fio}";
					operationalWorkLine.PlanSmetaResponsibleUserId = (Guid?)value;
					break;
				case nameof(OperationalWorkLineData.PlanSmetaDtFactStart):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Плановая смета.Фактическая дата начала: {operationalWorkLine.PlanSmetaDtFactStart} -> {(DateTime?)value}";
					operationalWorkLine.PlanSmetaDtFactStart = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.PlanSmetaDtFactEnd):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Плановая смета.Фактическая дата окончания: {operationalWorkLine.PlanSmetaDtFactEnd} -> {(DateTime?)value}";
					operationalWorkLine.PlanSmetaDtFactEnd = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.ExecuteSmetaMain):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Исполнительная смета.Основная: {operationalWorkLine.ExecuteSmetaMain} -> {value}";
					operationalWorkLine.ExecuteSmetaMain = GetDoubleValue((string)value);
					break;
				case nameof(OperationalWorkLineData.ExecuteSmetaFixed):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Исполнительная смета. Закрепление: {operationalWorkLine.ExecuteSmetaFixed} -> {value}";
					operationalWorkLine.ExecuteSmetaFixed = GetDoubleValue((string)value);
					break;
				case nameof(OperationalWorkLineData.ExecuteSmeta3DScan):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Исполнительная смета. 3D сканирование: {operationalWorkLine.ExecuteSmeta3DScan} -> {value}";
					operationalWorkLine.ExecuteSmeta3DScan = GetDoubleValue((string)value);
					break;
				case nameof(OperationalWorkLineData.ExecuteSmetaGeoradScan):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Исполнительная смета. Георадарное сканирование: {operationalWorkLine.ExecuteSmetaGeoradScan} -> {value}";
					operationalWorkLine.ExecuteSmetaGeoradScan = GetDoubleValue((string)value);
					break;
				case nameof(OperationalWorkLineData.ExecuteSmetaCountSkvazhin):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Исполнительная  смета. Колич.скважин: {operationalWorkLine.ExecuteSmetaCountSkvazhin} -> {value}";
					operationalWorkLine.ExecuteSmetaCountSkvazhin = GetDoubleValue((string)value);
					break;
				case nameof(OperationalWorkLineData.ExecuteSmetaPm):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Исполнительная  смета. П.м.: {operationalWorkLine.ExecuteSmetaPm} -> {value}";
					operationalWorkLine.ExecuteSmetaPm = GetDoubleValue((string)value);
					break;
				case nameof(OperationalWorkLineData.ExecuteSmetaCountMonolit):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Исполнительная  смета. Количество монолитов: {operationalWorkLine.ExecuteSmetaCountMonolit} -> {value}";
					operationalWorkLine.ExecuteSmetaCountMonolit = GetDoubleValue((string)value);
					break;
				case nameof(OperationalWorkLineData.ExecuteSmetaResponsibleUserId):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Исполнительная смета.Ответственный: {_userService.GetListWithDeleted().FirstOrDefault(x => x.Id == operationalWorkLine.ExecuteSmetaResponsibleUserId)?.Fio} -> " +
						$"{_userService.GetListWithDeleted().FirstOrDefault(x => x.Id == (Guid?)value)?.Fio}";
					operationalWorkLine.ExecuteSmetaResponsibleUserId = (Guid?)value;
					break;
				case nameof(OperationalWorkLineData.ExecuteSmetaDtFactStart):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Исполнительная смета.Фактическая дата начала: {operationalWorkLine.ExecuteSmetaDtFactStart} -> {(DateTime?)value}";
					operationalWorkLine.ExecuteSmetaDtFactStart = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.ExecuteSmetaDtFactEnd):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Исполнительная смета.Фактическая дата окончания: {operationalWorkLine.ExecuteSmetaDtFactEnd} -> {(DateTime?)value}";
					operationalWorkLine.ExecuteSmetaDtFactEnd = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.ExecuteSmetaAdditional):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Исполнительная смета.Дополнительная: {operationalWorkLine.ExecuteSmetaAdditional} -> {(string)value}";
					operationalWorkLine.ExecuteSmetaAdditional = (string)value;
					break;
				case nameof(OperationalWorkLineData.MainSpecialistId):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Главный специалист: {_userService.GetListWithDeleted().FirstOrDefault(x => x.Id == operationalWorkLine.MainSpecialistId)?.Fio} -> " +
						$"{_userService.GetListWithDeleted().FirstOrDefault(x => x.Id == (Guid?)value)?.Fio}";
					operationalWorkLine.MainSpecialistId = (Guid?)value;
					break;
				case nameof(OperationalWorkLineData.DtActPassingStripSurvey):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Акт сдачи полосовой съемки: {operationalWorkLine.DtActPassingStripSurvey} -> {(DateTime?)value}";
					operationalWorkLine.DtActPassingStripSurvey = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.DtActPassingRapper):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Акт сдачи реперов: {operationalWorkLine.DtActPassingRapper} -> {(DateTime?)value}";
					operationalWorkLine.DtActPassingRapper = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.DtTopoplan):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Топопланы: {operationalWorkLine.DtTopoplan} -> {(DateTime?)value}";
					operationalWorkLine.DtTopoplan = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.DtLoadInParallelProjection):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Загрузка в параллел. проект: {operationalWorkLine.DtLoadInParallelProjection} -> {(DateTime?)value}";
					operationalWorkLine.DtLoadInParallelProjection = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.DtProfilAndStatement):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Профили и ведомости: {operationalWorkLine.DtProfilAndStatement} -> {(DateTime?)value}";
					operationalWorkLine.DtProfilAndStatement = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.DtTransferProfileIgiAndIgmi):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Передача профилей ИГИ и ИГМИ: {operationalWorkLine.DtTransferProfileIgiAndIgmi} -> {(DateTime?)value}";
					operationalWorkLine.DtTransferProfileIgiAndIgmi = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.DtUnloadingReportFromSapsan):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Выгрузка отчета в Сапсан2020: {operationalWorkLine.DtUnloadingReportFromSapsan} -> {(DateTime?)value}";
					operationalWorkLine.DtUnloadingReportFromSapsan = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.DtNormocontrol):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Нормоконтроль: {operationalWorkLine.DtNormocontrol} -> {(DateTime?)value}";
					operationalWorkLine.DtNormocontrol = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.Reason):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Причина: {operationalWorkLine.Reason} -> {(string)value}";
					operationalWorkLine.Reason = (string)value;
					break;
				case nameof(OperationalWorkLineData.HasRemark):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Наличие замечания: {operationalWorkLine.HasRemark} -> {(bool)value}";
					operationalWorkLine.HasRemark = (bool)value;
					break;
				case nameof(OperationalWorkLineData.DtKameralIgdiEnd):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Окончание камеральных ИГДИ: {operationalWorkLine.DtKameralIgdiEnd} -> {(DateTime?)value}";
					operationalWorkLine.DtKameralIgdiEnd = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.DtForecastEndIgdi):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Прогноз.срок окончания ИГДИ: {operationalWorkLine.DtForecastEndIgdi} -> {(DateTime?)value}";
					operationalWorkLine.DtForecastEndIgdi = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.VodotokCount):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Число водотоков: {operationalWorkLine.VodotokCount} -> {(string)value}";
					operationalWorkLine.VodotokCount = (string)value;
					break;
				case nameof(OperationalWorkLineData.CalculationsCount):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Количество расчетов: {operationalWorkLine.CalculationsCount} -> {(string)value}";
					operationalWorkLine.CalculationsCount = (string)value;
					break;
				case nameof(OperationalWorkLineData.DtSigning):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Дата подписания: {operationalWorkLine.DtSigning} -> {(DateTime?)value}";
					operationalWorkLine.DtSigning = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.PvoCount):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"ПВО, всего шт: { operationalWorkLine.PvoCount} -> {(string)value}";
					operationalWorkLine.PvoCount = (string)value;
					break;
				case nameof(OperationalWorkLineData.SurvayField):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Изыскания площадок, всего га: {operationalWorkLine.SurvayField} -> {(string)value}";
					operationalWorkLine.SurvayField = (string)value;
					break;
				case nameof(OperationalWorkLineData.SurvayTrack):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Изыскания трасс, всего км: { operationalWorkLine.SurvayTrack} -> {(string)value}";
					operationalWorkLine.SurvayTrack = (string)value;
					break;
				case nameof(OperationalWorkLineData.TZ):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"ТЗ: {operationalWorkLine.TZ} -> {(DateTime?)value}";
					operationalWorkLine.TZ = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.DtEnd):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Дата окончания: {operationalWorkLine.DtEnd} -> {(DateTime?)value}";
					operationalWorkLine.DtEnd = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.Tfo):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"ТФО: {operationalWorkLine.Tfo} -> {(string)value}";
					operationalWorkLine.Tfo = (string)value;
					break;
				case nameof(OperationalWorkLineData.ResponsibleUser2):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Ответственный2: {_userService.GetListWithDeleted().FirstOrDefault(x => x.Id == operationalWorkLine.ResponsibleUser2)?.Fio}" +
						$" -> {_userService.GetListWithDeleted().FirstOrDefault(x => x.Id == (Guid?)value)?.Fio}";
					operationalWorkLine.ResponsibleUser2 = (Guid?)value;
					break;
				case nameof(OperationalWorkLineData.DtPlanCompilation):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Плановая дата составления: {operationalWorkLine.DtPlanCompilation} -> {(DateTime?)value}";
					operationalWorkLine.DtPlanCompilation = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.DtSendCompilation):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Дата отправки на согласование: {operationalWorkLine.DtSendCompilation} -> {(DateTime?)value}";
					operationalWorkLine.DtSendCompilation = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.DtSend):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Дата отправки: {operationalWorkLine.DtSend} -> {(DateTime?)value}";
					operationalWorkLine.DtSend = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.Fon):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"ФОН: {operationalWorkLine.Fon} -> {(string)value}";
					operationalWorkLine.Fon = (string)value;
					break;
				case nameof(OperationalWorkLineData.PHH):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"РХХ: {operationalWorkLine.PHH} -> {(string)value}";
					operationalWorkLine.PHH = (string)value;
					break;
				case nameof(OperationalWorkLineData.DtIssuePrescriptionGO):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Предписание ГО дата выдачи: {operationalWorkLine.DtIssuePrescriptionGO} -> {(DateTime?)value}";
					operationalWorkLine.DtIssuePrescriptionGO = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.PGR):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"ПГР (ИГИ): {operationalWorkLine.PGR} -> {(string)value}";
					operationalWorkLine.PGR = (string)value;
					break;
				case nameof(OperationalWorkLineData.IGS):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"ИГС (ИГИ): {operationalWorkLine.IGS} -> {(string)value}";
					operationalWorkLine.IGS = (string)value;
					break;
				case nameof(OperationalWorkLineData.PchvOa):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"ПЧВ ОА: {operationalWorkLine.PchvOa} -> {(string)value}";
					operationalWorkLine.PchvOa = (string)value;
					break;
				case nameof(OperationalWorkLineData.PchvBak):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"ПЧВ БАК: {operationalWorkLine.PchvBak} -> {(string)value}";
					operationalWorkLine.PchvBak = (string)value;
					break;
				case nameof(OperationalWorkLineData.PchvPrz):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"ПЧВ ПРЗ: {operationalWorkLine.PchvPrz} -> {(string)value}";
					operationalWorkLine.PchvPrz = (string)value;
					break;
				case nameof(OperationalWorkLineData.AhSH):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"АХ Ш: {operationalWorkLine.AhSH} -> {(string)value}";
					operationalWorkLine.AhSH = (string)value;
					break;
				case nameof(OperationalWorkLineData.AhPr):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"АХ ПР: {operationalWorkLine.AhPr} -> {(string)value}";
					operationalWorkLine.AhPr = (string)value;
					break;
				case nameof(OperationalWorkLineData.PchvZso):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"ПЧВ ЗСО: {operationalWorkLine.PchvZso} -> {(string)value}";
					operationalWorkLine.PchvZso = (string)value;
					break;
				case nameof(OperationalWorkLineData.PovVod):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"ПОВ.ВОД: {operationalWorkLine.PovVod} -> {(string)value}";
					operationalWorkLine.PovVod = (string)value;
					break;
				case nameof(OperationalWorkLineData.PodzVodIpvs):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"ПОДЗ.ВОД ИПВС: {operationalWorkLine.PodzVodIpvs} -> {(string)value}";
					operationalWorkLine.PodzVodIpvs = (string)value;
					break;
				case nameof(OperationalWorkLineData.PodzVodIgs):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"ПОДЗ.ВОД ИГС: {operationalWorkLine.PodzVodIgs} -> {(string)value}";
					operationalWorkLine.PodzVodIgs = (string)value;
					break;
				case nameof(OperationalWorkLineData.Don):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"ДОН: {operationalWorkLine.Don} -> {(string)value}";
					operationalWorkLine.Don = (string)value;
					break;
				case nameof(OperationalWorkLineData.Av):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"АВ: {operationalWorkLine.Av} -> {(string)value}";
					operationalWorkLine.Av = (string)value;
					break;
				case nameof(OperationalWorkLineData.GraS):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"ГраС: {operationalWorkLine.GraS} -> {(string)value}";
					operationalWorkLine.GraS = (string)value;
					break;
				case nameof(OperationalWorkLineData.Rad):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"РАД: {operationalWorkLine.Rad} -> {(string)value}";
					operationalWorkLine.Rad = (string)value;
					break;
				case nameof(OperationalWorkLineData.Ern):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"ЕРН: {operationalWorkLine.Ern} -> {(string)value}";
					operationalWorkLine.Ern = (string)value;
					break;
				case nameof(OperationalWorkLineData.Ff):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"ФФ: {operationalWorkLine.Ff} -> {(string)value}";
					operationalWorkLine.Ff = (string)value;
					break;
				case nameof(OperationalWorkLineData.ProtocolRadiation):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Протокол радиации: {operationalWorkLine.ProtocolRadiation} -> {(string)value}";
					operationalWorkLine.ProtocolRadiation = (string)value;
					break;
				case nameof(OperationalWorkLineData.DtForecastEndKameralIgdi):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Прогнозная дата окончания камеральных работ ИГДИ : {operationalWorkLine.DtForecastEndKameralIgdi} -> {(DateTime?)value}";
					operationalWorkLine.DtForecastEndKameralIgdi = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.DtForecastKameralIgi):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Прогнозная дата окончания камеральных работ ИГИ  : {operationalWorkLine.DtForecastKameralIgi} -> {(DateTime?)value}";
					operationalWorkLine.DtForecastKameralIgi = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.DtForecastCameralIgmi):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Прогнозная дата окончания камеральных работ ИГМИ : {operationalWorkLine.DtForecastCameralIgmi} -> {(DateTime?)value}";
					operationalWorkLine.DtForecastCameralIgmi = (DateTime?)value;
					break;
				case nameof(OperationalWorkLineData.HasProtocolFieldSurvay):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Наличие протоколов полевого обследования: {operationalWorkLine.HasProtocolFieldSurvay} -> {(string)value}";
					operationalWorkLine.HasProtocolFieldSurvay = (string)value;
					break;
				case nameof(OperationalWorkLineData.HasGraficPart):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Наличие графической части: {operationalWorkLine.HasGraficPart} -> {(string)value}";
					operationalWorkLine.HasGraficPart = (string)value;
					break;
				case nameof(OperationalWorkLineData.Vetstancia):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Ветстанция: {operationalWorkLine.Vetstancia} -> {(string)value}";
					operationalWorkLine.Vetstancia = (string)value;
					break;
				case nameof(OperationalWorkLineData.Districts):
					changeList = $"Работа:{_operationalWorkRepository.GetAll().Single(x => x.Id == operationalWorkLine.OperationalWorkId)?.Name}. " +
						$"Районы: {operationalWorkLine.Districts} -> {(string)value}";
					operationalWorkLine.Districts = (string)value;
					break;
			}

			using (var scope = new RequiredTransactionScope())
			{
				_operationalWorkLineRepository.Update(operationalWorkLine);

				_logService.Log(
					operationalWorkLine.OperationalLineId,
					"Изменение работы из строки оперативки",
					changeList,
					operationalWorkLine.Id);

				scope.Complete();
			}
		}

		private double? GetDoubleValue(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}

			if (!double.TryParse(value, out var result))
			{
				throw new ClientException($"Не удалось преобразовать введенное значение в число с плавающей запятой!");
			}

			return result;
		}
	}
}
