using MovieWebApi.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebApi.Data
{
    public interface IMovieRepository
    {
        Task<List<Movies>> GetAllAsync();
        Task<Movies> GetByIdAsync(int id);
        Task<int> CreateAsync(Movies movie);        
        Task UpdateAsync(Movies movie);        
        Task DeleteAsync(int id);
        Task<bool> Exists(int id);
        Task PublishAsync(int id);
        Task RejectAsync(int id);
    }
}
