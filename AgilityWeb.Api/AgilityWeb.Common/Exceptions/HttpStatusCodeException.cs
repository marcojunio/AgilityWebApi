using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace AgilityWeb.Common.Exceptions
{
    public class HttpStatusCodeException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; }

        public HttpStatusCodeException(HttpStatusCode httpStatusCode, HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, Exception inner)
            : this(statusCode, inner.ToString())
        {
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, JObject errorObject)
            : this(statusCode, errorObject.ToString())
        {
            ContentType = @"application/json";
        }
    }
}