using BionicCoreLibrary.Core.GenereicRepository;
using BionicCoreLibrary.DapperRepository.Repositries.BaseRepository;
using Microsoft.Extensions.DependencyInjection;
using xUnitify.Common.ExampleEntity;
using SqlKata.Execution;
using BionicCoreLibrary.Core.Concrete;

namespace xUnitify.Common.ExampleRepository
{

    public class RepositriesTests : TestFixture
    {
        private readonly IGenericDapperRepository<Team> genericRepository;
        private readonly IUserDapperRepository userDapperRepository;

        public RepositriesTests()
        {
            genericRepository = ServiceProvider.GetService<IGenericDapperRepository<Team>>();
            userDapperRepository = ServiceProvider.GetService<IUserDapperRepository>();
        }
        [Fact]
        public async Task TestRepositries()
        {
            var getTeamResult = (await genericRepository.EntityQuery()
            .Select("FirstName")
            .GetAsync<string>()).ToList();
        }
        [Fact]
        public async Task UserDapperRepositoryTests()
        {
            var getTeamResult = (await userDapperRepository.EntityQuery()
                .GetAsync<Users>()).ToList();
        }
    }
}