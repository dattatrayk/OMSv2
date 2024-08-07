﻿using OMSv2.Service.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OMSv2.Service.Models
{
    /// <summary>
    /// API result 
    /// </summary>
    public class ApiResult
    {
        private string _message;

        /// <summary>
        /// ctor
        /// </summary>
        public ApiResult()
        {
            Status = ErrorCode.SomethingWentWrong;
        }

        /// <summary>
        /// Api call status
        /// </summary>
        public ErrorCode Status { get; set; }

        /// <summary>
        /// Message response
        /// </summary>
        public string Message
        {
            get
            {
                if (string.IsNullOrEmpty(_message))
                {
                    Message = Status.GetType()
                            .GetMember(Status.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
                }
                return _message;
            }
            set
            {
                _message = value;
            }
        }

    }

    /// <summary>
    /// API result along with data. 
    /// </summary>
    /// <typeparam name="T">Type of data</typeparam>
    public class ApiResultWithData<T> : ApiResult
    {
        /// <summary>
        /// Data when Get API call is retrived
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    /// Used when sending only ID as an response
    /// </summary>
    public class RecordResponse
    {
        /// <summary>
        /// Record ID
        /// </summary>
        public int Id { get; set; }
        public string ApiKey { get; set; }
    }
}
