using System.Collections.Generic;

namespace MovieMVC.General.Infrastructure.Sessions
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCartItem> ShoppingCartItem{get;set;}
        public double ShoppingCartTotal { get; set; }
    }
}
