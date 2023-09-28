using SqlKata.Compilers;
using System.Data;
using System.Data.SqlClient;
using BionicCoreLibrary.Core;
using BionicCoreLibrary.Core.Configuration;

namespace BionicCoreLibrary.Infrastructure
{
    public static class DapperInitializerExtension
    {
        public static void InitializeDapper(this IServiceCollection services, Configurations configurations)
        {
            services.AddTransient(_ => new BionicSqlKataConnecton(new SqlConnection(configurations.BionicAuthConnectionStrings.BionicAuthDb),
                new SqlServerCompiler()));
            services.AddTransient(_ => new SecondarySqlKataConnection(new SqlConnection(configurations.ConnectionStrings.AppDataBase),
                new SqlServerCompiler()));
            //services.AddTransient<IDbConnection>(_ => new SqlConnection(connectionString));
        }
    }
}
