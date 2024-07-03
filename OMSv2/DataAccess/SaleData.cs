using OMSv2.Service.Entity;
using OMSv2.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Data;

namespace OMSv2.Service
{
    public class SaleData
    {
        public List<Sale> GetAll(SaleFilterParameter parameter)
        {
            var database = DbHandler.GetDatabase();
            var saleList = new List<Sale>();
            using (var command = database.GetStoredProcCommand("Select_Sale"))
            {
                database.AddInParameter(command, "ClientID", DbType.Guid, parameter.ClientID);

                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    while (dataReader.Read())
                    {
                        var sale = new Sale();
                        sale.SaleID = SafeParser.ParseInteger(dataReader["SaleID"]);
                        sale.SaleDate = SafeParser.ParseDate(dataReader["SaleDate"]);
                        sale.CustomerName = SafeParser.ParseString(dataReader["CustomerName"]);
                        sale.ContactNo = SafeParser.ParseString(dataReader["ContactNo"]);
                        sale.Email = SafeParser.ParseString(dataReader["Email"]);
                        sale.Address = SafeParser.ParseString(dataReader["Address"]);
                        sale.TotalAmount = SafeParser.ParseDouble(dataReader["TotalAmount"]);
                        sale.Quantity = SafeParser.ParseInteger(dataReader["Quantity"]);
                        sale.CreatedBy = SafeParser.ParseGuid(dataReader["CreatedBy"]);
                        sale.CreatedOn = SafeParser.ParseDate(dataReader["CreatedOn"]);

                        saleList.Add(sale);
                    }

                    dataReader.Close();
                    return saleList;
                }
            }
        }
        public Result Insert(Sale sale)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Insert_Sale"))
            {
                //database.AddInParameter(command, "SaleID", DbType.Int32, sale.SaleID);
                database.AddInParameter(command, "ClientID", DbType.Guid, sale.ClientID);
                database.AddInParameter(command, "SaleDate", DbType.DateTime, sale.SaleDate);
                database.AddInParameter(command, "CustomerID", DbType.Int32, sale.CustomerID);
                database.AddInParameter(command, "CustomerName", DbType.String, sale.CustomerName);
                database.AddInParameter(command, "ContactNo", DbType.String, sale.ContactNo);
                database.AddInParameter(command, "Email", DbType.String, sale.Email);
                database.AddInParameter(command, "Address", DbType.String, sale.Address);
                database.AddInParameter(command, "TotalAmount", DbType.Double, sale.TotalAmount);
                database.AddInParameter(command, "Quantity", DbType.Int16, sale.Quantity);
                database.AddInParameter(command, "CreatedBy", DbType.Guid, sale.CreatedBy);

                // Add output parameter for SaleID
                var saleIDParameter = command.CreateParameter();
                saleIDParameter.ParameterName = "SaleID";
                saleIDParameter.DbType = DbType.Int32;
                saleIDParameter.Direction = ParameterDirection.Output;
                command.Parameters.Add(saleIDParameter);

                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true, ID = (int)saleIDParameter.Value };
                return new Result();
            }
        }

        public Result Update(Sale sale)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Update_Sale"))
            {
                database.AddInParameter(command, "SaleID", DbType.Int32, sale.SaleID);
                database.AddInParameter(command, "SaleDate", DbType.DateTime, sale.SaleDate);
                database.AddInParameter(command, "CustomerName", DbType.String, sale.CustomerName);
                database.AddInParameter(command, "ContactNo", DbType.String, sale.ContactNo);
                database.AddInParameter(command, "Email", DbType.String, sale.Email);
                database.AddInParameter(command, "Address", DbType.String, sale.Address);
                database.AddInParameter(command, "TotalAmount", DbType.Double, sale.TotalAmount);
                database.AddInParameter(command, "Quantity", DbType.Int16, sale.Quantity);
                database.AddInParameter(command, "ModifiedBy", DbType.Guid, sale.ModifiedBy);
                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true };
                return new Result();
            }
        }
        public Result Delete(int saleID, Guid modifiedBy)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Delete_Sale"))
            {
                database.AddInParameter(command, "SaleID", DbType.Int32, saleID);
                database.AddInParameter(command, "ModifiedBy", DbType.Guid, modifiedBy);

                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true };
                return new Result();
            }
        }
        public Sale GetByID(int saleID)
        {
            var database = DbHandler.GetDatabase();
            var sale = new Sale();
            using (var command = database.GetStoredProcCommand("Get_SaleByID"))
            {
                database.AddInParameter(command, "SaleID", DbType.Int32, saleID);
                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    if (dataReader.Read())
                    {
                        sale.SaleID = saleID;
                        sale.SaleDate = SafeParser.ParseDate(dataReader["SaleDate"]);
                        sale.CustomerName = SafeParser.ParseString(dataReader["CustomerName"]);
                        sale.ContactNo = SafeParser.ParseString(dataReader["ContactNo"]);
                        sale.Email = SafeParser.ParseString(dataReader["Email"]);
                        sale.Address = SafeParser.ParseString(dataReader["Address"]);
                        sale.TotalAmount = SafeParser.ParseDouble(dataReader["TotalAmount"]);
                        sale.Quantity = SafeParser.ParseInteger(dataReader["Quantity"]);
                        sale.CreatedBy = SafeParser.ParseGuid(dataReader["CreatedBy"]);
                        sale.CreatedOn = SafeParser.ParseDate(dataReader["CreatedOn"]);
                    }
                    dataReader.NextResult();
                    while (dataReader.Read())
                    {
                        var saleDetails = new SaleDetails();
                        saleDetails.SaleDetailID = SafeParser.ParseInteger(dataReader["SaleDetailID"]);
                        saleDetails.SaleID = SafeParser.ParseInteger(dataReader["SaleID"]);
                        saleDetails.ItemID = SafeParser.ParseInteger(dataReader["ItemID"]);
                        saleDetails.Price = SafeParser.ParseDouble(dataReader["Price"]);
                        saleDetails.Quantity = SafeParser.ParseInteger(dataReader["Quantity"]);
                        saleDetails.ItemName = SafeParser.ParseString(dataReader["ItemName"]);

                        sale.SaleDetail.Add(saleDetails);
                    }
                    dataReader.Close();
                    return sale;
                }
            }
        }


    }
}

