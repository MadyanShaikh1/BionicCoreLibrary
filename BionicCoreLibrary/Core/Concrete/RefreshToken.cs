namespace BionicCoreLibrary.Core.Concrete
{
    public class RefreshTokens
    {
        public int RefreshTokenID { get; set; }
        public string Token { get; set; }
        public int UserID { get; set; }
        public int TenantID { get; set; }
    }

}
