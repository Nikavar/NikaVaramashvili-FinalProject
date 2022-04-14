using MovieWebApi.Service.Abstractions;
using Microsoft.EntityFrameworkCore;
using MovieWebApi.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesManagement.Service.Abstractions;
using MovieWebApi.PersistenceDB.Context;

namespace MoviesManagement.Service.Implementations
{
    public class OrdersServiceAPI : IOrdersServiceAPI
    {
        private readonly MovieDbContext _context;
        public OrdersServiceAPI(MovieDbContext context)
        {
            _context = context;
        }

        public async Task DeleteOrder(int orderID, string userId)
        {
            var order = await _context.Orders.Where(o => o.Id== orderID&&o.UserId==userId).FirstOrDefaultAsync();
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }


        public async Task<List<OrderAPI>> GetOrdersByUserAsync(string userId)
        {
            var orders = await _context.Orders.Where(n=>n.UserId == userId).ToListAsync();
            return orders;
        }

        public async Task<int> StoreOrderAsync(int MovieId, string userId)
        {
          var movie=  await _context.Movies.Where(n=>n.Id == MovieId).FirstOrDefaultAsync();
            if (movie == null) return -1;

            var newOrder = new OrderAPI
            {
                MovieId = MovieId,
                Price = movie.Price,
                UserId = userId
            };

           await  _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();
            return newOrder.Id;
        }
    }
}
