using OMSv2.Service.Entity;
using OMSv2.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Data;

namespace OMSv2.Service
{
    public class CustomerData
    {
        public List<Customer> GetAll(CustomerFilterParameter parameter)
        {
            var database = DbHandler.GetDatabase();
            var customerList = new List<Customer>();
            using (var command = database.GetStoredProcCommand("Select_Customer"))
            {
                database.AddInParameter(command, "ClientID", DbType.Guid, parameter.ClientID);

                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    while (dataReader.Read())
                    {
                        var customer = new Customer();
                        customer.CustomerID = SafeParser.ParseInteger(dataReader["CustomerID"]);
                        customer.Name = SafeParser.ParseString(dataReader["Name"]);
                        customer.ContactNo = SafeParser.ParseString(dataReader["ContactNo"]);
                        customer.Email = SafeParser.ParseString(dataReader["Email"]);
                        customer.AddressDetails = JsonParser.GetObject<AddressDetails>(SafeParser.ParseString(dataReader["AddressDetails"]));
                        customer.CreatedBy = SafeParser.ParseGuid(dataReader["CreatedBy"]);
                        customer.CreatedOn = SafeParser.ParseDate(dataReader["CreatedOn"]);

                        customerList.Add(customer);
                    }

                    dataReader.Close();
                    return customerList;
                }
            }
        }
        public Result Insert(Customer customer)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Insert_Customer"))
            {
                //database.AddInParameter(command, "CustomerID", DbType.Int32, customer.CustomerID);
                database.AddInParameter(command, "ClientID", DbType.Guid, customer.ClientID);
                database.AddInParameter(command, "Name", DbType.String, customer.Name);
                database.AddInParameter(command, "ContactNo", DbType.String, customer.ContactNo);
                database.AddInParameter(command, "Email", DbType.String, customer.Email);
                database.AddInParameter(command, "AddressDetails", DbType.String, JsonParser.GetJson(customer.AddressDetails));
                database.AddInParameter(command, "CreatedBy", DbType.Guid, customer.CreatedBy);
                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true };
                return new Result();
            }
        }

        public Result Update(Customer customer)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Update_Customer"))
            {
                database.AddInParameter(command, "CustomerID", DbType.Int32, customer.CustomerID);
                database.AddInParameter(command, "Name", DbType.String, customer.Name);
                database.AddInParameter(command, "ContactNo", DbType.String, customer.ContactNo);
                database.AddInParameter(command, "Email", DbType.String, customer.Email);
                database.AddInParameter(command, "AddressDetails", DbType.String, JsonParser.GetJson(customer.AddressDetails));
                database.AddInParameter(command, "ModifiedBy", DbType.Guid, customer.ModifiedBy);
                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true };
                return new Result();
            }
        }
        public Result Delete(int customerID, Guid modifiedBy)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Delete_Customer"))
            {
                database.AddInParameter(command, "CustomerID", DbType.Int32, customerID);
                database.AddInParameter(command, "ModifiedBy", DbType.Guid, modifiedBy);

                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true };
                return new Result();
            }
        }
        public Customer GetByID(int customerID)
        {
            var database = DbHandler.GetDatabase();
            var customer = new Customer();
            using (var command = database.GetStoredProcCommand("Get_CustomerByID"))
            {
                database.AddInParameter(command, "CustomerID", DbType.Int32, customerID);
                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    if (dataReader.Read())
                    {
                        customer.Name = SafeParser.ParseString(dataReader["Name"]);
                        customer.ContactNo = SafeParser.ParseString(dataReader["ContactNo"]);
                        customer.Email = SafeParser.ParseString(dataReader["Email"]);
                        customer.AddressDetails = JsonParser.GetObject<AddressDetails>(SafeParser.ParseString(dataReader["AddressDetails"]));
                        customer.CreatedBy = SafeParser.ParseGuid(dataReader["CreatedBy"]);
                        customer.CreatedOn = SafeParser.ParseDate(dataReader["CreatedOn"]);
                    }
                    dataReader.Close();
                    return customer;
                }
            }
        }


    }
}

