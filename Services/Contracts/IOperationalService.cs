using System;
using System.Linq;
using Sapsan.Modules.Operational.Services.Data;

namespace Sapsan.Modules.Operational.Services.Contracts
{
	public interface IOperationalService
	{
		IQueryable<OperationalData> GetList();
		IQueryable<OperationalData> GetListWithDeleted();
		void Update(Guid id, OperationalData entity);
		Guid Add(OperationalData entity);
		void Delete(Guid id);
	}
}