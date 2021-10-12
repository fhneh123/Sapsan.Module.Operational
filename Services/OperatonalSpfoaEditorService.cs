using System;
using System.Collections.Generic;
using System.Linq;
using Sapsan.Modules.Operational.DAL.Entity;
using Sapsan.Modules.Operational.Enums;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan2.Core.Service.Contracts;
using Sapsan2.Core.Transactions;
using TES.Fx.Data.Ef;

namespace Sapsan.Modules.Operational.Services
{
	public class OperatonalSpfoaEditorService : IOperationalSpfoaEditorService
	{
		private readonly IEntityRepository<OperationalSpfoaEditor> _spfoaEditorRepository;
		private readonly ILogService _logService;
		private readonly IOperationalWorkService _operationalWorkService;
		private readonly IOperationalEntityStatusService _operationalEntityStatusService;

		public OperatonalSpfoaEditorService(
			IEntityRepository<OperationalSpfoaEditor> spfoaEditorRepository,
			ILogService logService,
			IOperationalWorkService operationalWorkService,
			IOperationalEntityStatusService operationalEntityStatusService)
		{
			_spfoaEditorRepository = spfoaEditorRepository;
			_logService = logService;
			_operationalWorkService = operationalWorkService;
			_operationalEntityStatusService = operationalEntityStatusService;
		}

		public IQueryable<OperationalSpfoaEditorData> GetList()
		{
			return _spfoaEditorRepository.GetAll().Where(x => x.DtDelete.HasValue == false).Select(x =>
				new OperationalSpfoaEditorData
				{
					Id = x.Id,
					OperatioalWorkId = x.OperatioalWorkId,
					OperationalStatusId = x.OperationalStatusId,
					OperationalDateTypeId = x.OperationalDateTypeId,
					SpfoaStatusId = x.SpfoaStatusId,
					OrderNum = x.OrderNum,
					DtCreate = x.DtCreate,
					DtDelete = x.DtDelete
				});
		}

		public Guid Add(OperationalSpfoaEditorData entity)
		{
			var data = new OperationalSpfoaEditor
			{
				Id = Guid.NewGuid(),
				OperatioalWorkId = entity.OperatioalWorkId,
				OperationalStatusId = entity.OperationalStatusId,
				SpfoaStatusId = entity.SpfoaStatusId,
				OperationalDateTypeId = entity.OperationalDateTypeId,
				OrderNum = entity.OrderNum,
				DtCreate = DateTime.Now
			};

			using (var scope = new RequiredTransactionScope())
			{
				_spfoaEditorRepository.Add(data);

				_logService.Log(
					data.Id,
					"Добавление связи СПФОА с ПГ",
					null,
					data.Id);

				scope.Complete();
			}

			return data.Id;
		}

		public void Update(Guid id, OperationalSpfoaEditorData entity)
		{
			var data = _spfoaEditorRepository.GetAll().FirstOrDefault(x => x.Id == id);

			if (data == null)
			{
				return;
			}

			if (data.OperatioalWorkId == entity.OperatioalWorkId
				&& data.OperationalStatusId == entity.OperationalStatusId
				&& data.OperationalDateTypeId == entity.OperationalDateTypeId
				&& data.SpfoaStatusId == entity.SpfoaStatusId
				&& data.OrderNum == entity.OrderNum)
			{
				return;
			}

			var changeList = new List<string>();

			if (data.OperatioalWorkId != entity.OperatioalWorkId)
			{
				changeList.Add(
					$"Работа: {_operationalWorkService.GetList().Single(x => x.Id == data.OperatioalWorkId).Name} " +
					$"-> {_operationalWorkService.GetList().Single(x => x.Id == entity.OperatioalWorkId).Name}");
			}

			if (data.OperationalStatusId != entity.OperationalStatusId)
			{
				changeList.Add(
					$"Статус в оперативке: {_operationalEntityStatusService.GetList().SingleOrDefault(x => x.Id == data.OperationalStatusId)?.Name} " +
					$"-> {_operationalEntityStatusService.GetList().SingleOrDefault(x => x.Id == entity.OperationalStatusId)?.Name}");
			}

			if (data.SpfoaStatusId != entity.SpfoaStatusId)
			{
				changeList.Add(
					$"Статус в СПФОА ИИ: {_operationalEntityStatusService.GetList().Single(x => x.Id == data.SpfoaStatusId).Name} " +
					$"-> {_operationalEntityStatusService.GetList().Single(x => x.Id == entity.SpfoaStatusId).Name}");
			}

			if (data.OperationalDateTypeId != entity.OperationalDateTypeId)
			{
				changeList.Add($"Тип даты: {(OperationalSpfoaDateTypeEnum)data.OperationalDateTypeId} " +
							   $"-> {(OperationalSpfoaDateTypeEnum)entity.OperationalDateTypeId}");
			}

			if (data.OrderNum != entity.OrderNum)
			{
				changeList.Add($"Порядковый номер: {data.OrderNum} -> {entity.OrderNum}");
			}

			data.OperatioalWorkId = entity.OperatioalWorkId;
			data.OperationalStatusId = entity.OperationalStatusId;
			data.SpfoaStatusId = entity.SpfoaStatusId;
			data.OperationalDateTypeId = entity.OperationalDateTypeId;
			data.OrderNum = entity.OrderNum;

			using (var scope = new RequiredTransactionScope())
			{
				_spfoaEditorRepository.Update(data);

				_logService.Log(
					data.Id,
					"Изменение связи СПФОА с ПГ",
					string.Join("; ", changeList),
					data.Id);

				scope.Complete();
			}
		}

		public void Delete(Guid id)
		{
			var data = _spfoaEditorRepository.GetAll().FirstOrDefault(x => x.Id == id);

			if (data == null)
			{
				return;
			}

			using (var scope = new RequiredTransactionScope())
			{
				data.DtDelete = DateTime.Now;
				_spfoaEditorRepository.Update(data);

				_logService.Log(
					data.Id,
					"Удаление связи СПФОА с ПГ",
					null,
					data.Id);

				scope.Complete();
			}
		}
	}
}