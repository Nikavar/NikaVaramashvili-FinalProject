using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebApi.Web.Middleware
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseMiddleware> _logger;

        public RequestResponseMiddleware(RequestDelegate next, ILogger<RequestResponseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request.Path;
            _logger.LogInformation($"Request Details: {request}");

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

            var response = context.Response.StatusCode;
            _logger.LogInformation($"Response Details: {response}");
        }
    }
}
