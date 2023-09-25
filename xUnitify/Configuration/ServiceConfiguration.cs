using BionicCoreLibrary.Core.GenereicRepository;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class ServiceConfiguration
{
    public static void Configure(IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyDependencies(Assembly.GetExecutingAssembly())// Replace with your assembly
            .AddClasses(classes => classes.AssignableTo(typeof(IGenericDapperRepository<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
    }
}
