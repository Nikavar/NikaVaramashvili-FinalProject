using MovieWebApi.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Service.Abstractions
{
    public interface IOrdersServiceAPI
    {
        Task<int> StoreOrderAsync(int MovieId,string UserId);
        Task<List<OrderAPI>> GetOrdersByUserAsync(string userId);
        Task DeleteOrder(int orderID, string userId);
    }
}
