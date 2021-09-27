using KingFashion.Models.Products;
using KingFashionShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response.Cart
{
    public class CartItemResponse
    {

        public int Id { get; set; }
        public ProductResult Product { get; set; }
        public int CartId { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public int Quantity { get; set; }
        public string Content { get; set; }

        public CartItemResponse()
        {

        }

        public CartItemResponse(CartItem cartItem)
        {
            Id = cartItem.Id;
            Product = new ProductResult(cartItem.Product);
            CartId = cartItem.CartId;
            Price = cartItem.Price;
            Discount = cartItem.Discount;
            Quantity = cartItem.Quantity;
            Content = cartItem.Content;
        }
    }
}
