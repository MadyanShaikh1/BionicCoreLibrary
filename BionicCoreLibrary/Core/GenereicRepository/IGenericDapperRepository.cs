using BionicCoreLibrary.Core;
using BionicCoreLibrary.Core.GenereicRepository;
using SqlKata;

namespace BionicCoreLibrary.Core.GenereicRepository
{
    public interface IGenericDapperRepository<TEntity> where TEntity : class
    {
        Query GetKataQuery();
    }
}
public class GenericDapperRepository<TEntity> : IGenericDapperRepository<TEntity> where TEntity : class
{
    private readonly SqlKataQuery sqlKataQuery;

    public GenericDapperRepository(SqlKataQuery sqlKataQuery)
    {
        this.sqlKataQuery = sqlKataQuery;
    }

    public Query GetKataQuery()
    {
        return sqlKataQuery.Query(typeof(TEntity).Name.ToString());
    }
}
