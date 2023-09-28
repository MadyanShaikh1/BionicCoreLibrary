using BionicCoreLibrary.Common.Constant;
using BionicCoreLibrary.Core.Concrete;
using BionicCoreLibrary.DapperRepository.Repositries.BaseRepository;
using BionicCoreLibrary.DependancyInjections.TransientDependancy;
using Microsoft.IdentityModel.Tokens;
using SqlKata.Execution;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BionicCoreLibrary.Authentication
{
    public interface IJwtToken : ITransientDependancy
    {
        Task<string> GenerateJwtToken(Users user);
    }
    public class JwtToken : IJwtToken
    {
        private readonly ITenantRepository tenantRepository;
        private readonly IJwtTenantConfigurationRepository jwtTenantConfigurationRepository;
        private readonly IJwtConfigurationRepository jwtConfigurationRepository;
        private readonly IConfiguration configuration;

        public JwtToken(ITenantRepository tenantRepository,
            IJwtTenantConfigurationRepository jwtTenantConfigurationRepository,
            IJwtConfigurationRepository jwtConfigurationRepository, IConfiguration configuration)
        {
            this.tenantRepository = tenantRepository;
            this.jwtTenantConfigurationRepository = jwtTenantConfigurationRepository;
            this.jwtConfigurationRepository = jwtConfigurationRepository;
            this.configuration = configuration;
        }
        public async Task<string> GenerateJwtToken(Users user)
        {
            string? tenantName = configuration.GetConnectionString(Constants.TenantName);
            int tenantId = await tenantRepository.EntityQuery(overrideConnection: true)
                   .Select("TenantID")
                   .Where("TenantName", tenantName)
                   .FirstOrDefaultAsync<int>();

            user.TenantID = tenantId;

            int? jwtConfigurationId = await jwtTenantConfigurationRepository.EntityQuery(overrideConnection: true)
                .Select("JwtConfigurationID")
                .Where("TenantID", tenantId)
                .FirstOrDefaultAsync<int>();

            JwtConfiguration tenantConfiguration = await jwtConfigurationRepository.EntityQuery(overrideConnection: true)
                .Where("Id", jwtConfigurationId)
                .FirstOrDefaultAsync<JwtConfiguration>();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tenantConfiguration.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("UserName",user.UserName),
                new Claim("UserId", user.UserID.ToString()), // Include a tenant identifier claim
                new Claim("TenantId", user.TenantID.ToString()) // Include a tenant identifier claim
                // Add other claims as needed (e.g., roles)
            };

            var token = new JwtSecurityToken(
                issuer: tenantConfiguration.Issuer,
                audience: tenantConfiguration.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
