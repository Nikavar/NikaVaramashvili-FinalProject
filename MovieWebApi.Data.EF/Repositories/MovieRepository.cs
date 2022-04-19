using MovieWebApi.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebApi.Data.EF
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IBaseRepository<Movies> _baseRepository;

        public MovieRepository(IBaseRepository<Movies> repository)
        {
            _baseRepository = repository;
        }

        public async Task<List<Movies>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync();
        }

        public async Task<Movies> GetByIdAsync(int id)
        {
            return await _baseRepository.GetAsync(id);
        }

        public async Task<int> CreateAsync(Movies movie)
        {
            await _baseRepository.AddAsync(movie);
            return movie.Id;
        }

        public async Task UpdateAsync(Movies movie)
        {
            await _baseRepository.UpdateAsync(movie);
        }

        public async Task DeleteAsync(int id)
        {
            await _baseRepository.RemoveAsync(id);
        }

        public async Task PublishAsync(int id)
        {
            var publishedMovie = await _baseRepository.GetAsync(id);
            publishedMovie.IsActive = true;
            await _baseRepository.UpdateAsync(publishedMovie);
        }

        public async Task RejectAsync(int id)
        {
            var rejectedMovie = await _baseRepository.GetAsync(id);
            rejectedMovie.IsActive = false;
            await _baseRepository.UpdateAsync(rejectedMovie);
        }

        public async Task<bool> Exists(int id)
        {
            return await _baseRepository.AnyAsync(x => x.Id == id);
        }
    }
}
