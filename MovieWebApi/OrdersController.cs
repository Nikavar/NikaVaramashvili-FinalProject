using MovieWebApi.Service.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieWebApi.Domain.POCO;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MoviesManagement.Service.Abstractions;

namespace MovieWebApi.Web.Controller
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersServiceAPI _service;
        public OrdersController(IOrdersServiceAPI service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] int movieId)
        {
            if (movieId == 0) return BadRequest("Please Enter Movie Id");
            var userId = User.FindFirstValue(ClaimTypes.Name);
           var result = await _service.StoreOrderAsync(movieId, userId);
            if (result == -1) return BadRequest("Movie Does not Exist");
            return Ok(result);
        }


        [HttpGet]
        public async Task<List<OrderAPI>> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.Name);
            var result = await _service.GetOrdersByUserAsync(userId);
            if (result == null) return null;
            return result;

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.Name);
            await _service.DeleteOrder(id, userId);

            return Ok();
        }
    }
}
