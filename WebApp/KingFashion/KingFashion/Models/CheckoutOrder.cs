using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response.CheckOut
{
   public class CheckoutOrder
    {
        public string SessionId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
    }
}
