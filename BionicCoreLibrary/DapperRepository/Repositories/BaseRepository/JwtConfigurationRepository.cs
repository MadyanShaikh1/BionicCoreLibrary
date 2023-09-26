using BionicCoreLibrary.Core;
using BionicCoreLibrary.Core.Concrete;
using BionicCoreLibrary.Core.GenereicRepository;

namespace BionicCoreLibrary.DapperRepository.Repositries.BaseRepository
{
    public interface IJwtConfigurationRepository : IGenericDapperRepository<JwtConfiguration>
    {
    }
    public class JwtConfigurationRepository : GenericDapperRepository<JwtConfiguration>, IJwtConfigurationRepository
    {
        public JwtConfigurationRepository(SqlKataQuery sqlKataQuery) : base(sqlKataQuery)
        {

        }

    }
}
