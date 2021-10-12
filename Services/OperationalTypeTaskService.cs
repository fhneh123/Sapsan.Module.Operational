using System;
using System.Collections.Generic;
using System.Linq;
using Sapsan.Modules.Operational.DAL.Entity;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan2.Contracts.Exceptions;
using Sapsan2.Core.Service.Contracts;
using Sapsan2.Core.Transactions;
using TES.Fx.Data.Ef;

namespace Sapsan.Modules.Operational.Services
{
	public class OperationalTypeTaskService : IOperationalTypeTaskService
	{
		private readonly IEntityRepository<OperationalTypeTask> _operationalTypeTaskRepository;
		private readonly ILogService _logService;
		private readonly IEntityRepository<OperationalLine> _operationalLineRepository;

		public OperationalTypeTaskService(
			IEntityRepository<OperationalTypeTask> operationalTypeTaskRepository,
			ILogService logService,
			IEntityRepository<OperationalLine> operationalLineRepository)
		{
			_operationalTypeTaskRepository = operationalTypeTaskRepository;
			_logService = logService;
			_operationalLineRepository = operationalLineRepository;
		}

		public IQueryable<OperationalTypeTaskData> GetList()
		{
			return GetListWithDeleted().Where(x => x.DtDelete.HasValue == false);
		}

		public IQueryable<OperationalTypeTaskData> GetListWithDeleted()
		{
			return _operationalTypeTaskRepository.GetAll().OrderBy(x => x.OrderNum).Select(x =>
				new OperationalTypeTaskData
				{
					Id = x.Id,
					Name = x.Name,
					OrderNum = x.OrderNum,
					DtCreate = x.DtCreate,
					DtDelete = x.DtDelete
				});
		}

		public Guid Add(OperationalTypeTaskData entity)
		{
			if (_operationalTypeTaskRepository.GetAll().Any(x => x.Name == entity.Name && x.DtDelete.HasValue == false))
			{
				throw new ClientException("Тип задания с таким именем уже существует");
			}

			var typeTask = new OperationalTypeTask
			{
				Id = Guid.NewGuid(),
				Name = entity.Name,
				OrderNum = entity.OrderNum,
				DtCreate = DateTime.Now,
			};

			using (var scope = new RequiredTransactionScope())
			{
				_operationalTypeTaskRepository.Add(typeTask);

				_logService.Log(
					typeTask.Id,
					"Добавление типа задания",
					entity.Name,
					typeTask.Id);

				scope.Complete();
			}

			return typeTask.Id;
		}

		public void Update(Guid id, OperationalTypeTaskData entity)
		{
			var typeTask = _operationalTypeTaskRepository.GetAll().FirstOrDefault(x => x.Id == id);

			if (typeTask == null)
			{
				return;
			}

			if (typeTask.Name == entity.Name
				&& typeTask.OrderNum == entity.OrderNum)
			{
				return;
			}

			var changeList = new List<string>();

			if (typeTask.Name != entity.Name)
			{
				changeList.Add($"Наименование: {typeTask.Name} -> {entity.Name}");
			}

			if (typeTask.OrderNum != entity.OrderNum)
			{
				changeList.Add($"Порядковый номер: {typeTask.OrderNum} -> {entity.OrderNum}");
			}

			typeTask.Name = entity.Name;
			typeTask.OrderNum = entity.OrderNum;

			using (var scope = new RequiredTransactionScope())
			{
				_operationalTypeTaskRepository.Update(typeTask);

				_logService.Log(
					typeTask.Id,
					"Изменение типа задания",
					string.Join("; ", changeList),
					typeTask.Id);

				scope.Complete();
			}
		}

		public void Delete(Guid id)
		{
			var typeTask = _operationalTypeTaskRepository.GetAll().FirstOrDefault(x => x.Id == id);
			if (typeTask == null)
			{
				return;
			}

			if (_operationalLineRepository.GetAll().Any(x => x.TypeTaskId == id))
			{
				throw new ClientException(
					"Для удаления типа задания сначала необходимо отвязать его от всех строк оперативок");
			}

			using (var scope = new RequiredTransactionScope())
			{
				typeTask.DtDelete = DateTime.Now;
				_operationalTypeTaskRepository.Update(typeTask);

				_logService.Log(
					typeTask.Id,
					"Удаление типа задания",
					typeTask.Name,
					typeTask.Id);

				scope.Complete();
			}
		}
	}
}