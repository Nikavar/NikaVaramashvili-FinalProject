using Microsoft.Extensions.DependencyInjection;
using MovieWebApi.Data;
using MovieWebApi.Data.EF;


namespace TBC_Movies.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMovieRepository, MovieRepository>();            
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}
