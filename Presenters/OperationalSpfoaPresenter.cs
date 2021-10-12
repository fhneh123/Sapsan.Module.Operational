using System;
using System.Collections.Generic;
using System.Linq;
using Sapsan.Modules.Operational.Enums;
using Sapsan.Modules.Operational.Presenters.Contracts;
using Sapsan.Modules.Operational.Presenters.Data;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan.Modules.ProjectScheduler.Services;
using Sapsan2.Contracts.Enums;
using Sapsan2.Core.Service.Contracts;

namespace Sapsan.Modules.Operational.Presenters
{
	public class OperationalSpfoaPresenter : IOperationalSpfoaPresenter
	{
		private readonly IOperationalSpfoaService _operationalSpfoaService;
		private readonly IContractService _contractService;
		private readonly IUserService _userService;
		private readonly IOrganizationService _organizationService;
		private readonly PlanService _planService;
		private readonly ISubdivisionService _subdivisionService;
		private readonly IOperationalEntityStatusService _operationalEntityStatusService;
		private readonly IOperationalLineService _operationalLineService;
		private readonly IOperationalWorkService _operationalWorkService;
		private readonly IOperationalWorkLineService _operationalWorkLineService;
		private readonly IOperationalSpfoaEditorPresenter _operationalSpfoaEditorPresenter;
		private readonly TaskService _taskService;
		private readonly IOperationalTypeTaskService _operationalTypeTaskService;

		public OperationalSpfoaPresenter(
			IOperationalSpfoaService operationalSpfoaService,
			IContractService contractService,
			IUserService userService,
			IOrganizationService organizationService,
			PlanService planService,
			ISubdivisionService subdivisionService,
			IOperationalLineService operationalLineService,
			IOperationalWorkService operationalWorkService,
			IOperationalWorkLineService operationalWorkLineService,
			IOperationalEntityStatusService operationalEntityStatusService,
			IOperationalSpfoaEditorPresenter operationalSpfoaEditorPresenter,
			TaskService taskService,
			IOperationalTypeTaskService operationalTypeTaskService)
		{
			_operationalSpfoaService = operationalSpfoaService;
			_contractService = contractService;
			_userService = userService;
			_organizationService = organizationService;
			_planService = planService;
			_subdivisionService = subdivisionService;
			_operationalLineService = operationalLineService;
			_operationalWorkService = operationalWorkService;
			_operationalWorkLineService = operationalWorkLineService;
			_operationalEntityStatusService = operationalEntityStatusService;
			_operationalSpfoaEditorPresenter = operationalSpfoaEditorPresenter;
			_taskService = taskService;
			_operationalTypeTaskService = operationalTypeTaskService;
		}

