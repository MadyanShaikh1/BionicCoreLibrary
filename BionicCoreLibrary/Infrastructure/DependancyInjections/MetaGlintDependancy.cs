using BionicCoreLibrary.Common.Helper;
using BionicCoreLibrary.Core.Configuration;
using BionicCoreLibrary.Core.Sessions;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace BionicCoreLibrary.Infrastructure.DependancyInjections
{
    public class MetaGlintDependancy
    {
        public readonly Configurations Configurations;

        public CurrentSession? Session;

        public MetaGlintDependancy(IHttpContextAccessor httpContext, Configurations configurations)
        {
            var _httpContextProperties = httpContext.HttpContext.User.Claims
                .Select(x => x.Properties)
                .FirstOrDefault();

            Configurations = configurations;
            LoadSession(_httpContextProperties);

        }

        public void LoadSession(IDictionary<string, string>? keyValuePairs)
        {
            if (keyValuePairs != null)
            {
                Session.UserId = Convert.ToInt32(keyValuePairs["UserId"]);
                Session.UserName = keyValuePairs["UserName"];
                Session.TenantId = Convert.ToInt32(keyValuePairs["TenantId"]);
            }
        }

    }
}
