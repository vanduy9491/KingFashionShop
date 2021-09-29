using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response.CheckOut
{
   public class CheckoutOrder
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }

        public void TransferFields(Models.Cart cart) {
            cart.FirstName = FirstName;
            cart.LastName = LastName;
            cart.Mobile = Mobile;
            cart.Email = Email;
            cart.Line1 =  Line1;
            cart.Line2 = Line2;
            cart.Province = Province;
            cart.Country = "VN";
        }
    }
}