		public List<OperationalSpfoaPresenterData> GetData()
		{
			var operationalSpfoaPresenterDatas = (from o in _operationalSpfoaService.GetList()
												  join c in _contractService.GetList() on o.ContractId equals c.Id
												  join g in _userService.GetList() on c.GipId equals g.Id into gy
												  from g in gy.DefaultIfEmpty()
												  join cu in _organizationService.GetList() on c.OperatorId equals cu.Id into cuy
												  from cu in cuy.DefaultIfEmpty()
												  join p in _planService.GetList() on o.PlanId equals p.Id
												  join s in _subdivisionService.GetList() on o.SubdivisionId equals s.Id
												  join st in _operationalEntityStatusService.GetList() on o.ExecuteStatusId equals st.Id into sty
												  from st in sty.DefaultIfEmpty()
												  join t in _operationalTypeTaskService.GetList() on o.TypeTaskId equals t.Id
												  orderby o.DtCreate
												  select new OperationalSpfoaPresenterData
												  {
													  Id = o.Id,
													  ContractId = c.Id,
													  ContractShifr = c.Shifr,
													  Gip = g.Fio,
													  ContractName = c.Subject,
													  CustomerName = cu.Name,
													  PlanId = p.Id,
													  PlanName = p.Name,
													  PlanIsMain = p.IsMain,
													  StageNumber = o.StageNumber,
													  TypeTaskId = o.TypeTaskId,
													  TypeTask = t.Name,
													  SubdivisionId = o.SubdivisionId,
													  SubdivisionShortName = s.ShortName,
													  ExecutorTypeId = c.ExecutorType,
													  ExecuteStatusId = o.ExecuteStatusId,
													  RemarkKoii = o.RemarkKoii,
													  DtActirovania = o.DtActirovania,
													  DtCreate = o.DtCreate,
													  DtDelete = o.DtDelete
												  }).ToList();

			var operationalLineDatas = _operationalLineService.GetList().ToArray();
			var operationalWorkDatas = _operationalWorkService.GetList().ToArray();
			var operationalWorkLineDatas = _operationalWorkLineService.GetList().ToArray();
			var spfoaEditorDatas = _operationalSpfoaEditorPresenter.GetData().ToArray();

			foreach (var item in operationalSpfoaPresenterDatas)
			{
				item.ExecutorType = item.ExecutorTypeId.HasValue
					? ContractExecutorTypeEnum.FindType((ContractExecutorType)item.ExecutorTypeId).Name
					: string.Empty;

				var operationalLines = operationalLineDatas.Where(x => x.ContractId == item.ContractId &&
																	   x.PlanId == item.PlanId
																	   && x.TypeTaskId == item.TypeTaskId &&
																	   x.SubdivisionId == item.SubdivisionId).ToArray();

				var igdi = operationalLines.FirstOrDefault(x => x.OperationalId == OperationalEnum.Igdi);
				var igmi = operationalLines.FirstOrDefault(x => x.OperationalId == OperationalEnum.Igmi);
				var igi = operationalLines.FirstOrDefault(x => x.OperationalId == OperationalEnum.Igi);
				var iei = operationalLines.FirstOrDefault(x => x.OperationalId == OperationalEnum.Iei);

				var workSmets = (from w in operationalWorkDatas
								 join wl in operationalWorkLineDatas on w.Id equals wl.OperationalWorkId
								 join o in operationalLineDatas on wl.OperationalLineId equals o.Id
								 where w.ExecuteSmetaMainVisible &&
									   (o.Id == igdi?.Id || o.Id == igmi?.Id || o.Id == igi?.Id || o.Id == iei?.Id)
								 select new
								 {
									 o.Id,
									 wl.ExecuteSmetaMain,
									 wl.ExecuteSmetaFixed,
									 wl.ExecuteSmeta3DScan,
									 wl.ExecuteSmetaGeoradScan,
									 wl.PlanSmetaMain,
									 wl.PlanSmetaFixed,
									 wl.PlanSmeta3DScan,
									 wl.PlanSmetaGeoradScan
								 }).ToArray();

				if (igdi != null && workSmets.Any(x => x.Id == igdi.Id))
				{
					var work = workSmets.First(x => x.Id == igdi.Id);

					item.ExecutePriceIgdiMain = work.ExecuteSmetaMain;
					item.ExecutePriceIgdiFixed = work.ExecuteSmetaFixed;
					item.ExecutePriceIgdi3DScan = work.ExecuteSmeta3DScan;
					item.PlanPriceIgdiFixed = work.PlanSmetaMain;
					item.PlanPriceIgdiMain = work.PlanSmetaFixed;
					item.PlanPriceIgdi3DScan = work.PlanSmeta3DScan;
				}

				if (igmi != null && workSmets.Any(x => x.Id == igmi.Id))
				{
					var work = workSmets.First(x => x.Id == igmi.Id);

					item.ExecutePriceIgmi = work.ExecuteSmetaMain;
					item.PlanPriceIgmi = work.PlanSmetaMain;
				}

				if (igi != null && workSmets.Any(x => x.Id == igi.Id))
				{
					var work = workSmets.First(x => x.Id == igi.Id);

					item.ExecutePriceIgi = work.ExecuteSmetaMain;
					item.ExecutePriceIgiGeoradScan = work.ExecuteSmetaGeoradScan;
					item.PlanPriceIgi = work.PlanSmetaMain;
					item.PlanPriceIgiGeoradScan = work.PlanSmetaGeoradScan;
				}

				if (iei != null && workSmets.Any(x => x.Id == iei.Id))
				{
					var work = workSmets.First(x => x.Id == iei.Id);

					item.ExecutePriceIei = work.ExecuteSmetaMain;
					item.PlanPriceIei = work.PlanSmetaMain;
				}

				item.PlanPriceResult = (item.PlanPriceIgdiFixed ?? 0) + (item.PlanPriceIgdiMain ?? 0) +
									   (item.PlanPriceIgdi3DScan ?? 0) + (item.PlanPriceIgmi ?? 0)
									   + (item.PlanPriceIgi ?? 0) + (item.PlanPriceIgiGeoradScan ?? 0) +
									   (item.PlanPriceIei ?? 0);

				item.ExecutePriceResult = (item.ExecutePriceIgdiMain ?? 0) + (item.ExecutePriceIgdiFixed ?? 0) +
										  (item.ExecutePriceIgdi3DScan ?? 0)
										  + (item.ExecutePriceIgmi ?? 0) + (item.ExecutePriceIgi ?? 0) +
										  (item.ExecutePriceIgiGeoradScan ?? 0) + (item.ExecutePriceIei ?? 0);

				item.Economy = item.PlanPriceResult - item.ExecutePriceResult;

				var statusKoiiOrderNum = -1;

				if (igdi != null)
				{
					GetPgDate(spfoaEditorDatas, operationalWorkLineDatas, igdi.Id, OperationalEnum.Igdi,
						out OperationalEntityStatusData spfoStatus, out DateTime? dtRelease);
					if (spfoStatus != null)
					{
						item.StatusIgdi = spfoStatus.Name;
						item.StatusIgdiId = spfoStatus.Id;
						item.DtReleaseIgdi = dtRelease;

						if (spfoStatus.OrderNum > statusKoiiOrderNum)
						{
							item.StatusKoii = spfoStatus.Name;
							item.StatusKoiiId = spfoStatus.Id;
							item.DtSummariKoii = dtRelease;
							statusKoiiOrderNum = spfoStatus.OrderNum;
						}
					}
				}

				if (igmi != null)
				{
					GetPgDate(spfoaEditorDatas, operationalWorkLineDatas, igmi.Id, OperationalEnum.Igmi,
						out OperationalEntityStatusData spfoStatus, out DateTime? dtRelease);
					if (spfoStatus != null)
					{
						item.StatusIgmi = spfoStatus.Name;
						item.StatusIgmiId = spfoStatus.Id;
						item.DtReleaseIgmi = dtRelease;

						if (spfoStatus.OrderNum > statusKoiiOrderNum)
						{
							item.StatusKoii = spfoStatus.Name;
							item.StatusKoiiId = spfoStatus.Id;
							item.DtSummariKoii = dtRelease;
							statusKoiiOrderNum = spfoStatus.OrderNum;
						}
					}
				}

				if (igi != null)
				{
					GetPgDate(spfoaEditorDatas, operationalWorkLineDatas, igi.Id, OperationalEnum.Igi,
						out OperationalEntityStatusData spfoStatus, out DateTime? dtRelease);
					if (spfoStatus != null)
					{
						item.StatusIgi = spfoStatus.Name;
						item.StatusIgiId = spfoStatus.Id;
						item.DtReleaseIgi = dtRelease;

						if (spfoStatus.OrderNum > statusKoiiOrderNum)
						{
							item.StatusKoii = spfoStatus.Name;
							item.StatusKoiiId = spfoStatus.Id;
							item.DtSummariKoii = dtRelease;
							statusKoiiOrderNum = spfoStatus.OrderNum;
						}
					}
				}

				if (iei != null)
				{
					GetPgDate(spfoaEditorDatas, operationalWorkLineDatas, iei.Id, OperationalEnum.Iei,
						out OperationalEntityStatusData spfoStatus, out DateTime? dtRelease);
					if (spfoStatus != null)
					{
						item.StatusIei = spfoStatus.Name;
						item.StatusIeiId = spfoStatus.Id;
						item.DtReleaseIei = dtRelease;

						if (spfoStatus.OrderNum > statusKoiiOrderNum)
						{
							item.StatusKoii = spfoStatus.Name;
							item.StatusKoiiId = spfoStatus.Id;
							item.DtSummariKoii = dtRelease;
							statusKoiiOrderNum = spfoStatus.OrderNum;
						}
					}
				}
			}

			return operationalSpfoaPresenterDatas;
		}

