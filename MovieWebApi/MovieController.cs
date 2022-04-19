using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesManagement.Service.Abstractions;
using MoviesManagement.Service.Models;
using MovieWebApi.Service;
using MovieWebApi.Service.Exceptions;
using MovieWebApi.Web.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebApi.Web.Controller
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        #region Fields

        private readonly IMovieService _service;
        private readonly ILogger<MovieController> _logger;
        #endregion

        #region Ctor
        public MovieController(IMovieService service, ILogger<MovieController> logger)
        {
            _service = service;
            _logger = logger;
        }
        #endregion

        #region GETs ==> Note: only User Mode
        [MapToApiVersion("1")]
        [HttpGet]
        public async Task<List<MovieDTO>> GetAll()
        {
            _logger.LogInformation("Get Movies From MovieController/Service");

            var result = await _service.GetAllAsync();
            if (result == null)
                throw new ObjectNotFoundException("data not founded");

            return result.Adapt<List<MovieDTO>>();
        }

        [MapToApiVersion("1")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetAsyncById(id);
            if (result.stat == Status.Success)
                return Ok(result.Item2.Adapt<MovieDTO>());

            return StatusCode((int)result.stat);
        }
        #endregion       
    }
}

