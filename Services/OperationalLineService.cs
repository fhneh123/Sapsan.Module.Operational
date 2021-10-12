using System;
using System.Collections.Generic;
using System.Linq;
using Sapsan.Modules.Operational.DAL.Entity;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan.Modules.ProjectScheduler.Services;
using Sapsan2.Core.Contracts;
using Sapsan2.Core.Service.Contracts;
using Sapsan2.Core.Transactions;
using TES.Fx.Data.Ef;

namespace Sapsan.Modules.Operational.Services
{
	public class OperationalLineService : IOperationalLineService
	{
		private readonly IEntityRepository<OperationalLine> _operationalLineRepository;
		private readonly IOperationalService _operationalService;
		private readonly ILogService _logService;
		private readonly PlanService _planService;
		private readonly IOperationalWorkLineService _operationalWorkLineService;
		private readonly WorkStructureService _workStructureService;
		private readonly IContractService _contractService;
		private readonly IDesignBasicService _designBasicService;
		private readonly ISubdivisionService _subdivisionService;
		private readonly IOperationalSpfoaService _operationalSpfoaService;
		private readonly IOperationalTypeTaskService _operationalTypeTaskService;

		public OperationalLineService(
			IEntityRepository<OperationalLine> operationalLineRepository,
			IOperationalService operationalService,
			ILogService logService,
			PlanService planService,
			IOperationalWorkLineService operationalWorkLineService,
			WorkStructureService workStructureService,
			IContractService contractService,
			IDesignBasicService designBasicService,
			ISubdivisionService subdivisionService,
			IOperationalSpfoaService operationalSpfoaService,
			IOperationalTypeTaskService operationalTypeTaskService)
		{
			_operationalLineRepository = operationalLineRepository;
			_operationalService = operationalService;
			_logService = logService;
			_planService = planService;
			_operationalWorkLineService = operationalWorkLineService;
			_workStructureService = workStructureService;
			_contractService = contractService;
			_designBasicService = designBasicService;
			_subdivisionService = subdivisionService;
			_operationalSpfoaService = operationalSpfoaService;
			_operationalTypeTaskService = operationalTypeTaskService;
		}

		public IQueryable<OperationalLineData> GetList()
		{
			return _operationalLineRepository.GetAll().Select(x => new OperationalLineData
			{
				Id = x.Id,
				OperationalId = x.OperationalId,
				ContractId = x.ContractId,
				PlanId = x.PlanId,
				Priority = x.Priority,
				ComplianceGroupId = x.ComplianceGroupId,
				TypeTaskId = x.TypeTaskId,
				SubdivisionId = x.SubdivisionId,
				DtCreate = x.DtCreate,
				DtDelete = x.DtDelete
			});
		}

		public Guid Add(OperationalLineData entity)
		{
			var operationalLine = new OperationalLine
			{
				Id = Guid.NewGuid(),
				OperationalId = entity.OperationalId,
				ContractId = entity.ContractId,
				PlanId = entity.PlanId,
				ComplianceGroupId = entity.ComplianceGroupId,
				TypeTaskId = entity.TypeTaskId,
				Priority = entity.Priority,
				SubdivisionId = entity.SubdivisionId,
				DtCreate = DateTime.Now
			};

			using (var scope = new RequiredTransactionScope())
			{
				_operationalLineRepository.Add(operationalLine);

				_operationalWorkLineService.AddAll(operationalLine.Id);

				var stageNumber = _workStructureService.GetList().SingleOrDefault(x => x.Id == operationalLine.ComplianceGroupId)?.StageNumber;
				_operationalSpfoaService.Add(entity, stageNumber);

				_logService.Log(
					operationalLine.Id,
					"Добавление строки оперативки",
					string.Empty,
					operationalLine.Id);

				scope.Complete();
			}

			return operationalLine.Id;
		}

