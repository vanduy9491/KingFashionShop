using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KingFashionShop.Service.Order
{
    public interface IOrderItemService
    {
        Task<Domain.Models.OrderItem> CreateOrderItem(Domain.Models.OrderItem orderItem);
    }
}
