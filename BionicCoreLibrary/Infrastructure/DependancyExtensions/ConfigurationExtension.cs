using BionicCoreLibrary.Core.Configuration;
using BionicCoreLibrary.Core.CoreServices;
using System.Configuration;

namespace BionicCoreLibrary.Infrastructure.DependancyExtensions
{
    public static class ConfigurationExtension
    {
        public static Configurations LoadConfiguration(this IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            var configurations = new Configurations();
            configuration.Bind(configurations);
            serviceDescriptors.AddSingleton(configurations);

            return configurations;
        }
    }
}
