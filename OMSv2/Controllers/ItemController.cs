using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OMSv2.Service.Entity;
using OMSv2.Service.Helpers;
using OMSv2.Service.Models;

namespace OMSv2.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        // GET: api/Item
        [HttpPost]
        public ApiResultWithData<List<Item>> Get(ItemFilterParameter parameter)
        {
            ApiResultWithData<List<Item>> result = new ApiResultWithData<List<Item>>();

            var apiKeyHelper = new ApiKeyHelper();
            var apiKey = Request.Headers["ApiKey"].ToString();

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {
                ItemData itemData = new ItemData();
                var items = itemData.GetAll(parameter);
                if (parameter.MinPrice != 0)
                    items = items.Where(x => x.Price >= parameter.MinPrice).ToList();
                if (parameter.MaxPrice != 0)
                    items = items.Where(x => x.Price <= parameter.MaxPrice).ToList();
                if (parameter.IsInStock)
                    items = items.Where(x => x.Stock > 0).ToList();
                if (!parameter.IsInStock)
                    items = items.Where(x => x.Stock <= 0).ToList();
                result.Data = items;
                result.Status = ErrorCode.Success;
            }
            else
                result.Status = ErrorCode.InvalidOrEmptyID;

            return result;
        }

        // POST: api/Item
        [HttpPost("Create")]
        public ApiResultWithData<RecordResponse> Post([FromBody]List<Item> items)
        {
            var result = new ApiResultWithData<RecordResponse>();
            var apiKeyHelper = new ApiKeyHelper();
            var apiKey = Request.Headers["ApiKey"].ToString(); //apiKeyHelper.GetApiKey(requestMessage);

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {

                ItemData itemData = new ItemData();
                var omsResult = new Result();
                foreach (var item in items)
                {
                    result = ValidateItem(item);
                    if (result.Status == ErrorCode.Success)
                    {
                        omsResult = itemData.Insert(item);
                    }
                }

                if (omsResult.IsValid)
                {
                    result.Status = ErrorCode.Success;
                    result.Data = new RecordResponse { Id = omsResult.ID };
                }
                else
                    result.Status = ErrorCode.SomethingWentWrong;
            }



            else
                result.Status = apiKeyHelper.ErrorCode;

            return result;
        }

        [HttpPost("Update")]
        public Models.ApiResult Update(Item item)
        {
            Models.ApiResult result = new Models.ApiResult();

            var apiKeyHelper = new ApiKeyHelper();
            var apiKey = Request.Headers["ApiKey"].ToString(); //apiKeyHelper.GetApiKey(requestMessage);

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {
                result = ValidateItem(item, true);
                if (result.Status == ErrorCode.Success)
                {
                    ItemData itemData = new ItemData();
                    var OMSResult = itemData.Update(item);
                    if (OMSResult.IsValid)
                        result.Status = ErrorCode.Success;
                    else
                        result.Status = ErrorCode.SomethingWentWrong;
                }

            }
            else
                result.Status = apiKeyHelper.ErrorCode;
            return result;
        }

        // DELETE: api/ApiWithActions/5
        [HttpPost("DeleteByID")]
        public Models.ApiResult Delete(int itemID)
        {
            Models.ApiResult result = new Models.ApiResult();
            var apiKeyHelper = new ApiKeyHelper();
            var apiKey = Request.Headers["ApiKey"].ToString(); //apiKeyHelper.GetApiKey(requestMessage);

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {
                ItemData itemData = new ItemData();
                var OMSResult = itemData.Delete(itemID, Guid.Empty);
                if (OMSResult.IsValid)
                    result.Status = ErrorCode.Success;
                else
                    result.Status = ErrorCode.SomethingWentWrong;
            }
            else
                result.Status = apiKeyHelper.ErrorCode;
            return result;
        }
        [HttpPost("GetByID")]
        public ApiResultWithData<Item> GetByID(int itemID)
        {
            var apiKeyHelper = new ApiKeyHelper();
            ApiResultWithData<Item> result = new ApiResultWithData<Item>();
            var apiKey = Request.Headers["ApiKey"].ToString(); //apiKeyHelper.GetApiKey(requestMessage);

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {

                ItemData itemData = new ItemData();
                var data = itemData.GetByID(itemID);
                result.Data = data;
                result.Status = ErrorCode.Success;

            }
            else
                result.Status = apiKeyHelper.ErrorCode;

            return result;
        }
        private ApiResultWithData<RecordResponse> ValidateItem(Item item, bool isUpdate = false)
        {
            // Validate basic required information is provided.
            if (string.IsNullOrEmpty(item.Name))
                return new ApiResultWithData<RecordResponse> { Status = ErrorCode.MandatoryFieldMissing };
            if (Utility.IsInvalidGuid(item.ClientID) && !isUpdate)
                return new ApiResultWithData<RecordResponse> { Status = ErrorCode.MandatoryFieldMissing };
            if (isUpdate && item.ItemID == 0)
                return new ApiResultWithData<RecordResponse> { Status = ErrorCode.MandatoryFieldMissing };
            return new ApiResultWithData<RecordResponse> { Status = ErrorCode.Success };
        }
    }
}
