using Microsoft.EntityFrameworkCore;
using MovieMVC.Service.Abstractions;
using MovieMVC.Service.Models;
using MovieWebApi.Domain.POCO;
using PersistenceDb.MVC.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMVC.Service.Implementations
{
    public class OrdersService : IOrdersService
    {
        private readonly MVCDbContext _context;
        public OrdersService(MVCDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserAsync(string userId)
        {
            var orders = await _context.Orders.Include(n => n.OrderMovies).ThenInclude(n => n.Movie).Where(n=>n.UserId==userId).ToListAsync();
            return orders;
        }

        public async Task StoreOrderAsync(Order order, List<OrderMovie> orderMovie)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            foreach(var item in orderMovie)
            {
                item.OrderId = order.Id;
                await _context.OrderMovies.AddAsync(item);
            }
            await _context.SaveChangesAsync();
        }
    }
}
