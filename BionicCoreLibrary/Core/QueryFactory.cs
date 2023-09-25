using Microsoft.AspNetCore.Http.Extensions;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data;

namespace BionicCoreLibrary.Core
{
    public class SqlKataQuery : QueryFactory
    {
        public IDbConnection DbConnection { get; }
        public SqlKataQuery(IDbConnection dbConnection, SqlServerCompiler compiler) : base(dbConnection, compiler)
        {
            DbConnection = dbConnection;
        }

    }
}
