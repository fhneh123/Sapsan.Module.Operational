using System;
using System.Linq;
using Sapsan.Modules.Operational.Services.Data;

namespace Sapsan.Modules.Operational.Services.Contracts
{
	public interface IOperationalTypeTaskService
	{
		IQueryable<OperationalTypeTaskData> GetList();
		IQueryable<OperationalTypeTaskData> GetListWithDeleted();
		void Update(Guid id, OperationalTypeTaskData entity);
		Guid Add(OperationalTypeTaskData entity);
		void Delete(Guid id);
	}
}