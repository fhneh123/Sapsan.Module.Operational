using Sapsan.Modules.Operational.DAL.Entity;
using Sapsan.Modules.Operational.Presenters;
using Sapsan.Modules.Operational.Presenters.Contracts;
using Sapsan.Modules.Operational.Services;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan2.Contracts.Contracts.UI;
using Sapsan2.Contracts.Data;
using TES.Fx.Data.Ef;

namespace Sapsan.Modules.Operational
{
	public class DependecyRegister : IDependecyRegister
	{
		public DependencyRegisterData[] GetRegistrationData()
		{
			return new[]
			{
				new DependencyRegisterData
				{
					Service = typeof(IEntityRepository<Operationals>),
					Impementation = typeof(EntityRepository<Operationals>)
				},
				new DependencyRegisterData
				{
					Service = typeof(IEntityRepository<OperationalWork>),
					Impementation = typeof(EntityRepository<OperationalWork>)
				},
				new DependencyRegisterData
				{
					Service = typeof(IEntityRepository<OperationalLine>),
					Impementation = typeof(EntityRepository<OperationalLine>)
				},
				new DependencyRegisterData
				{
					Service = typeof(IEntityRepository<OperationalEntityStatus>),
					Impementation = typeof(EntityRepository<OperationalEntityStatus>)
				},
				new DependencyRegisterData
				{
					Service = typeof(IEntityRepository<OperationalWorkLine>),
					Impementation = typeof(EntityRepository<OperationalWorkLine>)
				},
				new DependencyRegisterData
				{
					Service = typeof(IEntityRepository<OperationalSpfoa>),
					Impementation = typeof(EntityRepository<OperationalSpfoa>)
				},
				new DependencyRegisterData
				{
					Service = typeof(IEntityRepository<OperationalSpfoaEditor>),
					Impementation = typeof(EntityRepository<OperationalSpfoaEditor>)
				},
				new DependencyRegisterData
				{
					Service = typeof(IEntityRepository<OperationalTypeTask>),
					Impementation = typeof(EntityRepository<OperationalTypeTask>)
				},
				new DependencyRegisterData
				{
					Service = typeof(IOperationalService),
					Impementation = typeof(OperationalService)
				},
				new DependencyRegisterData
				{
					Service = typeof(IOperationalWorkService),
					Impementation = typeof(OperationalWorkService)
				},
				new DependencyRegisterData
				{
					Service = typeof(IOperationalWorkPresenter),
					Impementation = typeof(OperationalWorkPresenter)
				},
				new DependencyRegisterData
				{
					Service = typeof(IOperationalLineService),
					Impementation = typeof(OperationalLineService)
				},
				new DependencyRegisterData
				{
					Service = typeof(IOperationalWorkLineService),
					Impementation = typeof(OperationalWorkLineService)
				},
				new DependencyRegisterData
				{
					Service = typeof(IOperationalLinePresenter),
					Impementation = typeof(OperationalLinePresenter)
				},
				new DependencyRegisterData
				{
					Service = typeof(IOperationalPresenter),
					Impementation = typeof(OperationalPresenter)
				},
				new DependencyRegisterData
				{
					Service = typeof(IOperationalEntityStatusService),
					Impementation = typeof(OperationalEntityStatusService)
				},
				new DependencyRegisterData
				{
					Service = typeof(IOperationalSpfoaService),
					Impementation = typeof(OperationalSpfoaService)
				},
				new DependencyRegisterData
				{
					Service = typeof(IOperationalSpfoaPresenter),
					Impementation = typeof(OperationalSpfoaPresenter)
				},
				new DependencyRegisterData
				{
					Service = typeof(IOperationalSpfoaEditorService),
					Impementation = typeof(OperatonalSpfoaEditorService)
				},
				new DependencyRegisterData
				{
					Service = typeof(IOperationalSpfoaEditorPresenter),
					Impementation = typeof(OperationalSpfoaEditorPresenter)
				},
				new DependencyRegisterData
				{
					Service = typeof(IOperationalTypeTaskService),
					Impementation = typeof(OperationalTypeTaskService)
				}
			};
		}

		public DependencyRegisterData[] GetRegistrationDataInNewContext()
		{
			return null;
		}
	}
}