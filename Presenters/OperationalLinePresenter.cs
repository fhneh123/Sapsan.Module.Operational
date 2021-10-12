using Sapsan.Modules.Operational.Presenters.Contracts;
using Sapsan.Modules.Operational.Presenters.Data;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan.Modules.ProjectScheduler.Services;
using Sapsan2.Core.Contracts;
using Sapsan2.Core.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sapsan.Modules.Operational.Presenters
{
	public class OperationalLinePresenter : IOperationalLinePresenter
	{
		private readonly IOperationalLineService _operationalLineService;
		private readonly IOperationalWorkLineService _operationalWorkLineService;
		private readonly IOperationalWorkService _operationalWorkService;
		private readonly IOrganizationService _organizationService;
		private readonly IContractService _contractService;
		private readonly IDesignBasicService _designBasicService;
		private readonly PlanService _planService;
		private readonly WorkStructureService _workStructureService;
		private readonly IUserService _userService;
		private readonly ISubdivisionService _subdivisionService;
		private readonly IOperationalTypeTaskService _operationalTypeTaskService;

		public OperationalLinePresenter(
			IOperationalLineService operationalLineService,
			IOperationalWorkLineService operationalWorkLineService,
			IOperationalWorkService operationalWorkService,
			IOrganizationService organizationService,
			IContractService contractService,
			IDesignBasicService designBasicService,
			PlanService planService,
			WorkStructureService workStructureService,
			IUserService userService,
			ISubdivisionService subdivisionService,
			IOperationalTypeTaskService operationalTypeTaskService)
		{
			_operationalLineService = operationalLineService;
			_operationalWorkLineService = operationalWorkLineService;
			_operationalWorkService = operationalWorkService;
			_organizationService = organizationService;
			_contractService = contractService;
			_designBasicService = designBasicService;
			_planService = planService;
			_workStructureService = workStructureService;
			_userService = userService;
			_subdivisionService = subdivisionService;
			_operationalTypeTaskService = operationalTypeTaskService;
		}

		public List<OperationalLinePresenterData> GetData(Guid operationalId, bool withAnnul, Guid? operationalLineId = null)
		{
			var operationalLines = (from x in _operationalLineService.GetList()
									join c in _contractService.GetList() on x.ContractId equals c.Id
									join u in _userService.GetListWithDeleted() on c.GipId equals u.Id into uy
									from u in uy.DefaultIfEmpty()
									join o in _organizationService.GetList() on c.OperatorId equals o.Id into yc
									from o in yc.DefaultIfEmpty()
									join p in _planService.GetList() on x.PlanId equals p.Id
									join w in _workStructureService.GetList() on x.ComplianceGroupId equals w.Id
									join s in _subdivisionService.GetList() on x.SubdivisionId equals s.Id
									join t in _operationalTypeTaskService.GetList() on x.TypeTaskId equals t.Id
									where x.OperationalId == operationalId
									orderby x.DtCreate
									select new OperationalLinePresenterData
									{
										Id = x.Id,
										OperationalId = x.OperationalId,
										ContractId = x.ContractId,
										PlanId = x.PlanId,
										PlanName = p.Name,
										PlanIsMain = p.IsMain,
										Priority = x.Priority,
										CustomerId = c.OperatorId,
										CustomerName = o.Name,
										Shifr = c.Shifr,
										Name = c.Subject,
										Gip = u.Fio,
										ComplianceGroupId = x.ComplianceGroupId,
										ComplianceGroupName = w.Name,
										StageNumber = w.StageNumber,
										DtPlanStart = w.StartDate,
										DtPlanEnd = w.FinishDate,
										DtForecastStart = w.StartDateForecast,
										DtForecastEnd = w.FinishDateForecast,
										DtFactStart = w.StartDateFact,
										DtFactEnd = w.FinishDateFact,
										TaskTypeId = t.Id,
										TaskType = t.Name,
										SubdivisionShortName = s.ShortName,
										SubdivisionId = s.Id,
										DtCreate = x.DtCreate,
										DtDelete = x.DtDelete
									});

			if (withAnnul == false)
			{
				operationalLines = operationalLines.Where(x => x.DtDelete.HasValue == false);
			}

			if (operationalLineId.HasValue)
			{
				operationalLines = operationalLines.Where(x => x.Id == operationalLineId);
			}

			return operationalLines.ToList();
		}

		public List<OperationalWorkLineData> GetWorkLines(Guid operationalId)
		{
			var operationLineIds = _operationalLineService.GetList().Where(x => x.OperationalId == operationalId).Select(x => x.Id).ToList();

			var operationalWorkLineDatas = _operationalWorkLineService.GetList().Where(x => operationLineIds.Contains(x.OperationalLineId)).ToList();

			var relationshipWorkLneDatas = GetRelationshipWorkLines(operationalId);

			operationalWorkLineDatas.AddRange(relationshipWorkLneDatas);

			return operationalWorkLineDatas;
		}

		private List<OperationalWorkLineData> GetRelationshipWorkLines(Guid operationalId)
		{
			var operationalRelationshipWorkLineData = new List<OperationalWorkLineData>();

			var fromWorkDatas = _operationalWorkService.GetList().Where(x => x.OperationalId == operationalId && x.FromOperationalWorkId.HasValue).ToList();

			var operationLineDatas = (from o in _operationalLineService.GetList()
									  join w in _workStructureService.GetList() on o.ComplianceGroupId equals w.Id
									  where o.OperationalId == operationalId && o.DtDelete.HasValue == false
									  select new
									  {
										  o,
										  o.Id,
										  o.ContractId,
										  o.PlanId,
										  o.TypeTaskId,
										  o.SubdivisionId,
										  w.StageNumber
									  }).ToList();

			var workDatas = (from wl in _operationalWorkLineService.GetList()
							join l in _operationalLineService.GetList() on wl.OperationalLineId equals l.Id
							join w in _workStructureService.GetList() on l.ComplianceGroupId equals w.Id
							where l.DtDelete.HasValue == false
							select new
							{
								wl.OperationalWorkId,
								l.ContractId,
								l.PlanId,
								l.TypeTaskId,
								l.SubdivisionId,
								w.StageNumber,
								wl
							}).ToList();

			foreach (var fromWorkData in fromWorkDatas)
			{
				foreach (var operationLineData in operationLineDatas)
				{
					var workData = workDatas.Where(x => x.OperationalWorkId == fromWorkData.FromOperationalWorkId && x.ContractId == operationLineData.ContractId
									&& x.PlanId == operationLineData.PlanId && x.TypeTaskId == operationLineData.TypeTaskId
									&& x.SubdivisionId == operationLineData.SubdivisionId && x.StageNumber == operationLineData.StageNumber).Select(x => x.wl).SingleOrDefault();

					if (workData != null)
					{
						workData.OperationalLineId = operationLineData.Id;
						workData.OperationalWorkId = fromWorkData.Id;
						operationalRelationshipWorkLineData.Add(workData);
					}
				}
			}

			return operationalRelationshipWorkLineData;
		}

		public object Get(string name, OperationalWorkLineData workLineData)
		{
			switch (name)
			{
				case nameof(OperationalWorkLineData.ComplianceWorkId):
					return workLineData.ComplianceWorkId;
				case nameof(OperationalWorkLineData.ComplianceWorkAllWorkId):
					return workLineData.ComplianceWorkAllWorkId;
				case nameof(OperationalWorkLineData.DtPlanStart):
					return workLineData.DtPlanStart;
				case nameof(OperationalWorkLineData.DtPlanEnd):
					return workLineData.DtPlanEnd;
				case nameof(OperationalWorkLineData.DtForecastStart):
					return workLineData.DtForecastStart;
				case nameof(OperationalWorkLineData.DtForecastEnd):
					return workLineData.DtForecastEnd;
				case nameof(OperationalWorkLineData.DtFactStart):
					return workLineData.DtFactStart;
				case nameof(OperationalWorkLineData.DtFactEnd):
					return workLineData.DtFactEnd;
				case nameof(OperationalWorkLineData.ResponsibleUserId):
					return workLineData.ResponsibleUserId;
				case nameof(OperationalWorkLineData.Comment):
					return workLineData.Comment;
				case nameof(OperationalWorkLineData.Status):
					return workLineData.Status;
				case nameof(OperationalWorkLineData.PlanSmetaMain):
					return workLineData.PlanSmetaMain;
				case nameof(OperationalWorkLineData.PlanSmetaFixed):
					return workLineData.PlanSmetaFixed;
				case nameof(OperationalWorkLineData.PlanSmeta3DScan):
					return workLineData.PlanSmeta3DScan;
				case nameof(OperationalWorkLineData.PlanSmetaGeoradScan):
					return workLineData.PlanSmetaGeoradScan;
				case nameof(OperationalWorkLineData.PlanSmetaResponsibleUserId):
					return workLineData.PlanSmetaResponsibleUserId;
				case nameof(OperationalWorkLineData.PlanSmetaDtFactStart):
					return workLineData.PlanSmetaDtFactStart;
				case nameof(OperationalWorkLineData.PlanSmetaDtFactEnd):
					return workLineData.PlanSmetaDtFactEnd;
				case nameof(OperationalWorkLineData.ExecuteSmetaMain):
					return workLineData.ExecuteSmetaMain;
				case nameof(OperationalWorkLineData.ExecuteSmetaFixed):
					return workLineData.ExecuteSmetaFixed;
				case nameof(OperationalWorkLineData.ExecuteSmeta3DScan):
					return workLineData.ExecuteSmeta3DScan;
				case nameof(OperationalWorkLineData.ExecuteSmetaGeoradScan):
					return workLineData.ExecuteSmetaGeoradScan;
				case nameof(OperationalWorkLineData.ExecuteSmetaResponsibleUserId):
					return workLineData.ExecuteSmetaResponsibleUserId;
				case nameof(OperationalWorkLineData.ExecuteSmetaDtFactStart):
					return workLineData.ExecuteSmetaDtFactStart;
				case nameof(OperationalWorkLineData.ExecuteSmetaDtFactEnd):
					return workLineData.ExecuteSmetaDtFactEnd;
				case nameof(OperationalWorkLineData.ExecuteSmetaAdditional):
					return workLineData.ExecuteSmetaAdditional;
				case nameof(OperationalWorkLineData.MainSpecialistId):
					return workLineData.MainSpecialistId;
				case nameof(OperationalWorkLineData.DtActPassingStripSurvey):
					return workLineData.DtActPassingStripSurvey;
				case nameof(OperationalWorkLineData.DtActPassingRapper):
					return workLineData.DtActPassingRapper;
				case nameof(OperationalWorkLineData.DtTopoplan):
					return workLineData.DtTopoplan;
				case nameof(OperationalWorkLineData.DtLoadInParallelProjection):
					return workLineData.DtLoadInParallelProjection;
				case nameof(OperationalWorkLineData.DtProfilAndStatement):
					return workLineData.DtProfilAndStatement;
				case nameof(OperationalWorkLineData.DtTransferProfileIgiAndIgmi):
					return workLineData.DtTransferProfileIgiAndIgmi;
				case nameof(OperationalWorkLineData.DtUnloadingReportFromSapsan):
					return workLineData.DtUnloadingReportFromSapsan;
				case nameof(OperationalWorkLineData.DtNormocontrol):
					return workLineData.DtNormocontrol;
				case nameof(OperationalWorkLineData.Reason):
					return workLineData.Reason;
				case nameof(OperationalWorkLineData.HasRemark):
					return workLineData.HasRemark;
				case nameof(OperationalWorkLineData.DtKameralIgdiEnd):
					return workLineData.DtKameralIgdiEnd;
				case nameof(OperationalWorkLineData.DtForecastEndIgdi):
					return workLineData.DtForecastEndIgdi;
				case nameof(OperationalWorkLineData.VodotokCount):
					return workLineData.VodotokCount;
				case nameof(OperationalWorkLineData.CalculationsCount):
					return workLineData.CalculationsCount;
				case nameof(OperationalWorkLineData.DtSigning):
					return workLineData.DtSigning;
				case nameof(OperationalWorkLineData.PvoCount):
					return workLineData.PvoCount;
				case nameof(OperationalWorkLineData.SurvayField):
					return workLineData.SurvayField;
				case nameof(OperationalWorkLineData.SurvayTrack):
					return workLineData.SurvayTrack;
				case nameof(OperationalWorkLineData.TZ):
					return workLineData.TZ;
				case nameof(OperationalWorkLineData.DtEnd):
					return workLineData.DtEnd;
				case nameof(OperationalWorkLineData.Tfo):
					return workLineData.Tfo;
				case nameof(OperationalWorkLineData.ResponsibleUser2):
					return workLineData.ResponsibleUser2;
				case nameof(OperationalWorkLineData.DtPlanCompilation):
					return workLineData.DtPlanCompilation;
				case nameof(OperationalWorkLineData.DtSendCompilation):
					return workLineData.DtSendCompilation;
				case nameof(OperationalWorkLineData.DtSend):
					return workLineData.DtSend;
				case nameof(OperationalWorkLineData.Fon):
					return workLineData.Fon;
				case nameof(OperationalWorkLineData.PHH):
					return workLineData.PHH;
				case nameof(OperationalWorkLineData.DtIssuePrescriptionGO):
					return workLineData.DtIssuePrescriptionGO;
				case nameof(OperationalWorkLineData.PGR):
					return workLineData.PGR;
				case nameof(OperationalWorkLineData.IGS):
					return workLineData.IGS;
				case nameof(OperationalWorkLineData.PchvOa):
					return workLineData.PchvOa;
				case nameof(OperationalWorkLineData.PchvBak):
					return workLineData.PchvBak;
				case nameof(OperationalWorkLineData.PchvPrz):
					return workLineData.PchvPrz;
				case nameof(OperationalWorkLineData.AhSH):
					return workLineData.AhSH;
				case nameof(OperationalWorkLineData.AhPr):
					return workLineData.AhPr;
				case nameof(OperationalWorkLineData.PchvZso):
					return workLineData.PchvZso;
				case nameof(OperationalWorkLineData.PovVod):
					return workLineData.PovVod;
				case nameof(OperationalWorkLineData.PodzVodIpvs):
					return workLineData.PodzVodIpvs;
				case nameof(OperationalWorkLineData.PodzVodIgs):
					return workLineData.PodzVodIgs;
				case nameof(OperationalWorkLineData.Don):
					return workLineData.Don;
				case nameof(OperationalWorkLineData.Av):
					return workLineData.Av;
				case nameof(OperationalWorkLineData.GraS):
					return workLineData.GraS;
				case nameof(OperationalWorkLineData.Rad):
					return workLineData.Rad;
				case nameof(OperationalWorkLineData.Ern):
					return workLineData.Ern;
				case nameof(OperationalWorkLineData.Ff):
					return workLineData.Ff;
				case nameof(OperationalWorkLineData.ProtocolRadiation):
					return workLineData.ProtocolRadiation;
				case nameof(OperationalWorkLineData.DtForecastEndKameralIgdi):
					return workLineData.DtForecastEndKameralIgdi;
				case nameof(OperationalWorkLineData.DtForecastKameralIgi):
					return workLineData.DtForecastKameralIgi;
				case nameof(OperationalWorkLineData.DtForecastCameralIgmi):
					return workLineData.DtForecastCameralIgmi;
				case nameof(OperationalWorkLineData.HasProtocolFieldSurvay):
					return workLineData.HasProtocolFieldSurvay;
				case nameof(OperationalWorkLineData.HasGraficPart):
					return workLineData.HasGraficPart;
				case nameof(OperationalWorkLineData.Vetstancia):
					return workLineData.Vetstancia;
				case nameof(OperationalWorkLineData.Districts):
					return workLineData.Districts;
				case nameof(OperationalWorkLineData.PlanSmetaCountSkvazhin):
					return workLineData.PlanSmetaCountSkvazhin;
				case nameof(OperationalWorkLineData.PlanSmetaPm):
					return workLineData.PlanSmetaPm;
				case nameof(OperationalWorkLineData.PlanSmetaCountMonolit):
					return workLineData.PlanSmetaCountMonolit;
				case nameof(OperationalWorkLineData.ExecuteSmetaCountSkvazhin):
					return workLineData.ExecuteSmetaCountSkvazhin;
				case nameof(OperationalWorkLineData.ExecuteSmetaPm):
					return workLineData.ExecuteSmetaPm;
				case nameof(OperationalWorkLineData.ExecuteSmetaCountMonolit):
					return workLineData.ExecuteSmetaCountMonolit;
			}
			return null;
		}
	}
}
