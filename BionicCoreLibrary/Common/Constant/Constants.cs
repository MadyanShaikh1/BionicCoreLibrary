namespace BionicCoreLibrary.Common.Constant
{
    public class Constants
    {
        #region Bionic configuation
        public const string BionicAuthConnection = "BionicAuthConnectionStrings";
        public const string BionicAuthDataBase = "BionicAuthDb";
        public const string ConnectionStrings = "ConnectionStrings";
        #endregion

        #region Reponse Messages
        public const string Success = "Success";
        public const string RecordNotFound = "Record does not exists";
        public const string AuthenticationFailed = "Authentication Failed";
        public const string PassworIncorrect = "Incorrect Password";
        public const string UserNameIncorrect = "Incorrect Username/Email";
        public const string Unauthorize = "Access Unauthorized";
        #endregion

        #region TenantConfiguration
        public const string DataBase = "AppDataBase";
        public const string TenantConfiguration = "TenantConfiguration";
        public const string TenantName = "TenantName";
        #endregion

    }
}
