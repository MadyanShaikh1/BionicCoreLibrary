using BionicCoreLibrary.Core;
using BionicCoreLibrary.Core.GenereicRepository;
using BionicCoreLibrary.DapperRepository.Concrete;

namespace BionicCoreLibrary.DapperRepository.Repositries.BaseRepository
{
    public interface IUserDapperRepository : IGenericDapperRepository<AspNetUsers>
    {
    }
    public class UserDapperRepository : GenericDapperRepository<AspNetUsers>, IUserDapperRepository
    {
        public UserDapperRepository(SqlKataQuery sqlKataQuery) : base(sqlKataQuery)
        {

        }

    }
}
