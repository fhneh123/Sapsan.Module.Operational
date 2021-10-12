using System.Collections.Generic;
using System.Linq;
using Sapsan.Modules.Operational.Enums;
using Sapsan.Modules.Operational.Presenters.Contracts;
using Sapsan.Modules.Operational.Presenters.Data;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan2.Contracts.Extensions;

namespace Sapsan.Modules.Operational.Presenters
{
	public class OperationalSpfoaEditorPresenter : IOperationalSpfoaEditorPresenter
	{
		public readonly IOperationalSpfoaEditorService _operationalSpfoaEditorService;
		public readonly IOperationalWorkService _operationalWorkService;
		public readonly IOperationalService _operationalService;
		public readonly IOperationalEntityStatusService _operationalEntityStatusService;

		public OperationalSpfoaEditorPresenter(
			IOperationalSpfoaEditorService operationalSpfoaEditorService,
			IOperationalWorkService operationalWorkService,
			IOperationalService operationalService,
			IOperationalEntityStatusService operationalEntityStatusService)
		{
			_operationalSpfoaEditorService = operationalSpfoaEditorService;
			_operationalWorkService = operationalWorkService;
			_operationalService = operationalService;
			_operationalEntityStatusService = operationalEntityStatusService;
		}

		public List<OperationalSpfoaEditorPresenterData> GetData()
		{
			var operationalSpfoaEditorPresenterData = (from s in _operationalSpfoaEditorService.GetList()
													   join w in _operationalWorkService.GetList() on s.OperatioalWorkId equals w.Id
													   join o in _operationalService.GetList() on w.OperationalId equals o.Id
													   join os in _operationalEntityStatusService.GetList() on s.OperationalStatusId equals os.Id into osy
													   from os in osy.DefaultIfEmpty()
													   join ss in _operationalEntityStatusService.GetList() on s.SpfoaStatusId equals ss.Id
													   select new OperationalSpfoaEditorPresenterData
													   {
														   Id = s.Id,
														   OperatioalId = o.Id,
														   OperatioalName = o.Name,
														   OperatioalWorkId = w.Id,
														   OperatioalWorkName = w.Name,
														   OperationalStatusId = os.Id,
														   OperationalStatusName = os.Name,
														   OperationalDateTypeId = s.OperationalDateTypeId,
														   SpfoaStatusId = ss.Id,
														   SpfoaStatus = ss.Name,
														   OrderNum = s.OrderNum,
														   DtCreate = s.DtCreate,
														   DtDelete = s.DtDelete
													   }).ToList();

			foreach (var item in operationalSpfoaEditorPresenterData)
			{
				item.OperationalDateType = ((OperationalSpfoaDateTypeEnum)item.OperationalDateTypeId).GetDisplayName();
			}

			return operationalSpfoaEditorPresenterData;
		}
	}
}