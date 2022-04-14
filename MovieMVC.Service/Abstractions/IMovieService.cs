using MovieMVC.Service.Models;
using MovieWebApi.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieMVC.Service.Abstractions
{
    public interface IMovieService
    {        
        List<MovieServiceModel> GetAllAsync();    
        Task<Movies> GetByIdAsync(int id); 
        List<MovieServiceModel> GetAllNonActiveAsync(); 
        List<MovieServiceModel> GetAllArchiveAsync();    
        Task CreateAsync(MovieServiceModel movie);
        Task UpdateAsync(MovieServiceModel movie);
        Task DeleteAsync(int id);
        Task PublishMovieAsync(int id);
        Task DeactivateMovieAsync(int id);
    }
}
