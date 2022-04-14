using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieMVC.General.Infrastructure.Sessions;
using MovieMVC.Service.Abstractions;
using MovieWebApi.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieWebApi.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MovieMVC.General.Infrastructure.Sessions;

namespace MovieMVC.General.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMovieService _service;
        private readonly IOrdersService _orders;
        public OrderController(IMovieService service, IOrdersService orders)
        {
            _service = service;
            _orders = orders;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var orders = await _orders.GetOrdersByUserAsync(userId);
            return View(orders);
        }
        [Authorize]
        public async Task<IActionResult> CompleteOrder()
        {
            var items = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart");
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string mail = User.FindFirstValue(ClaimTypes.Email);
            var order = new Order(){
                UserId=userId,
                Email=mail
            };


            var list = new List<OrderMovie>();
            foreach (var item in items)
            {
                list.Add(new OrderMovie
                {
                    Amount = item.amount,
                    MovieId = item.movie.Id,
                    OrderId = order.Id,
                    Price = item.movie.Price
                });
            }
            await _orders.StoreOrderAsync(order, list);

            List<ShoppingCartItem> cart = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart");
            cart.Clear();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);


            return View("OrderCompleted");
        }

        public IActionResult ShoppingCart()
        {            
            var cart = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart");
            // if(cart.Any(item=>item.movie)==null) return RedirectToAction(nameof(Index));

            try
            {
                var response = new ShoppingCartViewModel()
                {
                    ShoppingCartItem = cart,
                    ShoppingCartTotal = cart.Sum(item => item.movie.Price * item.amount)
                };


                return View(response);
            }
            catch(Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
    
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            if (string.IsNullOrEmpty(id.ToString())||id==0) return RedirectToAction(nameof(Index));


            var productModel = await _service.GetByIdAsync(id);
            if (SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart") == null)
            {
                List<ShoppingCartItem> cart = new List<ShoppingCartItem>();
                cart.Add(new ShoppingCartItem { movie= productModel, amount = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<ShoppingCartItem> cart = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].amount++;
                }
                else
                {
                    cart.Add(new ShoppingCartItem { movie = productModel, amount = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction(nameof(ShoppingCart));
           
        }

        public IActionResult RemoveItemFromShoppingCart(int id)
        {
            List<ShoppingCartItem> cart = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction(nameof(ShoppingCart));
            
        }
        private int isExist(int id)
        {
            List<ShoppingCartItem> cart = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].movie.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
