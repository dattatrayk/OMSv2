using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OMSv2.Service.DataAccess;
using OMSv2.Service.Entity;
using OMSv2.Service.Helpers;
using OMSv2.Service.Models;

namespace OMSv2.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpPost("Create")]
        public ApiResultWithData<RecordResponse> Post(Client client)
        {
            var apiKeyHelper = new ApiKeyHelper();
            var result = new ApiResultWithData<RecordResponse>();
            var apiKey = apiKeyHelper.GetApiKey(Request);

            if (apiKeyHelper.IsValidMasterAPIKey(apiKey))
            {
                result = ValidateClient(client);
                if (result.Status == ErrorCode.Success)
                {
                    ClientData clientData = new ClientData();
                    client.ClientID = Guid.NewGuid();
                    client.ApiKey = ApiKeyGenerator.GetKey(client.ClientID, client.Name);
                    var omsResult = clientData.Insert(client);
                    if (omsResult.IsValid)
                    {
                        result.Status = ErrorCode.Success;
                        result.Data = new RecordResponse { Id = omsResult.ID, ApiKey = client.ApiKey };
                    }
                    else
                    {
                        result.Status = ErrorCode.SomethingWentWrong;
                    }
                }
            }
            else
            {
                result.Status = apiKeyHelper.ErrorCode;
            }

            return result;
        }

        private ApiResultWithData<RecordResponse> ValidateClient(Client client, bool isUpdate = false)
        {
            // Validate basic required information is provided.
            if (string.IsNullOrEmpty(client.Name))
            {
                return new ApiResultWithData<RecordResponse> { Status = ErrorCode.MandatoryFieldMissing };
            }
            if (string.IsNullOrEmpty(client.ContactNo))
            {
                return new ApiResultWithData<RecordResponse> { Status = ErrorCode.MandatoryFieldMissing };
            }
            if (string.IsNullOrEmpty(client.Email))
            {
                return new ApiResultWithData<RecordResponse> { Status = ErrorCode.MandatoryFieldMissing };
            }
            if (isUpdate && Utility.IsInvalidGuid(client.ClientID))
            {
                return new ApiResultWithData<RecordResponse> { Status = ErrorCode.MandatoryFieldMissing };
            }
            return new ApiResultWithData<RecordResponse> { Status = ErrorCode.Success };
        }
    }
}

