using KingFashionShop.Domain.Models;
using KingFashionShop.Domain.Response.Cart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KingFashionShop.Service.CartService
{
    public interface ICartItemService
    {
        public Task<CartItem> GetByCartId(string cartId);
        public Task<CartItem> CreateCartItem(CartItem cartItem);
        public Task<CartItem> UpdateCartItem(CartItem cartItem);

        public Task<IEnumerable<CartItem>> GetCartItemsByCartId(int cartId);

      
    }
}
