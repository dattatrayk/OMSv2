//using OMSv2.Service.Entity;
//using OMSv2.Service.Helpers;
//using System;
//using System.Collections.Generic;
//using System.Data;


//namespace OMSv2.Service.DataAccess
//{
//    public class UserData
//    {
      
//        public Result Insert(User user)
//        {
//            var database = DbHandler.GetDatabase();
//            using (var command = database.GetStoredProcCommand("Insert_User"))
//            {
//                database.AddInParameter(command, "UserID", DbType.Int32, user.UserID);
//                database.AddInParameter(command, "UserName", DbType.String, user.UserName);
//                database.AddInParameter(command, "PasswordHash", DbType.String, user.PasswordHash);
//                database.AddInParameter(command, "IsActive", DbType.Boolean, user.IsActive);
//                database.AddInParameter(command, "ClientID", DbType.Guid, user.ClientID);
//                int outValue = database.ExecuteNonQuery(command);
//                if (outValue > 0)
//                    return new Result() { IsValid = true };
//                return new Result();
//            }
//        }

//        public Result Update(User user)
//        {
//            var database = DbHandler.GetDatabase();
//            using (var command = database.GetStoredProcCommand("Update_User"))
//            {
//                database.AddInParameter(command, "UserID", DbType.Int32, user.UserID);
//                database.AddInParameter(command, "Name", DbType.String, user.Name);
//                database.AddInParameter(command, "ContactNo", DbType.String, user.ContactNo);
//                database.AddInParameter(command, "Email", DbType.String, user.Email);
//                database.AddInParameter(command, "AddressDetails", DbType.String, JsonParser.GetJson(user.AddressDetails));
//                database.AddInParameter(command, "ModifiedBy", DbType.Guid, user.ModifiedBy);
//                int outValue = database.ExecuteNonQuery(command);
//                if (outValue > 0)
//                    return new Result() { IsValid = true };
//                return new Result();
//            }
//        }
//        public Result Delete(int userID, Guid modifiedBy)
//        {
//            var database = DbHandler.GetDatabase();
//            using (var command = database.GetStoredProcCommand("Delete_User"))
//            {
//                database.AddInParameter(command, "UserID", DbType.Int32, userID);
//                database.AddInParameter(command, "ModifiedBy", DbType.Guid, modifiedBy);

//                int outValue = database.ExecuteNonQuery(command);
//                if (outValue > 0)
//                    return new Result() { IsValid = true };
//                return new Result();
//            }
//        }
//        public User GetByID(string userName)
//        {
//            var database = DbHandler.GetDatabase();
//            var user = new User();
//            using (var command = database.GetStoredProcCommand("Get_UserByID"))
//            {
//                database.AddInParameter(command, "UserName", DbType.String, userName);
//                using (IDataReader dataReader = database.ExecuteReader(command))
//                {
//                    if (dataReader.Read())
//                    {
//                        user.UserID = SafeParser.ParseGuid(dataReader["UserID"]);
//                        user.UserName = SafeParser.ParseString(dataReader["UserName"]);
//                    }
//                    dataReader.Close();
//                    return user;
//                }
//            }
//        }


//    }

//}
