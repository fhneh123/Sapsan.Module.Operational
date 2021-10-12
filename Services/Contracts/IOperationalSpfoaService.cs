using System;
using System.Linq;
using Sapsan.Modules.Operational.Services.Data;

namespace Sapsan.Modules.Operational.Services.Contracts
{
	public interface IOperationalSpfoaService
	{
		IQueryable<OperationalSpfoaData> GetList();
		void Update(Guid id, OperationalSpfoaData entity);
		void Add(OperationalLineData operationalLineData, int? stageNumber);
	}
}