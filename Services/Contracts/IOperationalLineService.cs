using System;
using System.Linq;
using Sapsan.Modules.Operational.Services.Data;

namespace Sapsan.Modules.Operational.Services.Contracts
{
	public interface IOperationalLineService
	{
		IQueryable<OperationalLineData> GetList();
		Guid Add(OperationalLineData entity);
		void Update(Guid id, OperationalLineData entity);
		void Delete(Guid id);
		void Recover(Guid id);
	}
}