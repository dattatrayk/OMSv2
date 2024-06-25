using Newtonsoft.Json;

namespace OMSv2.Service.Helpers
{
    public class JsonParser
    {
        /// <summary>
        /// Json to object parser
        /// </summary>
        public static T GetObject<T>(object value) where T : new()
        {
            try
            {
                string json = SafeParser.ParseString(value);
                if (!string.IsNullOrEmpty(json))
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch
            {
            }
            return new T();
        }

        /// <summary>
        /// Object to Json parser
        /// </summary>
        public static string GetJson<T>(T t)
        {
            try
            {
                if (t != null)
                {
                    return JsonConvert.SerializeObject(t);
                }
            }
            catch
            {
            }
            return string.Empty;
        }
    }
}
