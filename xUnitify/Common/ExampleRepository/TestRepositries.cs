using BionicCoreLibrary.Core.GenereicRepository;
using BionicCoreLibrary.DapperRepository.Concrete;
using BionicCoreLibrary.DapperRepository.Repositries.BaseRepository;
using Microsoft.Extensions.DependencyInjection;
using xUnitify.Common.ExampleEntity;
using SqlKata.Execution;


namespace xUnitify.Common.ExampleRepository
{

    public class RepositriesTests : TestFixture
    {
        private readonly IGenericDapperRepository<Team> genericRepository;
        private readonly IUserDapperRepository userDapperRepository;
        //private readonly Mock<IGenericRepository<Team>> genericRepositoryMock;

        public RepositriesTests()
        {
            genericRepository = ServiceProvider.GetService<IGenericDapperRepository<Team>>();
            userDapperRepository = ServiceProvider.GetService<IUserDapperRepository>();
        }



        [Fact]
        public async Task TestRepositries()
        {
            try
            {
                var getTeamResult = (await genericRepository.GetKataQuery()
.Select("FirstName")
    .GetAsync<string>()).ToList();
            }
            catch (Exception)
            {

          
            }


        }
        [Fact]
        public async Task UserDapperRepositoryTests()
        {
            var getTeamResult = (await userDapperRepository.GetKataQuery()
                .GetAsync<AspNetUsers>()).ToList();

        }
    }
}