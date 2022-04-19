using Microsoft.AspNetCore.Builder;
using MovieWebApi.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebApi.Infrastructure.Extensions
{
    public static class ErrorHandlerMiddlewareExtension
    {
        public static IApplicationBuilder AddErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
