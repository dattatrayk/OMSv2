using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMSv2.Service.Entity
{
    public class Result
    {
        public Result()
        {
            Errors = new List<string>();
            ErrorCodes = new List<ErrorCode>();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ErrorCode> ErrorCodes { get; set; }
    }
    public enum ErrorCode
    {
        [Display(Name = "Success")]
        Success = 1,
        [Display(Name = "Invalid or empty id")]
        InvalidOrEmptyID = 2,
        [Display(Name = "Something Went Wrong")]
        SomethingWentWrong = 201,

        [Display(Name = "Mandatory field is missing.")]
        MandatoryFieldMissing
    }
}
