using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace AgilityWeb.Common.Exceptions
{
    public class HttpStatusCodeException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; }
        public string TitleError { get; set; }

        public HttpStatusCodeException(string titleError, HttpStatusCode httpStatusCode, HttpStatusCode statusCode)
        {
            TitleError = titleError;
            StatusCode = statusCode;
        }

        public HttpStatusCodeException(string titleError, HttpStatusCode statusCode, string message) : base(message)
        {
            ContentType = @"application/json";
            TitleError = titleError;
            StatusCode = statusCode;
        }

        public HttpStatusCodeException(string titleError, HttpStatusCode statusCode, Exception inner)
            : this(titleError, statusCode, inner.ToString())
        {
        }
    }
}