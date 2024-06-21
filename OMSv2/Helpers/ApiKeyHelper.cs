using OMSv2.Service.Entity;
using System.Linq;
using System.Net.Http;

namespace OMSv2.Service.Helpers
{
    public class ApiKeyHelper
    {
        public ErrorCode ErrorCode { get; private set; }
        public bool IsValidAPIKey(string apiKey)
        {
            return true;
        }

        public string GetApiKey(HttpRequestMessage requestMessage)
        {
            var apiKey = string.Empty;
            if (requestMessage.Headers.Contains("apiKey"))
                apiKey = requestMessage.Headers.GetValues("apiKey")?.FirstOrDefault();
            return apiKey;
        }
    }
}
