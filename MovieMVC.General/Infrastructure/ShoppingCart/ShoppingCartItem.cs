using MovieWebApi.Domain.POCO;
using System.ComponentModel.DataAnnotations;


namespace MovieMVC.General.Infrastructure.Sessions
{ 
    public class ShoppingCartItem
    {
        //[Key]
        //public int Id { get; set; }
        public Movies movie { get; set; }
        public int amount { get; set; }

    }
}
