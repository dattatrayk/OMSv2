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
    public class BrandController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public ApiResultWithData<List<Brand>> Get(Guid clientID)
        {
            ApiResultWithData<List<Brand>> result = new ApiResultWithData<List<Brand>>();

            BrandData brandData = new BrandData();
            result.Data = brandData.GetAll(clientID);
            result.Status = ErrorCode.Success;
            return result;
        }

        [Authorize]
        [HttpPost("Create")]
        public ApiResultWithData<RecordResponse> Post(Brand brand)
        {
            var result = new ApiResultWithData<RecordResponse>();

            result = ValidateBrand(brand);
            if (result.Status == ErrorCode.Success)
            {
                BrandData brandData = new BrandData();
                var OMSResult = brandData.Insert(brand);
                if (OMSResult.IsValid)
                {
                    result.Status = ErrorCode.Success;
                    result.Data = new RecordResponse { Id = OMSResult.ID };
                }
                else
                    result.Status = ErrorCode.SomethingWentWrong;
            }

            return result;
        }

        [Authorize]
        [HttpPost("Update")]
        public ApiResult Update(Brand brand)
        {
            ApiResult result = new ApiResult();

            result = ValidateBrand(brand, true);
            if (result.Status == ErrorCode.Success)
            {
                BrandData brandData = new BrandData();
                var OMSResult = brandData.Update(brand);
                if (OMSResult.IsValid)
                    result.Status = ErrorCode.Success;
                else
                    result.Status = ErrorCode.SomethingWentWrong;
            }

            return result;
        }

        [Authorize]
        [HttpPost("DeleteByID")]
        public ApiResult Delete(int brandID)
        {
            ApiResult result = new ApiResult();

            BrandData brandData = new BrandData();
            var OMSResult = brandData.Delete(brandID, Guid.Empty);
            if (OMSResult.IsValid)
                result.Status = ErrorCode.Success;
            else
                result.Status = ErrorCode.SomethingWentWrong;

            return result;
        }

        [Authorize]
        [HttpPost("GetByID")]
        public ApiResultWithData<Brand> GetByID(int brandID)
        {
            ApiResultWithData<Brand> result = new ApiResultWithData<Brand>();

            BrandData brandData = new BrandData();
            var data = brandData.GetByID(brandID);
            result.Data = data;
            result.Status = ErrorCode.Success;

            return result;
        }

        private ApiResultWithData<RecordResponse> ValidateBrand(Brand brand, bool isUpdate = false)
        {
            // Validate basic required information is provided.
            if (string.IsNullOrEmpty(brand.BrandName))
                return new ApiResultWithData<RecordResponse> { Status = ErrorCode.MandatoryFieldMissing };
            if (Utility.IsInvalidGuid(brand.ClientID) && !isUpdate)
                return new ApiResultWithData<RecordResponse> { Status = ErrorCode.MandatoryFieldMissing };
            if (isUpdate && brand.BrandID == 0)
                return new ApiResultWithData<RecordResponse> { Status = ErrorCode.MandatoryFieldMissing };
            return new ApiResultWithData<RecordResponse> { Status = ErrorCode.Success };
        }
    }
}
