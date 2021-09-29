using KingFashionShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response.Cart
{
    public class CartResponse
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public string Token { get; set; }
        public CartStatus Status { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }

        public IEnumerable<CartItemResponse> Items { get; set; }

        public CartResponse()
        {
                
        }

        public static CartResponse ToCartResult(Models.Cart cart) {
            CartResponse result = new CartResponse();
            if (cart == null) return result;
            result.Id = cart.Id;
            result.SessionId = cart.SessionId;
            result.Token = cart.Token;
            result.Status = cart.Status;
            result.FirstName = cart.FirstName;
            result.MiddleName = cart.MiddleName;
            result.LastName = cart.LastName;
            result.Mobile = cart.Mobile;
            result.Email = cart.Email;
            result.Line1 = cart.Line1;
            result.Line2 = cart.Line2;
            result.City = cart.City;
            result.Province = cart.Province;
            result.Country = cart.Country;
            var cartItems = new List<CartItemResponse>();

            foreach (var item in cart.CartItems)
            {
                cartItems.Add(new CartItemResponse(item));
            }
            result.Items = cartItems;
            return result;
        }
    }
}
