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
	public class OperationalSpfoaService : IOperationalSpfoaService
	{
		private readonly IEntityRepository<OperationalSpfoa> _spfoaRepository;
		private readonly IOperationalEntityStatusService _operationalEntityStatusService;
		private readonly ILogService _logService;

		public OperationalSpfoaService(
			IEntityRepository<OperationalSpfoa> spfoaRepository,
			IOperationalEntityStatusService operationalEntityStatusService,
			ILogService logService)
		{
			_spfoaRepository = spfoaRepository;
			_operationalEntityStatusService = operationalEntityStatusService;
			_logService = logService;
		}

		public IQueryable<OperationalSpfoaData> GetList()
		{
			return _spfoaRepository.GetAll().OrderBy(x => x.DtCreate).Select(x => new OperationalSpfoaData
			{
				Id = x.Id,
				ContractId = x.ContractId,
				PlanId = x.PlanId,
				StageNumber = x.StageNumber,
				TypeTaskId = x.TypeTaskId,
				SubdivisionId = x.SubdivisionId,
				ExecuteStatusId = x.ExecuteStatusId,
				RemarkKoii = x.RemarkKoii,
				DtActirovania = x.DtActirovania,
				DtCreate = x.DtCreate,
				DtDelete = x.DtDelete
			});
		}

		public void Update(Guid id, OperationalSpfoaData entity)
		{
			var data = _spfoaRepository.GetAll().FirstOrDefault(x => x.Id == id);

			if (data == null)
			{
				return;
			}

			if (data.ExecuteStatusId == entity.ExecuteStatusId
				&& data.DtActirovania == entity.DtActirovania
				&& data.RemarkKoii == entity.RemarkKoii)
			{
				return;
			}

			var changeList = new List<string>();

			if (data.ExecuteStatusId != entity.ExecuteStatusId)
			{
				changeList.Add(
					$"Выполнение: {_operationalEntityStatusService.GetList().SingleOrDefault(x => x.Id == data.ExecuteStatusId)?.Name} ->" +
					$" {_operationalEntityStatusService.GetList().SingleOrDefault(x => x.Id == entity.ExecuteStatusId)?.Name}");
			}

			if (data.DtActirovania != entity.DtActirovania)
			{
				changeList.Add($"Дата актирования: {data.DtActirovania} ->" +
							   $" {entity.DtActirovania}");
			}

			if (data.RemarkKoii != entity.RemarkKoii)
			{
				changeList.Add($"Примечание КОИИ: {data.RemarkKoii} ->" +
							   $" {entity.RemarkKoii}");
			}

			data.ExecuteStatusId = entity.ExecuteStatusId;
			data.DtActirovania = entity.DtActirovania;
			data.RemarkKoii = entity.RemarkKoii;

			using (var scope = new RequiredTransactionScope())
			{
				_spfoaRepository.Update(data);

				_logService.Log(
					data.Id,
					"Изменение СПФОА",
					string.Join("; ", changeList),
					data.Id);

				scope.Complete();
			}
		}

		public void Add(OperationalLineData operationalLineData, int? stageNumber)
		{
			if (_spfoaRepository.GetAll().Any(x => x.ContractId == operationalLineData.ContractId &&
												   x.PlanId == operationalLineData.PlanId
												   && x.TypeTaskId == operationalLineData.TypeTaskId &&
												   x.SubdivisionId == operationalLineData.SubdivisionId &&
												   x.StageNumber == stageNumber))
			{
				return;
			}

			var spfoa = new OperationalSpfoa
			{
				Id = Guid.NewGuid(),
				ContractId = operationalLineData.ContractId,
				PlanId = operationalLineData.PlanId,
				StageNumber = stageNumber,
				TypeTaskId = operationalLineData.TypeTaskId,
				SubdivisionId = operationalLineData.SubdivisionId,
				DtCreate = DateTime.Now
			};

			using (var scope = new RequiredTransactionScope())
			{
				_spfoaRepository.Add(spfoa);

				_logService.Log(
					spfoa.Id,
					"Добавление СПФОА",
					null,
					spfoa.Id);

				scope.Complete();
			}
		}
	}
}