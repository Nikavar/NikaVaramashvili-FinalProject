using Mapster;
using Microsoft.Extensions.DependencyInjection;
using MoviesManagement.Service.Models;
using MovieWebApi.Domain.POCO;
using MovieWebApi.Service.Models;
using MovieWebApi.Service.Models.JWT;
using MovieWebApi.Web.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebApi.Web.Mappings
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection service)
        {
            TypeAdapterConfig<Users, UserRegisterServiceModel>
                .NewConfig()
                .TwoWays();

            // Service <==> POCO
            TypeAdapterConfig<MovieServiceModel, Movies>
            .NewConfig()
            .TwoWays();

            // Service <==> DTO
            TypeAdapterConfig<MovieServiceModel, MovieDTO>
            .NewConfig()
            .TwoWays();
        }
    }
}
