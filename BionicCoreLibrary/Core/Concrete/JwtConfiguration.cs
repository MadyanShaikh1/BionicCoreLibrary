namespace BionicCoreLibrary.Core.Concrete
{
    public class JwtConfiguration
    {
        public int Id { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }

    }

}
