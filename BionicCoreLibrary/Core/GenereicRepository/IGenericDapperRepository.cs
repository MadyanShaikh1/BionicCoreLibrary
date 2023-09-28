using BionicCoreLibrary.Core;
using BionicCoreLibrary.Core.GenereicRepository;
using SqlKata;

namespace BionicCoreLibrary.Core.GenereicRepository
{
    public interface IGenericDapperRepository<TEntity> where TEntity : class
    {
        Query EntityQuery(bool overrideConnection = false);
    }
}
public class GenericDapperRepository<TEntity> : IGenericDapperRepository<TEntity> where TEntity : class
{
    private readonly BionicSqlKataConnecton BionicSqlKataConnecton;
    private readonly SecondarySqlKataConnection SecondarySqlKataConnection;

    public GenericDapperRepository(BionicSqlKataConnecton bionicSqlKataConnecton,
        SecondarySqlKataConnection secondarySqlKataConnection)
    {
        this.BionicSqlKataConnecton = bionicSqlKataConnecton;
        this.SecondarySqlKataConnection = secondarySqlKataConnection;
    }
    public Query EntityQuery(bool overrideConnection)
    {
        return overrideConnection ?
            BionicSqlKataConnecton.Query(typeof(TEntity).Name.ToString()) :
            SecondarySqlKataConnection.Query(typeof(TEntity).Name.ToString());
    }
}
