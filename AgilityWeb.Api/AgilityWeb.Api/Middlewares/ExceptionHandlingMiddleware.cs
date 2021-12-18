using System;
using System.Net;
using System.Threading.Tasks;
using AgilityWeb.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace AgilityWeb.Api.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (HttpStatusCodeException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception exceptionObj)
            {
                await HandleExceptionAsync(context, exceptionObj);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, HttpStatusCodeException exception)
        {
            string result;
            context.Response.ContentType = "application/json";
            if (exception != null)
            {
                result = new ErrorDetails()
                {
                    Message = exception.Message,
                    StatusCode = (int) exception.StatusCode
                }.ToString();
                context.Response.StatusCode = (int) exception.StatusCode;
            }
            else
            {
                result = new ErrorDetails()
                {
                    Message = "Runtime Error",
                    StatusCode = (int) HttpStatusCode.BadRequest
                }.ToString();
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            }

            return context.Response.WriteAsync(result);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string result = new ErrorDetails()
            {
                Message = exception.Message,
                StatusCode = (int) HttpStatusCode.InternalServerError
            }.ToString();
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(result);
        }
    }
}