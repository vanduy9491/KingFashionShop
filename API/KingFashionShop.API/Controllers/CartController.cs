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

        [HttpGet("GetBySessionId")]
        public async Task<CartResponse> GetBySessionId()
        {
            var sessionId = Request.Cookies["sessionId"];
            var cart = await cartService.GetBySessionId(sessionId);
            return CartResponse.ToCartResult(cart);
          
        }
      
        [HttpGet("ChangeItem")]
        public async Task<CartResponse> ChangeItem([FromQuery] int productId, [FromQuery] int quantity)
        {
            var cart = await cartService.ChangeItem(new ChangeCart() { 
             sessionId = Request.Cookies["sessionId"],
             productId = productId,
             quantity = quantity
            });
            return CartResponse.ToCartResult(cart);
        }

        [HttpGet("Remove")]
        public async Task<CartResponse> RemoveItem([FromQuery] int productId)
        {
            var cart = await cartService.Remove(new RemoveCart()
            {
                sessionId = Request.Cookies["sessionId"],
                productId = productId,
            });
            return CartResponse.ToCartResult(cart);

        }
        [HttpPost("Add")]
        public async Task<CartResponse> CreateCart(AddCart addCart)
        {
            addCart.sessionId = Request.Cookies["sessionId"];
            var cart = await cartService.AddCart(addCart);
            return CartResponse.ToCartResult(cart);
        }
    }
}
