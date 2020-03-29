using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartApp.Models
{
    public class CartModel
    {
        public int idProduct { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
