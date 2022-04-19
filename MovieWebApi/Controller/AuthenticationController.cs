using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieWebApi.Service.Abstractions;
using MovieWebApi.Service.Models;
using MovieWebApi.Web.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebApi.Controller
{      
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticateService _service;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticateService service, ILogger<AuthenticationController> logger)
        {
            _service = service;
            _logger = logger;
        }

        //User Authentification, returns User Data
        [MapToApiVersion("1")]
        [HttpPost]
        public IActionResult Post(string userName, string Password)
        {
            _logger.LogInformation("Post user From AuthentificationController/Service");

            var user = _service.Login(userName, Password);
            if (user == null) return BadRequest(new { message = "Username or Password is NOT Correct!!!" });

            return Ok(user);
        }

        //Users Registration, returns User data
        [HttpPost("Registration"), ActionName("Registration")]
        public IActionResult Register(UserRegisterServiceModel model)
        {
            var result = _service.Registration(model);
            if (result == null) return BadRequest(new { message = "Username u entered, Already Exists" });

            return Ok(result);
        }
    }
}
