using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersistenceDb.MVC.Context;
using System.Threading.Tasks;

namespace MovieMVC.Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly MVCDbContext _context;
        public OrderController(MVCDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {            
            var orders = await _context.Orders.Include(n => n.OrderMovies).ThenInclude(n => n.Movie).ToListAsync();
            return View(orders);
        }
    }
}
