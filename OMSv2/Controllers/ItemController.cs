using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OMSv2.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        // GET: api/Item
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //// GET: api/Item/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Item
       // [HttpPost]
        //public void Post(HttpRequestMessage requestMessage, Item item)
        //{
        //    var result = new ApiResultWithData<RecordResponse>();
        //    var apiKeyHelper = new ApiKeyHelper();
        //    var apiKey = requestMessage.Headers.GetValues("apiKey")?.FirstOrDefault();

        //    if (apiKeyHelper.IsValidAPIKey(apiKey, ApiModule.Customer, ApiAccess.Create))
        //    {
        //        result = ValidateRenter(renter);
        //        if (result.Status == ErrorCode.Success)
        //        {
        //            if (renter.CreditCardDetail != null)
        //            {
        //                foreach (var item in renter.CreditCardDetail)
        //                {
        //                    item.TemporaryCRUD.IsNew = true;
        //                }
        //            }
        //            var itemData = new ItemData();
        //            var fleetletResult = itemData.Insert(item);
        //            if (fleetletResult.IsValid)
        //            {
        //                result.Status = ErrorCode.Success;
        //                result.Data = new RecordResponse { Id = fleetletResult.ID };
        //            }
        //            else
        //                result.Status = ErrorCode.SomethingWentWrong;
        //        }

        //        logger.Log($"Completed Post Renter {DateTimeHelper.CurrentDateTime(context.TimeZoneId)}");
        //    }
        //    else
        //        result.Status = apiKeyHelper.ErrorCode;

        //    return result;
        //}

        // PUT: api/Item/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