		private void GetPgDate(OperationalSpfoaEditorPresenterData[] spfoaEditorDatas,
			OperationalWorkLineData[] operationalWorkLineDatas, Guid operationalLineId, Guid operationalId,
			out OperationalEntityStatusData spfoStatus, out DateTime? dtRelease)
		{
			spfoStatus = null;
			dtRelease = null;

			foreach (var spfoaEditor in spfoaEditorDatas.Where(x => x.OperatioalId == operationalId)
				.OrderByDescending(x => x.OrderNum))
			{
				var workLine = operationalWorkLineDatas.Single(x =>
					x.OperationalLineId == operationalLineId && x.OperationalWorkId == spfoaEditor.OperatioalWorkId);
				if (spfoaEditor.OperationalStatusId.HasValue)
				{
					if (workLine.Status != spfoaEditor.OperationalStatusName)
					{
						continue;
					}

					spfoStatus = _operationalEntityStatusService.GetList()
						.Single(x => x.Id == spfoaEditor.SpfoaStatusId);
					dtRelease = workLine.ComplianceWorkId.HasValue
						? GetWorkData(spfoaEditor.OperationalDateTypeId, workLine.ComplianceWorkId.Value)
						: GetOperationalWorkLineData(spfoaEditor.OperationalDateTypeId, workLine);
					return;
				}

				var date = workLine.ComplianceWorkId.HasValue
					? GetWorkData(spfoaEditor.OperationalDateTypeId, workLine.ComplianceWorkId.Value)
					: GetOperationalWorkLineData(spfoaEditor.OperationalDateTypeId, workLine);
				if (date == null)
				{
					continue;
				}

				spfoStatus = _operationalEntityStatusService.GetList().Single(x => x.Id == spfoaEditor.SpfoaStatusId);
				dtRelease = date;

				return;
			}
		}

