using OMSv2.Service.DataAccess.Helpers;
using OMSv2.Service.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace OMSv2.Service
{
    public class SaleDetailsData
    {
        public List<SaleDetails> GetAll()
        {
            var database = DbHandler.GetDatabase();
            var saleDetailsList = new List<SaleDetails>();
            using (var command = database.GetStoredProcCommand("Select_SaleDetails"))
            {
                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    while (dataReader.Read())
                    {
                        var saleDetails = new SaleDetails();
                        saleDetails.SaleDetailsID = SafeParser.ParseInteger(dataReader["SaleDetailsID"]);
                        saleDetails.SaleID = SafeParser.ParseInteger(dataReader["SaleID"]);
                        saleDetails.ItemID = SafeParser.ParseInteger(dataReader["ItemID"]);
                        saleDetails.Price = SafeParser.ParseDouble(dataReader["Price"]);
                        saleDetails.Quantity = SafeParser.ParseInteger(dataReader["Quantity"]);
                        saleDetails.CreatedBy = SafeParser.ParseGuid(dataReader["CreatedBy"]);
                        saleDetails.SaleName = SafeParser.ParseString(dataReader["SaleName"]);
                        saleDetails.ItemName = SafeParser.ParseString(dataReader["ItemName"]);
                        saleDetails.CreatedOn = SafeParser.ParseDate(dataReader["CreatedOn"]);

                        saleDetailsList.Add(saleDetails);
                    }

                    dataReader.Close();
                    return saleDetailsList;
                }
            }
        }
        public Result Insert(SaleDetails saleDetails)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Insert_SaleDetails"))
            {
                //database.AddInParameter(command, "SaleDetailsID", DbType.Int16, saleDetails.SaleDetailsID);
                database.AddInParameter(command, "SaleID", DbType.Int16, saleDetails.SaleID);
                database.AddInParameter(command, "ItemID", DbType.Int16, saleDetails.ItemID);
                database.AddInParameter(command, "Price", DbType.Double, saleDetails.Price);
                database.AddInParameter(command, "Quantity", DbType.Int16, saleDetails.Quantity);
                database.AddInParameter(command, "CreatedBy", DbType.Guid, saleDetails.CreatedBy);
                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true };
                return new Result();
            }
        }

        public Result Update(SaleDetails saleDetails)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Update_SaleDetails"))
            {
                database.AddInParameter(command, "SaleDetailsID", DbType.Int16, saleDetails.SaleDetailsID);
                database.AddInParameter(command, "SaleID", DbType.Int16, saleDetails.SaleID);
                database.AddInParameter(command, "ItemID", DbType.Int16, saleDetails.ItemID);
                database.AddInParameter(command, "Price", DbType.Double, saleDetails.Price);
                database.AddInParameter(command, "Quantity", DbType.Int16, saleDetails.Quantity);
                database.AddInParameter(command, "ModifiedBy", DbType.Guid, saleDetails.ModifiedBy);
                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true };
                return new Result();
            }
        }
        public Result Delete(int saleDetailsID, Guid modifiedBy)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Delete_SaleDetails"))
            {
                database.AddInParameter(command, "SaleDetailsID", DbType.Int16, saleDetailsID);
                database.AddInParameter(command, "ModifiedBy", DbType.Guid, modifiedBy);

                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true };
                return new Result();
            }
        }
        public SaleDetails GetByID(int saleDetailsID)
        {
            var database = DbHandler.GetDatabase();
            var saleDetails = new SaleDetails();
            using (var command = database.GetStoredProcCommand("Get_SaleDetailsByID"))
            {
                database.AddInParameter(command, "SaleDetailsID", DbType.Int16, saleDetailsID);
                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    if (dataReader.Read())
                    {
                        saleDetails.SaleDetailsID = SafeParser.ParseInteger(dataReader["SaleDetailsID"]);
                        saleDetails.SaleID = SafeParser.ParseInteger(dataReader["SaleID"]);
                        saleDetails.ItemID = SafeParser.ParseInteger(dataReader["ItemID"]);
                        saleDetails.Price = SafeParser.ParseDouble(dataReader["Price"]);
                        saleDetails.Quantity = SafeParser.ParseInteger(dataReader["Quantity"]);
                        saleDetails.CreatedBy = SafeParser.ParseGuid(dataReader["CreatedBy"]);
                        saleDetails.ItemName = SafeParser.ParseString(dataReader["ItemName"]);
                        saleDetails.SaleName = SafeParser.ParseString(dataReader["SaleName"]);
                        saleDetails.CreatedOn = SafeParser.ParseDate(dataReader["CreatedOn"]);
                    }
                    dataReader.Close();
                    return saleDetails;
                }
            }
        }

        
    }
}

