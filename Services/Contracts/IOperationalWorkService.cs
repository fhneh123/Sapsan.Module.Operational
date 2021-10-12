using System;
using System.Linq;
using Sapsan.Modules.Operational.Services.Data;

namespace Sapsan.Modules.Operational.Services.Contracts
{
	public interface IOperationalWorkService
	{
		IQueryable<OperationalWorkData> GetList();
		IQueryable<OperationalWorkData> GetListWithDeleted();
		Guid Add(OperationalWorkData entity);
		void Update(Guid id, OperationalWorkData entity);
		void Delete(Guid id);
	}
}