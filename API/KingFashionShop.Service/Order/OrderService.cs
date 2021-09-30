using Dapper;
using KingFashionShop.Domain.Models;
using KingFashionShop.Domain.Response.CheckOut;
using KingFashionShop.Service.CartService;
using KingFashionShop.Service.TransactionService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace KingFashionShop.Service.Order
{
    public class OrderService : BaseService, IOrderService
    {
        private ICartService cartService;
        private ICartItemService cartItemService;
        private IOrderItemService orderItemService;
        private ITransactionService transactionService;
        public OrderService(IConfiguration configuration) : base(configuration)
        {
            cartService = new CartService.CartService(configuration);
            cartItemService = new CartItemService(configuration);
            orderItemService = new OrderItemService(configuration);
            transactionService = new TransactionService.TransactionService(configuration);

        }
        public async Task<Domain.Models.Order> Checkout(CheckoutOrder checkoutOrder)
        {
            var cart = await cartService.GetBySessionId(checkoutOrder.SessionId);
            checkoutOrder.TransferFields(cart);
            await cartService.UpdateCart(cart);
            cart = await cartService.GetBySessionId(checkoutOrder.SessionId);
            float tax = 0;
            float shipping = 0;
            float discount = 0;
            float itemDiscount = 0;
            float subTotal = 0;
            float total = 0;
            float grandTotal = 0;
            foreach (var cartItem in cart.CartItems)
            {
                subTotal += (cartItem.Price * cartItem.Quantity);
                itemDiscount += cartItem.Discount;
            }
            total = subTotal + tax + shipping;
            grandTotal = total;
            var order = new Domain.Models.Order()
            {
                FirstName = cart.FirstName,
                MiddleName = cart.MiddleName,
                LastName = cart.LastName,
                Mobile = cart.Mobile,
                Email = cart.Email,
                Line1 = cart.Line1,
                Line2 = cart.Line2,
                Province = cart.Province,
                City = cart.City,
                Country = cart.Country,
                SubTotal = subTotal,
                Total = total,
                GrandTotal = grandTotal,
                Token = Guid.NewGuid().ToString(),
                SessionId = cart.SessionId,
                Tax = tax,
                Discount = discount,
                ItemDiscount = itemDiscount,
                Shipping = shipping,
                Content = cart.Content,
                Status = Domain.Models.OrderStatus.New
            };
            order = await CreateOrder(order);
            foreach (var cartItem in cart.CartItems)
            {
                await orderItemService.CreateOrderItem(new Domain.Models.OrderItem()
                {
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity,
                    Discount = cartItem.Discount,
                    SKU = cartItem.SKU,
                    Content = cartItem.Content

                });
            }
            if(order.UserId != null)
            {
                await transactionService.CreateTransaction(new Domain.Models.CreateTransaction()
                {
                    UserId = order.UserId,
                    CreatedAt = order.CreatedAt,
                    OrderId = order.Id,
                    Content = order.Content
                });
            }
           
            return order;

        }
        public async Task<Domain.Models.Order> CreateOrder(Domain.Models.Order order)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@userId", order.UserId);
            parameters.Add("@sessionId", Guid.NewGuid().ToString());
            parameters.Add("@token", order.Token);
            parameters.Add("@status", order.Status);
            parameters.Add("@subTotal", order.SubTotal);
            parameters.Add("@itemDiscount", order.ItemDiscount);
            parameters.Add("@tax", order.Tax);
            parameters.Add("@shipping", order.Shipping);
            parameters.Add("@total", order.Total);
            parameters.Add("@promo", order.Promo);
            parameters.Add("@discount", order.Discount);
            parameters.Add("@grandTotal", order.GrandTotal);
            parameters.Add("@firstName", order.FirstName);
            parameters.Add("@middleName", order.MiddleName);
            parameters.Add("@lastName", order.LastName);
            parameters.Add("@mobile", order.Mobile);
            parameters.Add("@email", order.Email);
            parameters.Add("@line1", order.Line1);
            parameters.Add("@line2", order.Line2);
            parameters.Add("@city", order.City);
            parameters.Add("@province", order.Province);
            parameters.Add("@country", order.Country);
            parameters.Add("@createdAt", DateTime.Now);
            parameters.Add("@Content", order.Content);

            return await SqlMapper.QueryFirstOrDefaultAsync<Domain.Models.Order>(
                   cnn: connection,
                   param: parameters,
                   sql: "sp_CreateOrderr",
                   commandType: CommandType.StoredProcedure
                   );

        }

        //public async Task<Domain.Models.Order> UpdateOrder(Domain.Models.Order order)
        //{

        //    return null;
        //}
    }
}
