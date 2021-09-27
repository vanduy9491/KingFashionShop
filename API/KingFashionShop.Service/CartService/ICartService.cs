using KingFashionShop.Domain.Models;
using KingFashionShop.Domain.Response.Cart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KingFashionShop.Service.CartService
{
    public interface ICartService
    {
        public Task<Cart> GetBySessionId(string sessionId);
        public Task<Cart> AddCart(AddCart addCart);
        public Task<Cart> UpdateCart(Cart cart);
        Task<Cart> ChangeItem(ChangeCart changeCart);
        Task<Cart> Remove(RemoveCart removeCart);
    }
}
