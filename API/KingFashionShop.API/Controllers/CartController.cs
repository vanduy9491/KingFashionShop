using KingFashionShop.Domain.Response.Cart;
using KingFashionShop.Domain.Response.Categories;
using KingFashionShop.Service.CartService;
using KingFashionShop.Service.CategoryService;
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
    public class CartController : ControllerBase
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet("GetBysessionId")]
        public async Task<CartResponse> GetBySessionId([FromQuery] string sessionId)
        {
            return await cartService.GetBySessionId(sessionId);
        }
        [HttpGet("CheckOut")]
        public async Task CheckOut([FromQuery] string sessionId)
        {
            await cartService.CheckOut(sessionId);
        }
        [HttpGet("UpdateCart")]
        public async Task<CartResponse> UpdateCart( UpdateCart updateCart)
        {
            return await cartService.UpdateCart(updateCart);
        }
        [HttpPost("Add")]
        public async Task<CartResponse> CreateCart(AddCart addCart)
        {
            return await cartService.AddCart(addCart);
        }
    }
}
