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
        public Task<CartResponse> GetBySessionId(string sessionId);
        public Task<CartResponse> AddCart(AddCart addCart);
        public Task<CartResponse> UpdateCart(UpdateCart updateCart);

        public Task<CartResponse> CreateCart(UpdateCart updateCart);
        public Task CheckOut(string sessionId);
        Task<CartResponse> ChangeItem(ChangeCart changeCart);
        Task<CartResponse> Remove(RemoveCart removeCart);
    }
}
