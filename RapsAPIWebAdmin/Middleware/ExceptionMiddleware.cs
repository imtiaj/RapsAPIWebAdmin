using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Utility.Exceptions;
using Utility.Helpers;

namespace RapsAPIWebAdmin.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception e)
            {
                await HandleCustomException(e,context);
            }            
        }

        private async Task HandleCustomException(Exception exception, HttpContext context)
        {
            var code = HttpStatusCode.InternalServerError;
            var error = new ErrorResponse();

            error.StatusCode = (int)code;
            error.Message = exception.Message;

            if (_env.IsDevelopment())
            {
                error.DevloperMessage = exception.StackTrace;
            }            

            switch (exception)
            {
                case RapsAppException rapsAppException:
                    error.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    break;
            }

            var errResultJson = JsonConvert.SerializeObject(error);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.StatusCode;

            await context.Response.WriteAsync(errResultJson);
        }
    }
}
