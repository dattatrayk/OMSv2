using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OMSv2.Service.Entity;
using OMSv2.Service.Helpers;
using OMSv2.Service.Models;

namespace OMSv2.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        // GET: api/Sale
        [HttpGet]
        public ApiResultWithData<List<Sale>> Get(SaleFilterParameter parameter)
        {
            ApiResultWithData<List<Sale>> result = new ApiResultWithData<List<Sale>>();

            var apiKeyHelper = new ApiKeyHelper();
            var apiKey = Request.Headers["ApiKey"].ToString();

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {
                SaleData saleData = new SaleData();
                result.Data = saleData.GetAll(parameter);
                result.Status = ErrorCode.Success;
            }
            else
                result.Status = ErrorCode.InvalidOrEmptyID;

            return result;
        }

        // POST: api/Sale
        [HttpPost("Create")]
        public ApiResultWithData<RecordResponse> Post(Sale sale)
        {
            var result = new ApiResultWithData<RecordResponse>();
            var apiKeyHelper = new ApiKeyHelper();
            var apiKey = Request.Headers["ApiKey"].ToString(); //apiKeyHelper.GetApiKey(requestMessage);

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {
                SaleData saleData = new SaleData();
                var omsResult = new Result();

                sale.SaleID = Guid.NewGuid();
                omsResult = saleData.Insert(sale);

                if (omsResult.IsValid)
                {

                    if (sale.SaleDetail != null && sale.SaleDetail.Count > 0)
                    {
                        SaleDetailsData saleDetailsData = new SaleDetailsData();

                        foreach (var item in sale.SaleDetail)
                        {
                            item.SaleID = sale.SaleID;
                            omsResult = saleDetailsData.Insert(item);
                        }
                    }
                }
                if (omsResult.IsValid)
                {
                    result.Status = ErrorCode.Success;
                    result.Data = new RecordResponse { Id = sale.SaleID };
                }
                else
                    result.Status = ErrorCode.SomethingWentWrong;
            }
            else
                result.Status = apiKeyHelper.ErrorCode;

            return result;
        }

        [HttpPost("Update")]
        public Models.ApiResult Update(Sale sale)
        {
            Models.ApiResult result = new Models.ApiResult();

            var apiKeyHelper = new ApiKeyHelper();
            var apiKey = Request.Headers["ApiKey"].ToString(); //apiKeyHelper.GetApiKey(requestMessage);

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {
                SaleData saleData = new SaleData();
                var OMSResult = saleData.Update(sale);
                if (OMSResult.IsValid)
                {
                    if (sale.SaleDetail != null && sale.SaleDetail.Count > 0)
                    {
                        SaleDetailsData saleDetailsData = new SaleDetailsData();
                        OMSResult = saleDetailsData.Delete(sale.SaleID);
                        foreach (var item in sale.SaleDetail)
                        {
                            item.SaleID = sale.SaleID;
                            OMSResult = saleDetailsData.Insert(item);
                        }
                    }
                }
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
        public Models.ApiResult Delete(Guid saleID)
        {
            Models.ApiResult result = new Models.ApiResult();
            var apiKeyHelper = new ApiKeyHelper();
            var apiKey = Request.Headers["ApiKey"].ToString(); //apiKeyHelper.GetApiKey(requestMessage);

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {
                SaleData saleData = new SaleData();
                var OMSResult = saleData.Delete(saleID, Guid.Empty);
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
        public ApiResultWithData<Sale> GetByID(Guid saleID)
        {
            var apiKeyHelper = new ApiKeyHelper();
            ApiResultWithData<Sale> result = new ApiResultWithData<Sale>();
            var apiKey = Request.Headers["ApiKey"].ToString(); //apiKeyHelper.GetApiKey(requestMessage);

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {

                SaleData saleData = new SaleData();
                var data = saleData.GetByID(saleID);
                result.Data = data;
                result.Status = ErrorCode.Success;

            }
            else
                result.Status = apiKeyHelper.ErrorCode;

            return result;
        }
    }
}
