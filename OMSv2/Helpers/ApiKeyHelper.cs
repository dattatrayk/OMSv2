using Fleetroot.Common.Helper;
using Microsoft.AspNetCore.Http;
using OMSv2.Service.DataAccess;
using OMSv2.Service.Entity;
using System;
using System.Linq;

namespace OMSv2.Service.Helpers
{
    public class ApiKeyHelper
    {
        
        public Guid ClientID { get; private set; }
        public ErrorCode ErrorCode { get; private set; }

        public  bool IsValidMasterAPIKey(string apiKey)
        {
            string masterApiKey = AppSettingProvider.GetMasterApiKey();
            return apiKey == masterApiKey;
        }
        public  bool IsValidAPI(string apiKey)
        {
            var clientID = Guid.Empty;

            if (string.IsNullOrEmpty(apiKey))
                return false;

            apiKey = apiKey.Replace(" ", "+");
            var descript = EncryptionDecryptionHelper.Decrypt(apiKey);

            var descriptArray = descript.Split('|');
            // check string array count, greater than  be 3.
            if (!(descriptArray.Count() >= 3))
                return false;

            // if guid not valid, then return false.
            if (!Guid.TryParse(descriptArray[1], out clientID))
                return false;

            //var keyRaw = EncryptionDecryptionHelper.Encrypt(descriptArray[0]);
            ClientData clientData = new ClientData();
            var keyDb = clientData.GetApiKey(clientID);
            // if guid valid, then check API is valid or not.
            if (string.Compare(apiKey, keyDb) != 0)
                return false;
            else
            {
                ClientID = clientID;
                return true;
            }
        }

        public  string GetApiKey(HttpRequest request)
        {
            if (request.Headers.ContainsKey("ApiKey"))
            {
                return request.Headers["ApiKey"].FirstOrDefault();
            }
            return string.Empty;
        }
    }
}
