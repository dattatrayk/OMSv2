using OMSv2.Service.DataAccess.Helpers;
using OMSv2.Service.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace OMSv2.Service
{
    public class ItemData
    {
        public List<Item> GetAll()
        {
            var database = DbHandler.GetDatabase();
            var itemList = new List<Item>();
            using (var command = database.GetStoredProcCommand("Select_Item"))
            {
                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    while (dataReader.Read())
                    {
                        var item = new Item();
                        item.ItemID = SafeParser.ParseInteger(dataReader["ItemID"]);
                        item.Name = SafeParser.ParseString(dataReader["Name"]);
                        item.Description = SafeParser.ParseString(dataReader["Description"]);
                        item.price = SafeParser.ParseDouble(dataReader["price"]);
                        item.ImgURL = SafeParser.ParseString(dataReader["ImgURL"]);
                        item.stock = SafeParser.ParseInteger(dataReader["stock"]);
                        item.CategoryID = SafeParser.ParseInteger(dataReader["CategoryID"]);
                        item.BrandID = SafeParser.ParseInteger(dataReader["BrandID"]);
                        //item.CreatedName = SafeParser.ParseString(dataReader["CreatedName"]);
                        item.BrandName = SafeParser.ParseString(dataReader["BrandName"]);
                        item.CategoryName = SafeParser.ParseString(dataReader["CategoryName"]);
                        item.CreatedOn = SafeParser.ParseDate(dataReader["CreatedOn"]);
                        itemList.Add(item);
                    }

                    dataReader.Close();
                    return itemList;
                }
            }
        }
        public Result Insert(Item item)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Insert_Item"))
            {
                //database.AddInParameter(command, "ItemID", DbType.Int16, item.ItemID);
                database.AddInParameter(command, "Name", DbType.String, item.Name);
                database.AddInParameter(command, "Description", DbType.String, item.Description);
                database.AddInParameter(command, "price", DbType.Double, item.price);
                database.AddInParameter(command, "ImgURL", DbType.String, item.ImgURL);
                database.AddInParameter(command, "stock", DbType.Int16, item.stock);
                database.AddInParameter(command, "CategoryID", DbType.Int16, item.CategoryID);
                database.AddInParameter(command, "BrandID", DbType.Int16, item.BrandID);
                database.AddInParameter(command, "CreatedBy", DbType.Guid, item.CreatedBy);
                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true };
                return new Result();
            }
        }

        public Result Update(Item item)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Update_Item"))
            {
                database.AddInParameter(command, "ItemID", DbType.Int16, item.ItemID);
                database.AddInParameter(command, "Name", DbType.String, item.Name);
                database.AddInParameter(command, "Description", DbType.String, item.Description);
                database.AddInParameter(command, "price", DbType.Double, item.price);
                database.AddInParameter(command, "ImgURL", DbType.String, item.ImgURL);
                database.AddInParameter(command, "stock", DbType.Int16, item.stock);
                database.AddInParameter(command, "CategoryID", DbType.Int16, item.CategoryID);
                database.AddInParameter(command, "BrandID", DbType.Int16, item.BrandID);
                database.AddInParameter(command, "ModifiedBy", DbType.Guid, item.ModifiedBy);
                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true };
                return new Result();
            }
        }
        public Result Delete(int itemID, Guid modifiedBy)
        {
            var database = DbHandler.GetDatabase();
            using (var command = database.GetStoredProcCommand("Delete_Item"))
            {
                database.AddInParameter(command, "ItemID", DbType.Int16, itemID);
                database.AddInParameter(command, "ModifiedBy", DbType.Guid, modifiedBy);

                int outValue = database.ExecuteNonQuery(command);
                if (outValue > 0)
                    return new Result() { IsValid = true };
                return new Result();
            }
        }
        public Item GetByID(int itemID)
        {
            var database = DbHandler.GetDatabase();
            var item = new Item();
            using (var command = database.GetStoredProcCommand("Get_ItemByID"))
            {
                database.AddInParameter(command, "ItemID", DbType.Int16, itemID);
                using (IDataReader dataReader = database.ExecuteReader(command))
                {
                    if (dataReader.Read())
                    {
                        item.Name = SafeParser.ParseString(dataReader["Name"]);
                        item.Description = SafeParser.ParseString(dataReader["Description"]);
                        item.price = SafeParser.ParseDouble(dataReader["price"]);
                        item.ImgURL = SafeParser.ParseString(dataReader["ImgURL"]);
                        item.stock = SafeParser.ParseInteger(dataReader["stock"]);
                        item.CategoryID = SafeParser.ParseInteger(dataReader["CategoryID"]);
                        item.BrandID = SafeParser.ParseInteger(dataReader["BrandID"]);
                        item.CreatedBy = SafeParser.ParseGuid(dataReader["CreatedBy"]);
                        item.CreatedOn = SafeParser.ParseDate(dataReader["CreatedOn"]);
                        item.BrandName = SafeParser.ParseString(dataReader["BrandName"]);
                        item.CategoryName = SafeParser.ParseString(dataReader["CategoryName"]);
                    }
                    dataReader.Close();
                    return item;
                }
            }
        }

        
    }
}

