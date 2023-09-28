﻿using System.Reflection;
using BionicCoreLibrary.Core.GenereicRepository;
using BionicCoreLibrary.DependancyInjections.ScopedDependancy;
using BionicCoreLibrary.DependancyInjections.SingletonDependancy;
using BionicCoreLibrary.DependancyInjections.TransientDependancy;

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
    public static void AddInfraStructure(this IServiceCollection serviceDescriptors)
    {
        serviceDescriptors.Scan(scan => scan.FromAssemblyDependencies(Assembly.GetExecutingAssembly())
        .AddClasses(x => x.AssignableTo(typeof(IGenericDapperRepository<>)))
        .AsImplementedInterfaces()
        .WithTransientLifetime());

        serviceDescriptors.Scan(scan => scan.FromAssemblyDependencies(Assembly.GetExecutingAssembly())
        .AddClasses(x => x.AssignableTo(typeof(ITransientDependancy)))
        .AsImplementedInterfaces()
        .WithTransientLifetime());

        serviceDescriptors.Scan(scan => scan.FromAssemblyDependencies(Assembly.GetExecutingAssembly())
        .AddClasses(x => x.AssignableTo(typeof(IScopedDependancy)))
        .AsImplementedInterfaces()
        .WithTransientLifetime());

        serviceDescriptors.Scan(scan => scan.FromAssemblyDependencies(Assembly.GetExecutingAssembly())
        .AddClasses(x => x.AssignableTo(typeof(ISingletonDependancy)))
        .AsImplementedInterfaces()
        .WithTransientLifetime());


    }
}
