using Microsoft.AspNetCore.Http.Extensions;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data;
using System.Data.Common;

namespace BionicCoreLibrary.Core
{
    public class BionicSqlKataConnecton : QueryFactory
    {
        public IDbConnection DbConnection;
        public BionicSqlKataConnecton(IDbConnection dbConnection, SqlServerCompiler compiler) : base(dbConnection, compiler)
        {
            DbConnection = dbConnection;
        }
    }
    public class SecondarySqlKataConnection : QueryFactory
    {
        public IDbConnection DbConnection;
        public SecondarySqlKataConnection(IDbConnection dbConnection, SqlServerCompiler compiler) : base(dbConnection, compiler)
        {
            DbConnection = dbConnection;
        }
    }
}
