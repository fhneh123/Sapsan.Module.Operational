using System.Collections.Generic;
using Sapsan.Modules.Operational.Presenters.Data;

namespace Sapsan.Modules.Operational.Presenters.Contracts
{
	public interface IOperationalWorkPresenter
	{
		List<OperationalWorkFullInfoPresenterData> GetData();
	}
}