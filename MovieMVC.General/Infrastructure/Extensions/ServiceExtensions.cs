using Microsoft.Extensions.DependencyInjection;
using MovieMVC.Service.Abstractions;
using MovieMVC.Service.Implementations;

namespace TBC_Movies.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {          
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IOrdersService, OrdersService>();
        }
    }
}
