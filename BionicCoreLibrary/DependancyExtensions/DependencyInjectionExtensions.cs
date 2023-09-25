using System.Reflection;
using BionicCoreLibrary.Core.GenereicRepository;

public static class DependencyInjectionExtensions
{
    public static void AddScopedImplementations(this IServiceCollection services, Assembly assembly)
    {
        var repositoryTypes = assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && type.GetInterfaces().Any(IsGenericRepository));

        foreach (var repositoryType in repositoryTypes)
        {
            var genericInterface = repositoryType.GetInterfaces().First(IsGenericRepository);
            var entityType = genericInterface.GetGenericArguments().First();
            var genericRepositoryType = typeof(IGenericDapperRepository<>).MakeGenericType(entityType);

            services.AddScoped(genericRepositoryType, repositoryType);
        }

    }
    private static bool IsGenericRepository(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IGenericDapperRepository<>);
    }
}
