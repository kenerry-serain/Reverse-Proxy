using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Patterns.WebAPI.Middleware
{
    public class ErrorHandlerMiddleware: IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var exceptionObject = new ExceptionResponse(exception);
            var exceptionSerialized = JsonConvert.SerializeObject(exceptionObject);

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(exceptionSerialized);
            }
        }
    }
}