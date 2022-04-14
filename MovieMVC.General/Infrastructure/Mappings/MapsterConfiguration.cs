using Mapster;
using Microsoft.Extensions.DependencyInjection;
using MovieMVC.Service.Models;
using MovieWebApi.Domain.POCO;
using MovieMVC.General.Models.Request.Movie;
using MovieMVC.General.Models.Response;

namespace MovieMVC.General.Infrastructure.Mappings
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection services)
        {
            TypeAdapterConfig<MovieWebApi.Domain.POCO.Movies, MovieServiceModel>
                .NewConfig()
                .TwoWays();

            TypeAdapterConfig<MovieDTO, MovieServiceModel>
                .NewConfig()
                .TwoWays();

            TypeAdapterConfig<MoviePostRequest, MovieServiceModel>
                .NewConfig()
                .TwoWays();

            TypeAdapterConfig<MoviePutRequest, MovieServiceModel>
                .NewConfig()
                .TwoWays();
        }
    }

}
