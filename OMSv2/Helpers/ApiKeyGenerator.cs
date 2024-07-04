using Fleetroot.Common.Helper;
using System;
using System.Linq;

namespace OMSv2.Service.Helpers
{
    public class ApiKeyGenerator
    {
        const int length = 4;
        static Random random = new Random();

        public static string GetKey(Guid clientID,string Name)
        {
            var combineCode = string.Join("|", RandomString(), clientID, Name, RandomString());
            var code = EncryptionDecryptionHelper.Encrypt(combineCode);
            return code;
        }

        private static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