		public void Update(Guid id, OperationalLineData entity)
		{
			var operationalLine = _operationalLineRepository.GetAll().FirstOrDefault(x => x.Id == id);

			if (operationalLine == null)
			{
				return;
			}

			if (operationalLine.OperationalId == entity.OperationalId
				&& operationalLine.ContractId == entity.ContractId
				&& operationalLine.PlanId == entity.PlanId
				&& operationalLine.Priority == entity.Priority
				&& operationalLine.ComplianceGroupId == entity.ComplianceGroupId
				&& operationalLine.TypeTaskId == entity.TypeTaskId
				&& operationalLine.SubdivisionId == entity.SubdivisionId)
			{
				return;
			}

			var changeList = new List<string>();

			if (operationalLine.OperationalId != entity.OperationalId)
			{
				changeList.Add(
					$"Оперативка: {_operationalService.GetList().Single(x => x.Id == operationalLine.OperationalId).Name} " +
					$"-> {_operationalService.GetList().Single(x => x.Id == entity.OperationalId).Name}");
			}

			if (operationalLine.ContractId != entity.ContractId)
			{
				changeList.Add(
					$"Проект: {_contractService.GetList().Single(x => x.Id == operationalLine.ContractId).Shifr} " +
					$"-> {_contractService.GetList().Single(x => x.Id == entity.ContractId).Shifr}");
			}

			if (operationalLine.PlanId != entity.PlanId)
			{
				changeList.Add($"План: {_planService.GetList().Single(x => x.Id == operationalLine.PlanId).Name} " +
							   $"-> {_planService.GetList().Single(x => x.Id == entity.PlanId).Name}");
			}

			if (operationalLine.Priority != entity.Priority)
			{
				changeList.Add($"Приоритет: {operationalLine.Priority} -> {entity.Priority}");
			}

			if (operationalLine.ComplianceGroupId != entity.ComplianceGroupId)
			{
				changeList.Add(
					$"Соответствует группе: {_workStructureService.GetList().FirstOrDefault(x => x.Id == operationalLine.ComplianceGroupId)?.Name} " +
					$"-> {_workStructureService.GetList().FirstOrDefault(x => x.Id == entity.ComplianceGroupId)?.Name}");

				_operationalWorkLineService.UpdateComplianceWork(id, entity.ComplianceGroupId);
			}

			if (operationalLine.TypeTaskId != entity.TypeTaskId)
			{
				changeList.Add(
					$"Тип задания: {_operationalTypeTaskService.GetList().SingleOrDefault(x => x.Id == operationalLine.TypeTaskId)?.Name} " +
					$"-> {_operationalTypeTaskService.GetList().SingleOrDefault(x => x.Id == entity.TypeTaskId)?.Name}");
			}

			if (operationalLine.SubdivisionId != entity.SubdivisionId)
			{
				changeList.Add(
					$"Подразделение: {_subdivisionService.GetList().Single(x => x.Id == operationalLine.SubdivisionId).ShortName} " +
					$"-> {_subdivisionService.GetList().Single(x => x.Id == entity.SubdivisionId).ShortName}");
			}

			operationalLine.OperationalId = entity.OperationalId;
			operationalLine.ContractId = entity.ContractId;
			operationalLine.PlanId = entity.PlanId;
			operationalLine.Priority = entity.Priority;
			operationalLine.ComplianceGroupId = entity.ComplianceGroupId;
			operationalLine.TypeTaskId = entity.TypeTaskId;
			operationalLine.SubdivisionId = entity.SubdivisionId;

			using (var scope = new RequiredTransactionScope())
			{
				_operationalLineRepository.Update(operationalLine);

				var stageNumber = _workStructureService.GetList().SingleOrDefault(x => x.Id == operationalLine.ComplianceGroupId)?.StageNumber;
				_operationalSpfoaService.Add(entity, stageNumber);

				_logService.Log(
					operationalLine.Id,
					"Изменение строки оперативки",
					string.Join("; ", changeList),
					operationalLine.Id);

				scope.Complete();
			}
		}

		public void Delete(Guid id)
		{
			var operationalLine = _operationalLineRepository.GetAll().FirstOrDefault(x => x.Id == id);

			if (operationalLine == null)
			{
				return;
			}

			using (var scope = new RequiredTransactionScope())
			{
				operationalLine.DtDelete = DateTime.Now;
				_operationalLineRepository.Update(operationalLine);

				_logService.Log(
					operationalLine.Id,
					"Удаление строки оперативки",
					string.Empty,
					operationalLine.Id);

				scope.Complete();
			}
		}

		public void Recover(Guid id)
		{
			var operationalLine = _operationalLineRepository.GetAll().FirstOrDefault(x => x.Id == id);

			if (operationalLine == null)
			{
				return;
			}

			using (var scope = new RequiredTransactionScope())
			{
				operationalLine.DtDelete = null;
				_operationalLineRepository.Update(operationalLine);

				_logService.Log(
					operationalLine.Id,
					"Восстановление строки оперативки",
					string.Empty,
					operationalLine.Id);

				scope.Complete();
			}
		}
	}
}