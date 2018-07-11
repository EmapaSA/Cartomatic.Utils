﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

#if NETFULL
using System.Web.Http;
using System.Web.Http.Results;
#endif

#if NETSTANDARD2_0
using Microsoft.AspNetCore.Mvc;
#endif


namespace Cartomatic.Utils.ApiClient
{
    public class ApiCallException : Exception
    {
        public ApiCallException(string msg)
            : base(msg)
        {
        }

        public ApiCallException(HttpStatusCode responseStatus, string msg)
            : base(ImproveMsg(responseStatus, msg))
        {
            ResponseStatus = responseStatus;
        }

        public ApiCallException(HttpStatusCode responseStatus, string msg, Exception ex)
            : base (ImproveMsg(responseStatus, msg), ex)
        {
            ResponseStatus = responseStatus;
        }

        protected static string ImproveMsg(HttpStatusCode responseStatus, string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                msg = $"Backend API replied with status code {(int)responseStatus} ({responseStatus})";
            }
            return msg;
        }

        public HttpStatusCode? ResponseStatus { get; set; }


#if NETFULL
        /// <summary>
        /// Extracts a response status if an exception is an api call exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ctrlr"></param>
        /// <returns></returns>
        public static IHttpActionResult HandleApiCallException(Exception ex, ApiController ctrlr)
        {
            if (ex is ApiCallException apiCallEx)
            {
                return new NegotiatedContentResult<object>(
                    apiCallEx.ResponseStatus ?? HttpStatusCode.BadRequest,
                    new { ErrorMessage = ex.Message },
                    ctrlr
                );
            }

            return null;
        }
#endif

#if NETSTANDARD2_0
        /// <summary>
        /// Extracts a response status if an exception is an api call exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static IActionResult HandleApiCallException(Exception ex)
        {
            if (ex is ApiCallException apiCallEx)
            {

                //Microsoft.AspNetCore.Mvc.WebApiCompatShim
                return new ObjectResult(new { ErrorMessage = ex.Message })
                {
                    StatusCode = (int?)(apiCallEx.ResponseStatus ?? HttpStatusCode.BadRequest)
                };

            }

            return null;
        }
#endif


    }
}