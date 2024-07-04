using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpPost]
        public ApiResultWithData<List<Item>> Get(ItemFilterParameter parameter)
        {
            ApiResultWithData<List<Item>> result = new ApiResultWithData<List<Item>>();

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
            return result;
        }

        [HttpPost("Insert")]
        public ApiResultWithData<RecordResponse> Insert([FromBody]List<Item> items)
        {
            var result = new ApiResultWithData<RecordResponse>();
            var apiKeyHelper = new ApiKeyHelper();
            var apiKey = apiKeyHelper.GetApiKey(Request);

            if (apiKeyHelper.IsValidAPI(apiKey))
            {
                ItemData itemData = new ItemData();
                var omsResult = new Result();
                foreach (var item in items)
                {
                    result = ValidateItem(item);
                    if (result.Status == ErrorCode.Success)
                    {
                        bool isValid = itemData.CheckItemCodeExists(item.ClientID, item.Code);
                        if (isValid)
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

        [Authorize]
        [HttpPost("Create")]
        public ApiResultWithData<RecordResponse> Post([FromBody]List<Item> items)
        {
            var result = new ApiResultWithData<RecordResponse>();

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

            return result;
        }

        [Authorize]
        [HttpPost("Update")]
        public ApiResult Update(Item item)
        {
            ApiResult result = new ApiResult();

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
            return result;
        }

        [Authorize]
        [HttpPost("DeleteByID")]
        public ApiResult Delete(int itemID)
        {
            ApiResult result = new ApiResult();

            ItemData itemData = new ItemData();
            var OMSResult = itemData.Delete(itemID, Guid.Empty);
            if (OMSResult.IsValid)
                result.Status = ErrorCode.Success;
            else
                result.Status = ErrorCode.SomethingWentWrong;
            return result;
        }

        [Authorize]
        [HttpPost("GetByID")]
        public ApiResultWithData<Item> GetByID(int itemID)
        {
            ApiResultWithData<Item> result = new ApiResultWithData<Item>();

            ItemData itemData = new ItemData();
            var data = itemData.GetByID(itemID);
            result.Data = data;
            result.Status = ErrorCode.Success;

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
