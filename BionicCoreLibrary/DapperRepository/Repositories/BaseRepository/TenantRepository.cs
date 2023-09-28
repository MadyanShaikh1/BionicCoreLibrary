using BionicCoreLibrary.Core;
using BionicCoreLibrary.Core.Concrete;
using BionicCoreLibrary.Core.GenereicRepository;

namespace BionicCoreLibrary.DapperRepository.Repositries.BaseRepository
{
    public interface ITenantRepository : IGenericDapperRepository<Tenants>
    {
    }
    public class TenantRepository : GenericDapperRepository<Tenants>, ITenantRepository
    {
        public TenantRepository(BionicSqlKataConnecton bionicSqlKataConnecton, SecondarySqlKataConnection secondarySqlKataConnection) : base(bionicSqlKataConnecton, secondarySqlKataConnection)
        {
        }
    }
}
