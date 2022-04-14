using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieMVC.Service.Abstractions;
using MovieMVC.Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.General.Models.Response;
using MovieMVC.General.Models.Request.Movie;

namespace MovieMVC.General.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _service;    

        public MovieController(IMovieService service)
        {
            _service = service;         
        }
        public IActionResult Index()
        {
            var result = _service.GetAllAsync();
            var moviesDTO = result.Adapt<List<MovieDTO>>();
            
            return View(moviesDTO);
        }
        public async Task<IActionResult> Details(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return View("NotFound");
            }

            ViewBag.Suggestions = result;

            return View(result);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult NonActive()
        {
            var result = _service.GetAllNonActiveAsync();
            var moviesDTO = result.Adapt<List<MovieDTO>>();
            return View(moviesDTO);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Activate(int id)
        {
            await _service.PublishMovieAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deactivate(int id)
        {
            await _service.DeactivateMovieAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Archive()
        {
            var result = _service.GetAllArchiveAsync();
            var moviesDTO = result.Adapt<List<MovieDTO>>();
            return View(moviesDTO);
        }

        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create(MoviePostRequest movie)
        {
            var newMovie = movie.Adapt<MovieServiceModel>();
            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            await _service.CreateAsync(newMovie);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(MovieServiceModel movie)
        {
            await _service.UpdateAsync(movie);

            if (!ModelState.IsValid)
            {
                return View("NotFound");
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.GetByIdAsync(id);
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
