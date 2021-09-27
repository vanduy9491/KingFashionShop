using Dapper;
using KingFashionShop.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace KingFashionShop.Service.Order
{
    public class OrderItemService : BaseService, IOrderItemService
    {

        public OrderItemService(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task< OrderItem> CreateOrderItem(OrderItem orderItem)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@productId", orderItem.ProductId);
            parameters.Add("@orderId", orderItem.OrderId);
            parameters.Add("@sku", orderItem.SKU);
            parameters.Add("@price", orderItem.Price);
            parameters.Add("@discount", orderItem.Discount);
            parameters.Add("@quantity", orderItem.Quantity);
            parameters.Add("@createdAt", orderItem.CreatedAt);
            parameters.Add("@content", orderItem.Content);
            return await SqlMapper.QueryFirstOrDefaultAsync<OrderItem>(
                cnn: connection, param: parameters, sql: "sp_CreateOrderItem", commandType: CommandType.StoredProcedure
                );
        }
    }
}
