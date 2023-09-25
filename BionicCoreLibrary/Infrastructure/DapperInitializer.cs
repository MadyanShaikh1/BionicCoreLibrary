using SqlKata.Compilers;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using BionicCoreLibrary.Core.GenereicRepository;
using BionicCoreLibrary.Core;

namespace BionicCoreLibrary.Infrastructure
{
    public static class DapperInitializerExtension
    {
        public static void InitializeDapper(this IServiceCollection services, string connectionString)
        {
            services.AddTransient(_ => new SqlKataQuery(new SqlConnection(connectionString), new SqlServerCompiler()));
            services.AddTransient<IDbConnection>(_ => new SqlConnection(connectionString));
            services.AddInfraStructure();
        }

        public static void AddInfraStructure(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.Scan(scan => scan.FromAssemblyDependencies(Assembly.GetExecutingAssembly())
            .AddClasses(x => x.AssignableTo(typeof(IGenericDapperRepository<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
        }
    }
}
