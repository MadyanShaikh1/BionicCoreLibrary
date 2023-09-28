using BionicCoreLibrary.Core;
using BionicCoreLibrary.Core.Concrete;
using BionicCoreLibrary.Core.GenereicRepository;

namespace BionicCoreLibrary.DapperRepository.Repositries.BaseRepository
{
    public interface IJwtTenantConfigurationRepository : IGenericDapperRepository<JwtTenantConfiguration>
    {
    }
    public class JwtTenantConfigurationRepository : GenericDapperRepository<JwtTenantConfiguration>, IJwtTenantConfigurationRepository
    {
        public JwtTenantConfigurationRepository(BionicSqlKataConnecton bionicSqlKataConnecton, SecondarySqlKataConnection secondarySqlKataConnection) : base(bionicSqlKataConnecton, secondarySqlKataConnection)
        {
        }
    }
}
