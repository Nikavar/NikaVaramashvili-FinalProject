using Microsoft.AspNetCore.Builder;
using MovieWebApi.Web.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebApi.Web.Infrastructure.Extensions
{
    public static class RespondeRequestExtensions
    {
        public static IApplicationBuilder UseRequestResponse(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseMiddleware>();
        }
    }
}
