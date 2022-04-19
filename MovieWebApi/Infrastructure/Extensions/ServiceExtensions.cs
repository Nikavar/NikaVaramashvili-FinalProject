using Microsoft.Extensions.DependencyInjection;
using MoviesManagement.Service.Abstractions;
using MoviesManagement.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebApi.Web.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            //services.AddScoped<IMovieService, MovieService>(); 
            //services.AddRepositories();
            //services.AddScoped<IJWTService, JWTService>();         
        }
    }
}
