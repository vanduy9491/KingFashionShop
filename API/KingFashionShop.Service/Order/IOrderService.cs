using KingFashionShop.Domain.Response.Cart;
using KingFashionShop.Domain.Response.CheckOut;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KingFashionShop.Service.Order
{
   public interface IOrderService
    {
        Task<Domain.Models.Order> Checkout(CheckoutOrder checkoutOrder);
        Task<Domain.Models.Order> CreateOrder(Domain.Models.Order order);
        Task<Domain.Models.Order> GetOrderBySessionId(string sessionId);
        //Task<Domain.Models.Order> UpdateOrder(Domain.Models.Order order);
    }
}
