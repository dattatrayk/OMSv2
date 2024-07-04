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
    public class CustomerController : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public ApiResultWithData<List<Customer>> Get(CustomerFilterParameter parameter)
        {
            ApiResultWithData<List<Customer>> result = new ApiResultWithData<List<Customer>>();

            CustomerData customerData = new CustomerData();
            var customerDetails = customerData.GetAll(parameter);
            customerDetails = customerDetails.Where(x => !string.IsNullOrEmpty(x.Name)).ToList();
            if (customerDetails != null && !string.IsNullOrEmpty(parameter.SearchText))
                customerDetails = customerDetails.FindAll(s => s.Name.ToUpper().Contains(parameter.SearchText.ToUpper()));
            result.Data = customerDetails;
            result.Status = ErrorCode.Success;

            return result;
        }

        [Authorize]
        [HttpPost("Create")]
        public ApiResultWithData<RecordResponse> Post(Customer customer)
        {
            var result = new ApiResultWithData<RecordResponse>();
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

            return result;
        }

        [Authorize]
        [HttpPost("Update")]
        public ApiResult Update(Customer customer)
        {
            ApiResult result = new ApiResult();

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

            return result;
        }

        [Authorize]
        [HttpPost("DeleteByID")]
        public ApiResult Delete(int customerID)
        {
            ApiResult result = new ApiResult();
            CustomerData customerData = new CustomerData();
            var OMSResult = customerData.Delete(customerID, Guid.Empty);
            if (OMSResult.IsValid)
                result.Status = ErrorCode.Success;
            else
                result.Status = ErrorCode.SomethingWentWrong;
            return result;
        }

        [Authorize]
        [HttpGet("GetByID")]
        public ApiResultWithData<Customer> GetByID(int customerID)
        {
            ApiResultWithData<Customer> result = new ApiResultWithData<Customer>();

            CustomerData customerData = new CustomerData();
            var data = customerData.GetByID(customerID);
            result.Data = data;
            result.Status = ErrorCode.Success;

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
