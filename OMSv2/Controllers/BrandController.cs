using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        // GET: api/Brand
        [HttpGet]
        public ApiResultWithData<List<Brand>> Get()
        {
            ApiResultWithData<List<Brand>> result = new ApiResultWithData<List<Brand>>();

            var apiKeyHelper = new ApiKeyHelper();
            var apiKey = Request.Headers["ApiKey"].ToString();

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {
                BrandData brandData = new BrandData();
                result.Data = brandData.GetAll();
                result.Status = ErrorCode.Success;
            }
            else
                result.Status =  ErrorCode.InvalidOrEmptyID;

            return result;
        }

        // POST: api/Brand
        [HttpPost("Create")]
        public ApiResultWithData<RecordResponse> Post(Brand brand)
        {
            var result = new ApiResultWithData<RecordResponse>();
            var apiKeyHelper = new ApiKeyHelper();
            var apiKey = Request.Headers["ApiKey"].ToString(); //apiKeyHelper.GetApiKey(requestMessage);

            if (apiKeyHelper.IsValidAPIKey(apiKey))
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

               
            
            else
                result.Status = apiKeyHelper.ErrorCode;

            return result;
        }

        [HttpPost("Update")]
        public Models.ApiResult Update(Brand brand)
        {
            Models.ApiResult result = new Models.ApiResult();

            var apiKeyHelper = new ApiKeyHelper();
            var apiKey = Request.Headers["ApiKey"].ToString(); //apiKeyHelper.GetApiKey(requestMessage);

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {
                BrandData brandData = new BrandData();
                var OMSResult = brandData.Update(brand);
                if (OMSResult.IsValid)
                        result.Status = ErrorCode.Success;
                    else
                        result.Status = ErrorCode.SomethingWentWrong;
                
            }
            else
                result.Status = apiKeyHelper.ErrorCode;
            return result;
        }

        // DELETE: api/ApiWithActions/5
        [HttpPost("DeleteByID")]
        public Models.ApiResult Delete( int brandID)
        {
            Models.ApiResult result = new Models.ApiResult();
            var apiKeyHelper = new ApiKeyHelper();
            var apiKey = Request.Headers["ApiKey"].ToString(); //apiKeyHelper.GetApiKey(requestMessage);

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {
                BrandData brandData = new BrandData();
                var OMSResult = brandData.Delete(brandID, Guid.Empty);
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
        public ApiResultWithData<Brand> GetByID(int brandID)
        {
            var apiKeyHelper = new ApiKeyHelper();
            ApiResultWithData<Brand> result = new ApiResultWithData<Brand>();
            var apiKey = Request.Headers["ApiKey"].ToString(); //apiKeyHelper.GetApiKey(requestMessage);

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {

                BrandData brandData = new BrandData();
                var data = brandData.GetByID(brandID);
                result.Data = data;
                result.Status = ErrorCode.Success;
                
            }
            else
                result.Status = apiKeyHelper.ErrorCode;

            return result;
        }
    }
}
