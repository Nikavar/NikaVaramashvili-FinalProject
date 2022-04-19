using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PersistenceDb.MVC.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService1
{
    public class ApiMovie
    {
        private readonly ILogger<ApiMovie> _logger;
        private readonly MVCDbContext _context;

        public ApiMovie(ILogger<ApiMovie> logger, MVCDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task Deactivate()
        {
            var oldMovies = await _context.Movies.Where(x => x.IsActive == true && x.EndDate < DateTime.Now).ToListAsync();
            foreach (var movies in oldMovies)
            {
                movies.IsActive = false;
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Deactivated Movie: {movies.Title}");
            }
        }
    }
}
