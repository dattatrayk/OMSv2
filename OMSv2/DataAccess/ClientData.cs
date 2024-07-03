using OMSv2.Service.Helpers;
using System;
using System.Data;

namespace OMSv2.Service.DataAccess
{
    public class ClientData
    {
        public string GetApiKey(Guid clientID)
        {
            var database = DbHandler.GetDatabase();
            string apiKey = string.Empty;
            using (var command = database.GetStoredProcCommand("Get_ApiKey"))
            {
                database.AddInParameter(command, "ClientID", DbType.Guid, clientID);
                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    if (dataReader.Read())
                    {
                        apiKey = SafeParser.ParseString(dataReader["ApiKey"]);
                    }
                    dataReader.Close();
                    return apiKey;
                }
            }
        }
    }
}
