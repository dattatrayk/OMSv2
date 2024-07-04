using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMSv2.Service.Entity;
using OMSv2.Service.Helpers;
using OMSv2.Service.Models;

namespace OMSv2.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public ApiResultWithData<List<Category>> Get(Guid clientID)
        {
            ApiResultWithData<List<Category>> result = new ApiResultWithData<List<Category>>();

            CategoryData categoryData = new CategoryData();
            result.Data = categoryData.GetAll(clientID);
            result.Status = ErrorCode.Success;

            return result;
        }

        [Authorize]
        [HttpPost("Create")]
        public ApiResultWithData<RecordResponse> Post(Category category)
        {
            var result = new ApiResultWithData<RecordResponse>();
            result = ValidateCategory(category);
            if (result.Status == ErrorCode.Success)
            {
                CategoryData categoryData = new CategoryData();
                var OMSResult = categoryData.Insert(category);
                if (OMSResult.IsValid)
                {
                    result.Status = ErrorCode.Success;
                    result.Data = new RecordResponse { Id = OMSResult.ID };
                }
                else
                {
                    result.Status = ErrorCode.SomethingWentWrong;
                }
            }


            return result;
        }

        [Authorize]
        [HttpPost("Update")]
        public ApiResult Update(Category category)
        {
            ApiResult result = new ApiResult();
            result = ValidateCategory(category, true);
            if (result.Status == ErrorCode.Success)
            {
                CategoryData categoryData = new CategoryData();
                var OMSResult = categoryData.Update(category);
                if (OMSResult.IsValid)
                {
                    result.Status = ErrorCode.Success;
                }
                else
                {
                    result.Status = ErrorCode.SomethingWentWrong;
                }
            }

            return result;
        }

        [Authorize]
        [HttpPost("DeleteByID")]
        public ApiResult Delete(int categoryID)
        {
            ApiResult result = new ApiResult();
            CategoryData categoryData = new CategoryData();
            var OMSResult = categoryData.Delete(categoryID, Guid.Empty);
            if (OMSResult.IsValid)
            {
                result.Status = ErrorCode.Success;
            }
            else
            {
                result.Status = ErrorCode.SomethingWentWrong;
            }

            return result;
        }

        [Authorize]
        [HttpPost("GetByID")]
        public ApiResultWithData<Category> GetByID(int categoryID)
        {
            ApiResultWithData<Category> result = new ApiResultWithData<Category>();

            CategoryData categoryData = new CategoryData();
            var data = categoryData.GetByID(categoryID);
            result.Data = data;
            result.Status = ErrorCode.Success;

            return result;
        }

        private ApiResultWithData<RecordResponse> ValidateCategory(Category category, bool isUpdate = false)
        {
            // Validate basic required information is provided.
            if (string.IsNullOrEmpty(category.CategoryName))
            {
                return new ApiResultWithData<RecordResponse> { Status = ErrorCode.MandatoryFieldMissing };
            }
            if (Utility.IsInvalidGuid(category.ClientID) && !isUpdate)
            {
                return new ApiResultWithData<RecordResponse> { Status = ErrorCode.MandatoryFieldMissing };
            }
            if (isUpdate && category.CategoryID == 0)
            {
                return new ApiResultWithData<RecordResponse> { Status = ErrorCode.MandatoryFieldMissing };
            }
            return new ApiResultWithData<RecordResponse> { Status = ErrorCode.Success };
        }
    }
}

