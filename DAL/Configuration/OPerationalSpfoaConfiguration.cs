using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using Sapsan.Modules.Operational.DAL.Entity;
using Sapsan2.Contracts.Contracts.Database;

namespace Sapsan.Modules.Operational.DAL.Configuration
{
	public class OPerationalSpfoaConfiguration : EntityTypeConfiguration<OperationalSpfoa>, IEntityConfiguration
	{
		public OPerationalSpfoaConfiguration()
		{
			ToTable("modules.operational_spfoa");
			HasKey(t => t.Id);
		}

		public void AddConfiguration(ConfigurationRegistrar registrar)
		{
			registrar.Add(this);
		}
	}
}