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
    public class CustomerController : ControllerBase
    {
        // GET: api/Customer
        [HttpGet]
        public ApiResultWithData<List<Customer>> Get(CustomerFilterParameter parameter)
        {
            ApiResultWithData<List<Customer>> result = new ApiResultWithData<List<Customer>>();

            var apiKeyHelper = new ApiKeyHelper();
            var apiKey = Request.Headers["ApiKey"].ToString();

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {
                CustomerData customerData = new CustomerData();
                result.Data = customerData.GetAll(parameter);
                result.Status = ErrorCode.Success;
            }
            else
                result.Status = ErrorCode.InvalidOrEmptyID;

            return result;
        }

        // POST: api/Customer
        [HttpPost("Create")]
        public ApiResultWithData<RecordResponse> Post(Customer customer)
        {
            var result = new ApiResultWithData<RecordResponse>();
            var apiKeyHelper = new ApiKeyHelper();
            var apiKey = Request.Headers["ApiKey"].ToString(); //apiKeyHelper.GetApiKey(requestMessage);

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {
                result = ValidateCustomer(customer);
                if (result.Status == ErrorCode.Success)
                {
                    CustomerData customerData = new CustomerData();
                    var omsResult = new Result();

                    omsResult = customerData.Insert(customer);

                    if (omsResult.IsValid)
                    {
                        result.Status = ErrorCode.Success;
                        result.Data = new RecordResponse { Id = customer.CustomerID };
                    }
                    else
                        result.Status = ErrorCode.SomethingWentWrong;
                }
            }



            else
                result.Status = apiKeyHelper.ErrorCode;

            return result;
        }

        [HttpPost("Update")]
        public Models.ApiResult Update(Customer customer)
        {
            Models.ApiResult result = new Models.ApiResult();

            var apiKeyHelper = new ApiKeyHelper();
            var apiKey = Request.Headers["ApiKey"].ToString(); //apiKeyHelper.GetApiKey(requestMessage);

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {
                result = ValidateCustomer(customer, true);
                if (result.Status == ErrorCode.Success)
                {
                    CustomerData customerData = new CustomerData();
                    var OMSResult = customerData.Update(customer);
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
        public Models.ApiResult Delete(int customerID)
        {
            Models.ApiResult result = new Models.ApiResult();
            var apiKeyHelper = new ApiKeyHelper();
            var apiKey = Request.Headers["ApiKey"].ToString(); //apiKeyHelper.GetApiKey(requestMessage);

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {
                CustomerData customerData = new CustomerData();
                var OMSResult = customerData.Delete(customerID, Guid.Empty);
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
        public ApiResultWithData<Customer> GetByID(int customerID)
        {
            var apiKeyHelper = new ApiKeyHelper();
            ApiResultWithData<Customer> result = new ApiResultWithData<Customer>();
            var apiKey = Request.Headers["ApiKey"].ToString(); //apiKeyHelper.GetApiKey(requestMessage);

            if (apiKeyHelper.IsValidAPIKey(apiKey))
            {

                CustomerData customerData = new CustomerData();
                var data = customerData.GetByID(customerID);
                result.Data = data;
                result.Status = ErrorCode.Success;

            }
            else
                result.Status = apiKeyHelper.ErrorCode;

            return result;
        }
        private ApiResultWithData<RecordResponse> ValidateCustomer(Customer customer, bool isUpdate = false)
        {
            // Validate basic required information is provided.
            if (string.IsNullOrEmpty(customer.Name))
                return new ApiResultWithData<RecordResponse> { Status = ErrorCode.MandatoryFieldMissing };
            if (string.IsNullOrEmpty(customer.ContactNo))
                return new ApiResultWithData<RecordResponse> { Status = ErrorCode.MandatoryFieldMissing };
            if (Utility.IsInvalidGuid(customer.ClientID) && !isUpdate)
                return new ApiResultWithData<RecordResponse> { Status = ErrorCode.MandatoryFieldMissing };
            if (isUpdate && customer.CustomerID == 0)
                return new ApiResultWithData<RecordResponse> { Status = ErrorCode.MandatoryFieldMissing };
            return new ApiResultWithData<RecordResponse> { Status = ErrorCode.Success };
        }
    }
}
