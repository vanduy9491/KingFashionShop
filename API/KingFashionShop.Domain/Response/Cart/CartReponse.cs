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

        public CartResponse(Models.Cart cart, IEnumerable<CartItemResponse> items)
        {
            Id = cart.Id;
            SessionId = cart.SessionId;
            Token = cart.Token;
            Status = cart.Status;
            FirstName = cart.FirstName;
            MiddleName = cart.MiddleName;
            LastName = cart.LastName;
            Mobile = cart.Mobile;
            Email = cart.Email;
            Line1 = cart.Line1;
            Line2 = cart.Line2;
            City = cart.City;
            Province = cart.Province;
            Country = cart.Country;
            Items = items;
        }
    }
}
