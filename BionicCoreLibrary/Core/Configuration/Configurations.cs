
namespace BionicCoreLibrary.Core.Configuration
{
    public class Configurations
    {
        public TenantConfiguration TenantConfiguration { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public BionicAuthConnectionStrings BionicAuthConnectionStrings { get; set; }

    }

    public record TenantConfiguration
    {
        public string TenantName { get; set; }
    }

    public record ConnectionStrings
    {
        public string AppDataBase { get; set; }
    }

    public record BionicAuthConnectionStrings
    {
        public string BionicAuthDb { get; set; }
    }
}
