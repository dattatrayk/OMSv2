using OMSv2.Service.Entity;
using OMSv2.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Data;

namespace OMSv2.Service
{
    public class BrandData
    {
        public List<Brand> GetAll(Guid clientID)
        {
            var database = DbHandler.GetDatabase();
            var brandList = new List<Brand>();
            using (var command = database.GetStoredProcCommand("Select_Brand"))
            {
                database.AddInParameter(command, "ClientID ", DbType.Guid, clientID);
                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    while (dataReader.Read())
                    {
                        var brand = new Brand();
                        brand.BrandName = SafeParser.ParseString(dataReader["BrandName"]);
                        brand.BrandID = SafeParser.ParseInteger(dataReader["BrandID"]);
                        brand.CreatedOn = SafeParser.ParseDate(dataReader["CreatedOn"]);
                        brandList.Add(brand);
                    }

                    dataReader.Close();
                    return brandList;
                }
            }
        }
        public Result Insert(Brand brand)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Insert_Brand"))
            {
                database.AddInParameter(command, "ClientID ", DbType.Guid, brand.ClientID);
                //database.AddInParameter(command, "BrandID ", DbType.Int32, brand.BrandID);
                database.AddInParameter(command, "BrandName", DbType.String, brand.BrandName);
                database.AddInParameter(command, "CreatedBy", DbType.Guid, brand.CreatedBy);
                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true, ID = brand.BrandID };
                return new Result();
            }
        }

        public Result Update(Brand brand)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Update_Brand"))
            {
                database.AddInParameter(command, "BrandID ", DbType.Int32, brand.BrandID);
                database.AddInParameter(command, "BrandName", DbType.String, brand.BrandName);
                database.AddInParameter(command, "ModifiedBy", DbType.Guid, brand.ModifiedBy);
                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true };
                return new Result();
            }
        }
        public Result Delete(int brandID, Guid modifiedBy)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Delete_Brand"))
            {
                database.AddInParameter(command, "BrandID", DbType.Int32, brandID);
                database.AddInParameter(command, "ModifiedBy", DbType.Guid, modifiedBy);

                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true };
                return new Result();
            }
        }
        public Brand GetByID(int brandID)
        {
            var database = DbHandler.GetDatabase();
            var brand = new Brand();
            using (var command = database.GetStoredProcCommand("Get_BrandByID"))
            {
                database.AddInParameter(command, "BrandID", DbType.Int32, brandID);
                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    if (dataReader.Read())
                    {

                        brand.BrandID = SafeParser.ParseInteger(dataReader["BrandID"]);
                        brand.BrandName = SafeParser.ParseString(dataReader["BrandName"]);
                        brand.CreatedBy = SafeParser.ParseGuid(dataReader["CreatedBy"]);
                        brand.CreatedOn = SafeParser.ParseDate(dataReader["CreatedOn"]);
                    }
                    dataReader.Close();
                    return brand;
                }
            }
        }


    }
}

