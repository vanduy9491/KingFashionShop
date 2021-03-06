using KingFashionShop.Domain.Models;
using KingFashionShop.Domain.Response.Cart;
using KingFashionShop.Domain.Response.Categories;
using KingFashionShop.Domain.Response.CheckOut;
using KingFashionShop.Service.CartService;
using KingFashionShop.Service.CategoryService;
using KingFashionShop.Service.Order;
using KingFashionShop.Service.TransactionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashionShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
       

        public OrderController(IOrderService orderService)
        { 
            this.orderService = orderService;
        }

        [HttpPost("Checkout")]
        public async Task<Order> CreateCart(CheckoutOrder checkoutOrder)
        {
            return await orderService.Checkout(checkoutOrder);
        }
    }
}
