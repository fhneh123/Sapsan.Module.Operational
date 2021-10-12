using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using Sapsan.Modules.Operational.DAL.Entity;
using Sapsan2.Contracts.Contracts.Database;

namespace Sapsan.Modules.Operational.DAL.Configuration
{
	public class OperationalEntityStatusConfiguration : EntityTypeConfiguration<OperationalEntityStatus>,
		IEntityConfiguration
	{
		public OperationalEntityStatusConfiguration()
		{
			ToTable("modules.operational_entity_status");
			HasKey(t => t.Id);
		}

		public void AddConfiguration(ConfigurationRegistrar registrar)
		{
			registrar.Add(this);
		}
	}
}