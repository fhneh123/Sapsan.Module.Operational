using System;
using System.Linq;
using Sapsan.Modules.Operational.Services.Data;

namespace Sapsan.Modules.Operational.Services.Contracts
{
	public interface IOperationalWorkLineService
	{
		IQueryable<OperationalWorkLineData> GetList();
		void AddAll(Guid operationalLineId);
		void Add(OperationalWorkLineData entity);
		void Update(Guid operationalLineId, Guid operationalWorkId, string columnName, object value);
		void UpdateComplianceWork(Guid operationalLineId, Guid newComplianceGroupId);
		bool CheckComplianceWorkIsAddedInAnyWorkLine(Guid operationalLineId, Guid operationalWorkId, Guid? complianceWorkId);
	}
}