		private DateTime? GetWorkData(int dateType, Guid workId)
		{
			switch (dateType)
			{
				case (int)OperationalSpfoaDateTypeEnum.ПрогнозНачала:
					return _taskService.GetList().SingleOrDefault(x => x.Id == workId)?.StartDateForecast;
				case (int)OperationalSpfoaDateTypeEnum.ПрогнозОкнчания:
					return _taskService.GetList().SingleOrDefault(x => x.Id == workId)?.FinishDateForecast;
				case (int)OperationalSpfoaDateTypeEnum.ФактНачала:
					return _taskService.GetList().SingleOrDefault(x => x.Id == workId)?.StartDateFact;
				case (int)OperationalSpfoaDateTypeEnum.ФактОкончания:
					return _taskService.GetList().SingleOrDefault(x => x.Id == workId)?.FinishDateFact;
			}

			return null;
		}

		private DateTime? GetOperationalWorkLineData(int dateType, OperationalWorkLineData workLineData)
		{
			switch (dateType)
			{
				case (int)OperationalSpfoaDateTypeEnum.ПрогнозНачала:
					return workLineData.DtForecastStart;
				case (int)OperationalSpfoaDateTypeEnum.ПрогнозОкнчания:
					return workLineData.DtForecastEnd;
				case (int)OperationalSpfoaDateTypeEnum.ФактНачала:
					return workLineData.DtFactStart;
				case (int)OperationalSpfoaDateTypeEnum.ФактОкончания:
					return workLineData.DtFactEnd;
			}

			return null;
		}
	}
}