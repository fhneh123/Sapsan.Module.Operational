using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using Sapsan.Modules.Operational.DAL.Entity;
using Sapsan2.Contracts.Contracts.Database;

namespace Sapsan.Modules.Operational.DAL.Configuration
{
	public class OperationalTypeTaskConfiguration : EntityTypeConfiguration<OperationalTypeTask>, IEntityConfiguration
	{
		public OperationalTypeTaskConfiguration()
		{
			ToTable("modules.operational_type_task");
			HasKey(t => t.Id);
		}

		public void AddConfiguration(ConfigurationRegistrar registrar)
		{
			registrar.Add(this);
		}
	}
}