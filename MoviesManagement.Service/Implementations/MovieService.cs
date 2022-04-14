using Mapster;
using Microsoft.EntityFrameworkCore;
using MoviesManagement.Service.Abstractions;
using MoviesManagement.Service.Models;
using MovieWebApi.Domain.POCO;
using MovieWebApi.PersistenceDB.Context;
using MovieWebApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Service.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _context;
        public MovieService(MovieDbContext context)
        {
            _context = context;
        }
        public async Task<List<MovieServiceModel>> GetAllAsync()
        {
            var movies = await _context.Movies.Where(x => x.IsActive == true).ToListAsync();
            return movies.Adapt<List<MovieServiceModel>>();
        }

        public async Task<(Status stat, MovieServiceModel)> GetAsyncById(int id)
        {
            var result = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            
            if (result == null) return (Status.NotFound, null);

            return (Status.Success, result.Adapt<MovieServiceModel>());
        }
    }
}
