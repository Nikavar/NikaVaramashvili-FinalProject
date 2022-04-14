using MoviesManagement.Service.Models;
using MovieWebApi.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Service.Abstractions
{
    public interface IMovieService
    {
        Task<List<MovieServiceModel>> GetAllAsync();
        Task<(Status stat, MovieServiceModel)> GetAsyncById(int id);
    }
}
