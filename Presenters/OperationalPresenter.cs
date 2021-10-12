using System.Collections.Generic;
using System.Linq;
using Sapsan.Modules.Operational.Presenters.Contracts;
using Sapsan.Modules.Operational.Presenters.Data;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.ProjectScheduler.Services;

namespace Sapsan.Modules.Operational.Presenters
{
	public class OperationalPresenter : IOperationalPresenter
	{
		private readonly IOperationalService _operationalService;
		private readonly WorkStructureSprService _workStructureSprService;

		public OperationalPresenter(
			IOperationalService operationalService,
			WorkStructureSprService workStructureSprService)
		{
			_operationalService = operationalService;
			_workStructureSprService = workStructureSprService;
		}

		public List<OperationalPresenterData> GetData()
		{
			return (from o in _operationalService.GetListWithDeleted()
					join w in _workStructureSprService.GetList() on o.ComplianceGroupId equals w.Id into wy
					from w in wy.DefaultIfEmpty()
					orderby o.OrderNum
					select new OperationalPresenterData
					{
						Id = o.Id,
						Name = o.Name,
						OrderNum = o.OrderNum,
						ComplianceGroupId = o.ComplianceGroupId,
						ComplianceGroupName = w.Name,
						DtCreate = o.DtCreate,
						DtDelete = o.DtDelete
					}).ToList();
		}
	}
}