using System;
using System.Linq;
using Sapsan.Modules.Operational.Services.Data;

namespace Sapsan.Modules.Operational.Services.Contracts
{
	public interface IOperationalEntityStatusService
	{
		IQueryable<OperationalEntityStatusData> GetList();

		Guid Add(OperationalEntityStatusData entity);

		void Update(Guid id, OperationalEntityStatusData entity);

		void Delete(Guid id);

		IQueryable<OperationalEntityStatusData> GetListWithDeleted();
	}
}