using OMSv2.Service.Helpers;
using OMSv2.Service.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace OMSv2.Service
{
    public class CategoryData
    {
        
        public List<Category> GetAll(Guid clientID)
        {
            var database = DbHandler.GetDatabase();
            var categoryList = new List<Category>();
            using (var command = database.GetStoredProcCommand("Select_Category"))
            {
                database.AddInParameter(command, "ClientID ", DbType.Guid, clientID);

                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    while (dataReader.Read())
                    {
                        var category = new Category();
                        category.CategoryName = SafeParser.ParseString(dataReader["CategoryName"]);
                        category.CategoryID = SafeParser.ParseInteger(dataReader["CategoryID"]);
                        category.CreatedOn = SafeParser.ParseDate(dataReader["CreatedOn"]);

                        categoryList.Add(category);
                    }

                    dataReader.Close();
                    return categoryList;
                }
            }
        }
        public Result Insert(Category category)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Insert_Category"))
            {
                //database.AddInParameter(command, "CategoryID ", DbType.Int32, category.CategoryID);
                database.AddInParameter(command, "ClientID ", DbType.Guid, category.ClientID);
                database.AddInParameter(command, "CategoryName", DbType.String, category.CategoryName);
                database.AddInParameter(command, "CreatedBy", DbType.Guid, category.CreatedBy);
                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true, ID = category.CategoryID };
                return new Result();
            }
        }

        public Result Update(Category category)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Update_Category"))
            {
                database.AddInParameter(command, "CategoryID ", DbType.Int32, category.CategoryID);
                database.AddInParameter(command, "CategoryName", DbType.String, category.CategoryName);
                database.AddInParameter(command, "ModifiedBy", DbType.Guid, category.ModifiedBy);
                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true };
                return new Result();
            }
        }
        public Result Delete(int categoryID, Guid modifiedBy)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Delete_Category"))
            {
                database.AddInParameter(command, "CategoryID", DbType.Int32, categoryID);
                database.AddInParameter(command, "ModifiedBy", DbType.Guid, modifiedBy);

                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true };
                return new Result();
            }
        }
        public Category GetByID(int categoryID)
        {
            var database = DbHandler.GetDatabase();
            var category = new Category();
            using (var command = database.GetStoredProcCommand("Get_CategoryByID"))
            {
                database.AddInParameter(command, "CategoryID", DbType.Int32, categoryID);
                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    if (dataReader.Read())
                    {
                        category.CategoryID = SafeParser.ParseInteger(dataReader["CategoryID"]);
                        category.CategoryName = SafeParser.ParseString(dataReader["CategoryName"]);
                        category.CreatedBy = SafeParser.ParseGuid(dataReader["CreatedBy"]);
                        category.CreatedOn = SafeParser.ParseDate(dataReader["CreatedOn"]);
                    }
                    dataReader.Close();
                    return category;
                }
            }
        }


    }
}

