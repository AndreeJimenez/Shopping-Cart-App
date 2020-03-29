using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartApp.Models
{
    class CheckoutModel
    {
        public int idCart { get; set; }
        public DateTime dateCheckout { get; set; }
        public string schoolID { get; set; }
        public string studentName { get; set; }
        public string productsIDs { get; set; }
    }
}
