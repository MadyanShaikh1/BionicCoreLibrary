using BionicCoreLibrary.Common.Constant;
using BionicCoreLibrary.Core.Concrete;
using BionicCoreLibrary.Core.Configuration;
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
        public static async void Authentication(this IApplicationBuilder applicationBuilder, IServiceCollection serviceDescriptors,
            Configurations configuration)
         {
            JwtConfiguration tenantConfiguration = await TenantConfigurations(applicationBuilder, serviceDescriptors, configuration);

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

            serviceDescriptors.BuildServiceProvider();
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
