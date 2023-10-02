using BionicCoreLibrary.Common.Constant;
using BionicCoreLibrary.Core.Concrete;
using BionicCoreLibrary.Core.Configuration;
using BionicCoreLibrary.DapperRepository.Repositries.BaseRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
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
            Configurations configuration)
        {
            serviceDescriptors.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Use your authentication scheme, e.g., JwtBearerDefaults.AuthenticationScheme
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;    // Use the same authentication scheme for challenges
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.JwtSettings.Issuer,
                    ValidAudience = configuration.JwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.JwtSettings.SecretKey))
                };
            });
        }

        private static async Task<JwtConfiguration> TenantConfigurations(IApplicationBuilder applicationBuilder, IServiceCollection serviceDescriptors, Configurations configuration)
        {
            ITenantRepository? tenantDapperRepository = applicationBuilder.ApplicationServices
                .GetService<ITenantRepository>();

            IJwtTenantConfigurationRepository? jwtTenantConfiguration = applicationBuilder.ApplicationServices
                .GetService<IJwtTenantConfigurationRepository>();

            IJwtConfigurationRepository? jwtConfiguration = applicationBuilder.ApplicationServices
                .GetService<IJwtConfigurationRepository>();

            var tenantName = configuration.TenantConfiguration.TenantName;

            int tenantId = await tenantDapperRepository.EntityQuery(overrideConnection: true)
                   .Select("TenantID")
                   .Where("TenantName", tenantName)
                   .FirstOrDefaultAsync<int>();

            int? jwtConfigurationId = await jwtTenantConfiguration.EntityQuery(overrideConnection: true)
                .Select("JwtConfigurationID")
                .Where("TenantID", tenantId)
                .FirstOrDefaultAsync<int>();

            JwtConfiguration tenantConfiguration = await jwtConfiguration.EntityQuery(overrideConnection: true)
                .Where("Id", jwtConfigurationId)
                .FirstOrDefaultAsync<JwtConfiguration>();
            return tenantConfiguration;
        }
    }
}
