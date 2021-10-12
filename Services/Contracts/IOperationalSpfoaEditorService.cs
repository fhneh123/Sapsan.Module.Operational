using System;
using System.Linq;
using Sapsan.Modules.Operational.Services.Data;

namespace Sapsan.Modules.Operational.Services.Contracts
{
	public interface IOperationalSpfoaEditorService
	{
		IQueryable<OperationalSpfoaEditorData> GetList();
		Guid Add(OperationalSpfoaEditorData entity);
		void Update(Guid id, OperationalSpfoaEditorData entity);
		void Delete(Guid id);
	}
}