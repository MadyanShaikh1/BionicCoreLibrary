namespace BionicCoreLibrary.Infrastructure.DependancyInjections.SharedDependancy
{
    public static class SharedDependancies
    {

        public static void SharedDependancy(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddHttpContextAccessor();
        }
    }
}
