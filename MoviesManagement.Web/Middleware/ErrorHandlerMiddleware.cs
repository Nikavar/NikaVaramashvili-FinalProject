using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MovieWebApi.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebApi.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {

            var exception = new ApiError(context, ex);
            _logger.LogError(exception.TraceId, exception.LogLevel, ex, exception.Type);

            var result = JsonConvert.SerializeObject(exception);
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.Status.Value;

            await context.Response.WriteAsync(result);
        }
    }
}
