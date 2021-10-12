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
	public class OperationalService : IOperationalService
	{
		private readonly IEntityRepository<Operationals> _operationalRepository;
		private readonly IEntityRepository<OperationalWork> _operationalWorkRepository;
		private readonly ILogService _logService;
		private readonly WorkStructureSprService _workStructureSprService;

		public OperationalService(
			IEntityRepository<Operationals> operationalRepository,
			IEntityRepository<OperationalWork> operationalWorkRepository,
			ILogService logService,
			WorkStructureSprService workStructureSprService)
		{
			_operationalRepository = operationalRepository;
			_logService = logService;
			_operationalWorkRepository = operationalWorkRepository;
			_workStructureSprService = workStructureSprService;
		}

		public IQueryable<OperationalData> GetList()
		{
			return GetListWithDeleted().Where(x => x.DtDelete.HasValue == false);
		}

		public IQueryable<OperationalData> GetListWithDeleted()
		{
			return _operationalRepository.GetAll().OrderBy(x => x.OrderNum).Select(x => new OperationalData
			{
				Id = x.Id,
				Name = x.Name,
				OrderNum = x.OrderNum,
				ComplianceGroupId = x.ComplianceGroupId,
				DtCreate = x.DtCreate,
				DtDelete = x.DtDelete
			});
		}

		public Guid Add(OperationalData entity)
		{
			if (_operationalRepository.GetAll().Any(x => x.Name == entity.Name && x.DtDelete.HasValue == false))
			{
				throw new ClientException("Опреативка с таким именем уже существует");
			}

			var operational = new Operationals
			{
				Id = Guid.NewGuid(),
				Name = entity.Name,
				OrderNum = entity.OrderNum,
				ComplianceGroupId = entity.ComplianceGroupId,
				DtCreate = DateTime.Now,
			};

			using (var scope = new RequiredTransactionScope())
			{
				_operationalRepository.Add(operational);

				_logService.Log(
					operational.Id,
					"Добавление оперативки",
					entity.Name,
					operational.Id);

				scope.Complete();
			}

			return operational.Id;
		}

		public void Update(Guid id, OperationalData entity)
		{
			var operational = _operationalRepository.GetAll().FirstOrDefault(x => x.Id == id);

			if (operational == null)
			{
				return;
			}

			if (operational.Name == entity.Name
				&& operational.OrderNum == entity.OrderNum
				&& operational.ComplianceGroupId == entity.ComplianceGroupId)
			{
				return;
			}

			var changeList = new List<string>();

			if (operational.Name != entity.Name)
			{
				changeList.Add($"Наименование: {operational.Name} -> {entity.Name}");
			}

			if (operational.OrderNum != entity.OrderNum)
			{
				changeList.Add($"Порядковый номер: {operational.OrderNum} -> {entity.OrderNum}");
			}

			if (operational.ComplianceGroupId != entity.ComplianceGroupId)
			{
				changeList.Add(
					$"Соответствует работе: {_workStructureSprService.GetList().FirstOrDefault(x => x.Id == operational.ComplianceGroupId)?.Name} " +
					$"-> {_workStructureSprService.GetList().FirstOrDefault(x => x.Id == entity.ComplianceGroupId)?.Name}");
			}

			operational.Name = entity.Name;
			operational.OrderNum = entity.OrderNum;
			operational.ComplianceGroupId = entity.ComplianceGroupId;

			using (var scope = new RequiredTransactionScope())
			{
				_operationalRepository.Update(operational);

				_logService.Log(
					operational.Id,
					"Изменение оперативки",
					string.Join("; ", changeList),
					operational.Id);

				scope.Complete();
			}
		}

		public void Delete(Guid id)
		{
			var operational = _operationalRepository.GetAll().FirstOrDefault(x => x.Id == id);
			if (operational == null)
			{
				return;
			}

			if (_operationalWorkRepository.GetAll().Any(x => x.OperationalId == id && x.DtDelete.HasValue == false))
			{
				throw new ClientException("Для удаления оперативки сначала необходимо удалить привязанные работы.");
			}

			using (var scope = new RequiredTransactionScope())
			{
				operational.DtDelete = DateTime.Now;
				_operationalRepository.Update(operational);

				_logService.Log(
					operational.Id,
					"Удаление оперативки",
					operational.Name,
					operational.Id);

				scope.Complete();
			}
		}
	}
}