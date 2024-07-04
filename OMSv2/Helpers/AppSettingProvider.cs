using Microsoft.Extensions.Configuration;

namespace OMSv2.Service.Helpers
{
    public static class AppSettingProvider
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public static string GetEncryptionKey()
        {
            return _configuration["AppSettings:EncryptionKey"];
        }
        public static string GetMasterApiKey()
        {
            return _configuration["AppSettings:MasterApiKey"];
        }
        public static string GetSecretKey()
        {
            return _configuration["JwtSettings:SecretKey"];
        }
        public static string GetIssuer()
        {
            return _configuration["JwtSettings:Issuer"];
        }
        public static string GetAudience()
        {
            return _configuration["JwtSettings:Audience"];
        }
    }
}
