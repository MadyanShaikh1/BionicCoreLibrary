using BCrypt.Net;
using BionicCoreLibrary.Authentication;
using BionicCoreLibrary.Core.Concrete;
using BionicCoreLibrary.DapperRepository.Repositries.BaseRepository;
using SqlKata.Execution;
using BCrypt.Net;
using System.Reflection.Metadata;
using BionicCoreLibrary.Common.Constant;
using BionicCoreLibrary.Infrastructure.DependancyInjections.TransientDependancy;

namespace BionicCoreLibrary.Core.CoreServices
{
    public interface IAuthenticationService : ITransientDependancy
    {
        Task<string> Authenticate(Users users);
    }
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserDapperRepository userDapperRepository;
        private readonly IJwtToken jwtToken;

        public AuthenticationService(IUserDapperRepository userDapperRepository, IJwtToken jwtToken)
        {
            this.userDapperRepository = userDapperRepository;
            this.jwtToken = jwtToken;
        }
        public async Task<string> Authenticate(Users users)
        {
            var userDetails = await userDapperRepository.EntityQuery(overrideConnection: true)
                .Where("UserName", users.Email.Trim())
                .OrWhere("Email", users.Email.Trim())
                .FirstOrDefaultAsync<Users>();

            if (userDetails == null)
                return string.Concat(Constants.AuthenticationFailed, ", ", Constants.RecordNotFound);

            bool isUserAuthenticated = isUserAuthenticated = BCrypt.Net.BCrypt.Verify(users.Password, userDetails.Password);

            if (!isUserAuthenticated)
                return string.Concat(Constants.AuthenticationFailed, ", ", Constants.PassworIncorrect);

            string? generateToken = await jwtToken.GenerateJwtToken(userDetails);

            return generateToken;
        }
    }
}
