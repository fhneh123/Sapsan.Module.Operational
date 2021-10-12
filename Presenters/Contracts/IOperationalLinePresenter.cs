using System;
using System.Collections.Generic;
using Sapsan.Modules.Operational.Presenters.Data;
using Sapsan.Modules.Operational.Services.Data;

namespace Sapsan.Modules.Operational.Presenters.Contracts
{
	public interface IOperationalLinePresenter
	{
		List<OperationalLinePresenterData> GetData(Guid operationalId, bool withAnnul, Guid? operationalLineId = null);
		List<OperationalWorkLineData> GetWorkLines(Guid operationalId);
		object Get(string name, OperationalWorkLineData workLineData);
	}
}