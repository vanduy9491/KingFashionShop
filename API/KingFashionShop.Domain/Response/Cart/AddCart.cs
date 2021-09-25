using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response.Cart
{
    public class AddCart
    {
        public string sessionId { get; set; }
        public int productId { get; set; }
    }
}
