using BionicCoreLibrary.Core.Configuration;

namespace BionicCoreLibrary.Core.CoreServices
{
        public class ConfigurationService
        {
            public Configurations LoadConfigurations()
            {
                var configurations = new Configurations();

                var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

                configuration.Bind(configurations);

                return configurations;
            }

        }
}
