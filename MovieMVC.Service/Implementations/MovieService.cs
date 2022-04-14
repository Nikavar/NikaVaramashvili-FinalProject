using MovieMVC.Service.Abstractions;
using MovieMVC.Service.Models;
using MovieWebApi.Data;
using MovieWebApi.Domain.POCO;
using PersistenceDb.MVC.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using MovieWebApi.PersistenceDB.Context;

namespace MovieMVC.Service.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repo;
        private readonly MVCDbContext _context;
        public MovieService(IMovieRepository repo, MVCDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task CreateAsync(MovieServiceModel movie)
        {
            var movieToAdd = new Movies()
            {
                Title = movie.Title,
                ProducedYear = movie.ProducedYear,
                Subject = movie.Subject,
                Description = movie.Description,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                PosterUrl = movie.PosterUrl,
                Price = movie.MoviePrice,
                IsActive = false
            };
            await _context.Movies.AddAsync(movieToAdd);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task DeactivateMovieAsync(int id)
        {
            await _repo.RejectAsync(id);
        }

        public List<MovieServiceModel> GetAllArchiveAsync()
        {
            var movies = _context.Movies.Where(x => x.EndDate < DateTime.Now);
            return movies.Adapt<List<MovieServiceModel>>();
        }

        public List<MovieServiceModel> GetAllAsync()
        {
            var movies = _context.Movies.Where(x => x.IsActive == true && x.EndDate > DateTime.Now);
            return movies.Adapt<List<MovieServiceModel>>();
        }

        public async Task<Movies> GetByIdAsync(int id)
        {
            return await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task PublishMovieAsync(int id)
        {
            await _repo.PublishAsync(id);
        }

        public async Task UpdateAsync(MovieServiceModel model)
        {
            var result = await _context.Movies.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (result != null)
            {
                result.Title = model.Title;
                result.ProducedYear = model.ProducedYear;
                result.Subject = model.Subject;
                result.Description = model.Description;
                result.StartDate = model.StartDate;
                result.EndDate = model.EndDate;
                result.PosterUrl = model.PosterUrl;
                result.Price = model.MoviePrice;
                result.IsActive = model.IsActive;

                await _context.SaveChangesAsync();
            }
        }

        public List<MovieServiceModel> GetAllNonActiveAsync()
        {
            var result = _context.Movies.Where(x => x.IsActive == false);
            return result.Adapt<List<MovieServiceModel>>();
        }
    }
}
