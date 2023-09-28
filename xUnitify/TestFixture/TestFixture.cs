using BionicCoreLibrary.Common.Constant;
using BionicCoreLibrary.Core.Configuration;
using BionicCoreLibrary.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class TestFixture
{
    public ServiceProvider ServiceProvider { get; }
    public IConfiguration Configuration { get; }
    public Configurations Configurations { get; }
    public TestFixture()
    {
        Configuration = new ConfigurationBuilder()
       .SetBasePath(AppContext.BaseDirectory) // Adjust the path as needed
       .AddJsonFile("appsettings.json") // Adjust the file name as needed
       .Build();

        string connectionStrings = Configuration.GetConnectionString(Constants.DataBase);
        var services = new ServiceCollection();

        // Configure your services with Scrutor
        services.InitializeDapper(Configurations);
        services.AddInfraStructure();
        ServiceConfiguration.Configure(services);

        ServiceProvider = services.BuildServiceProvider();
    }

}
