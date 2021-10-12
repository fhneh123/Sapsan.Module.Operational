using System;
using System.Collections.Generic;
using System.Linq;
using Sapsan.Modules.Operational.DAL.Entity;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan2.Core.Service.Contracts;
using Sapsan2.Core.Transactions;
using TES.Fx.Data.Ef;

namespace Sapsan.Modules.Operational.Services
{
	public class OperationalEntityStatusService : IOperationalEntityStatusService
	{
		private readonly IEntityRepository<OperationalEntityStatus> _statusRepository;
		private readonly ILogService _logService;

		public OperationalEntityStatusService(
			IEntityRepository<OperationalEntityStatus> statusRepository,
			ILogService logService)
		{
			_statusRepository = statusRepository;
			_logService = logService;
		}

		public IQueryable<OperationalEntityStatusData> GetList()
		{
			return GetListWithDeleted().Where(x => x.DtDelete.HasValue == false);
		}

		public IQueryable<OperationalEntityStatusData> GetListWithDeleted()
		{
			return _statusRepository.GetAll().Select(x => new OperationalEntityStatusData
			{
				Id = x.Id,
				OperationalWorkId = x.OperationalWorkId,
				Name = x.Name,
				EntityId = x.EntityId,
				Color = x.Color,
				OrderNum = x.OrderNum,
				DtCreate = x.DtCreate,
				DtDelete = x.DtDelete
			}).OrderBy(x => x.OrderNum);
		}

		public Guid Add(OperationalEntityStatusData entity)
		{
			var operatonalWorkStatus = new OperationalEntityStatus
			{
				Id = Guid.NewGuid(),
				OperationalWorkId = entity.OperationalWorkId,
				Name = entity.Name,
				EntityId = entity.EntityId,
				Color = entity.Color,
				OrderNum = entity.OrderNum,
				DtCreate = DateTime.Now
			};

			using (var scope = new RequiredTransactionScope())
			{
				_statusRepository.Add(operatonalWorkStatus);

				_logService.Log(
					operatonalWorkStatus.Id,
					"Добавление статуса",
					entity.Name,
					operatonalWorkStatus.OperationalWorkId);

				scope.Complete();
			}

			return operatonalWorkStatus.Id;
		}

		public void Update(Guid id, OperationalEntityStatusData entity)
		{
			var status = _statusRepository.GetAll().FirstOrDefault(x => x.Id == id);

			if (status == null)
			{
				return;
			}

			if (status.Name == entity.Name
				&& status.OrderNum == entity.OrderNum
				&& status.Color == entity.Color)
			{
				return;
			}

			var changeList = new List<string>();

			if (status.Name != entity.Name)
			{
				changeList.Add($"Наименование: {status.Name} -> {entity.Name}");
			}

			if (status.OrderNum != entity.OrderNum)
			{
				changeList.Add($"Порядковый номер: {status.OrderNum} -> {entity.OrderNum}");
			}

			if (status.Color != entity.Color)
			{
				changeList.Add($"Цвет: {status.Color} -> {entity.Color}");
			}

			status.Name = entity.Name;
			status.OrderNum = entity.OrderNum;
			status.Color = entity.Color;

			using (var scope = new RequiredTransactionScope())
			{
				_statusRepository.Update(status);

				_logService.Log(
					status.Id,
					"Изменение статуса",
					string.Join("; ", changeList),
					status.Id);

				scope.Complete();
			}
		}

		public void Delete(Guid id)
		{
			var status = _statusRepository.GetAll().FirstOrDefault(x => x.Id == id);
			if (status == null)
			{
				return;
			}

			using (var scope = new RequiredTransactionScope())
			{
				status.DtDelete = DateTime.Now;
				_statusRepository.Update(status);

				_logService.Log(
					status.Id,
					"Удаление статуса",
					status.Name,
					status.Id);

				scope.Complete();
			}
		}
	}
}