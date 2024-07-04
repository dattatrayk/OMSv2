using OMSv2.Service.Entity;
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

        public Result Insert(Client client)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Insert_Client"))
            {
                database.AddInParameter(command, "ClientID", DbType.Guid, client.ClientID);
                database.AddInParameter(command, "Name", DbType.String, client.Name);
                database.AddInParameter(command, "ContactNo", DbType.String, client.ContactNo);
                database.AddInParameter(command, "Email", DbType.String, client.Email);
                database.AddInParameter(command, "Address", DbType.String, client.Address);
                database.AddInParameter(command, "ApiKey", DbType.String, client.ApiKey);
                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true, Code = client.ApiKey };
                return new Result();
            }
        }
    }
}
