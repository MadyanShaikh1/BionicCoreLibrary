using BionicCoreLibrary.Common.Constant;
using BionicCoreLibrary.Core.Concrete;
using BionicCoreLibrary.DapperRepository.Repositries.BaseRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SqlKata.Execution;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;

namespace BionicCoreLibrary.Authentication
{
    public static class Authentications
    {
        public static async void Authentication(this IServiceCollection serviceDescriptors,
            IConfiguration configuration)
        {
            JwtConfiguration tenantConfiguration = await TenantConfigurations(serviceDescriptors, configuration);

            serviceDescriptors.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tenantConfiguration.Issuer,
                    ValidAudience = tenantConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tenantConfiguration.Key))
                };
            });

        }
        private static async Task<JwtConfiguration> TenantConfigurations(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            ITenantRepository? tenantDapperRepository = serviceDescriptors.BuildServiceProvider()
                .GetService<ITenantRepository>();

            IJwtTenantConfigurationRepository? jwtTenantConfiguration = serviceDescriptors.BuildServiceProvider()
                .GetService<IJwtTenantConfigurationRepository>();

            IJwtConfigurationRepository? jwtConfiguration = serviceDescriptors.BuildServiceProvider()
                .GetService<IJwtConfigurationRepository>();

            string? tenantName = configuration.GetConnectionString(Constants.TenantName);

            int tenantId = await tenantDapperRepository.EntityQuery()
                   .Select("TenantID")
                   .Where("TenantName", tenantName)
                   .FirstOrDefaultAsync<int>();

            int? jwtConfigurationId = await jwtTenantConfiguration.EntityQuery()
                .Select("JwtConfigurationID")
                .Where("TenantID", tenantId)
                .FirstOrDefaultAsync<int>();

            JwtConfiguration tenantConfiguration = await jwtConfiguration.EntityQuery()
                .Where("Id", jwtConfigurationId)
                .FirstOrDefaultAsync<JwtConfiguration>();
            return tenantConfiguration;
        }
    }
}
