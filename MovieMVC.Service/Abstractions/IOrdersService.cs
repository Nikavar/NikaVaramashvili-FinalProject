using MovieMVC.Service.Models;
using MovieWebApi.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieMVC.Service.Abstractions
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(Order order, List<OrderMovie> orderMovie);
        Task<List<Order>> GetOrdersByUserAsync(string userId);
    }
}
