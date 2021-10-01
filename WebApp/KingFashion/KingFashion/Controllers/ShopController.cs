using KingFashion.Commons;
using KingFashion.Helpers;
using KingFashion.Models;
using KingFashion.Models.Categorys;
using KingFashion.Models.Contacts;
using KingFashion.Models.Products;
using KingFashionShop.Domain.Response;
using KingFashionShop.Domain.Response.CheckOut;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingFashionWeb.Controllers
{
    public class ShopController : Controller
    {
        private static int categoryId;
        private static int productId;
        private readonly ILogger<ShopController> _logger;
        public ShopController(ILogger<ShopController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            ShopAll shopAll = new ShopAll();
            shopAll.Categories = await ApiHelper.HttpGet<List<CategoryResult>>(@$"{Common.ApiUrl}Category");
            shopAll.Products = await ApiHelper.HttpGet<List<ProductResult>>(@$"{Common.ApiUrl}Product/GetProductsTopCategory?limit=8");
            string sessionId = Request.Cookies["sessionId"];
            if (sessionId == null)
            {
                string data = DateTime.Now.ToString();
                sessionId = Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
                Response.Cookies.Append("sessionId", sessionId, new CookieOptions() { Expires = DateTimeOffset.Now.AddYears(2) });
            }
            return View(shopAll);
        }

        [HttpPost]
        [Route("/Shop/Index/{productId:int}")]
        public async Task<IActionResult> Index(int productId)
        {
            ShopAll shopAll = new ShopAll();
            shopAll.Categories = await ApiHelper.HttpGet<List<CategoryResult>>(@$"{Common.ApiUrl}Category");
            shopAll.Products = await ApiHelper.HttpGet<List<ProductResult>>(@$"{Common.ApiUrl}Product");
            return View(shopAll);
        }

        [HttpGet]
        [Route("/Shop/Get/{id}")]
        public IActionResult Get(int id)
        {
            var data = ApiHelper.HttpGet<Product>(@$"{Common.ApiUrl}Product/{id}");
            return Ok(data);
        }

        [Route("/Shop")]
        public async Task<IActionResult> ViewShop()
        {
            ShopAll shopAll = new ShopAll();
            shopAll.Categories = await ApiHelper.HttpGet<List<CategoryResult>>(@$"{Common.ApiUrl}Category");
            shopAll.Products = await ApiHelper.HttpGet<List<ProductResult>>(@$"{Common.ApiUrl}Product/GetProductsTopCategory?limit=16");

            return View(shopAll);

        }
        [HttpGet]
        [Route("/Shop/Contact")]
        public async Task<IActionResult> Contact()
        {
            
            return View();

        }
        [HttpPost]
        [Route("/Shop/GetProduct/{proId:int}")]
        public async Task<IActionResult> ProductDetails(int proId)
        {
            productId = proId;
            var data = await ApiHelper.HttpGet<List<Product>>(@$"{Common.ApiUrl}Product/GetProduct/{proId}");
            return View(data);
        }
        [Route("/CheckOut")]
        public async Task<IActionResult> CheckOut()
        {
            ShopAll shopAll = new ShopAll();
            shopAll.Categories = await ApiHelper.HttpGet<List<CategoryResult>>(@$"{Common.ApiUrl}Category");
            shopAll.Products = await ApiHelper.HttpGet<List<ProductResult>>(@$"{Common.ApiUrl}Product");
            return View(shopAll);
        }

        [HttpPost]
        [Route("/CheckOut")]
        public async Task<OrderResult> CheckOut([FromQuery] string? firstName , [FromQuery] string? lastName, [FromQuery] string? mobile, [FromQuery] string? email, [FromQuery] string? line1, [FromQuery] string? city, [FromQuery] string? province)
         {
            CheckoutOrder checkoutOrder = new CheckoutOrder() {
                FirstName = firstName,
                LastName = lastName,
                Mobile = mobile,
                Email = email,
                Line1 = line1,
                City = city
            };
            checkoutOrder.SessionId = Request.Cookies["sessionId"];
            var order = await ApiHelper.HttpPost<OrderResult>(@$"{Common.ApiUrl}Order/Checkout", "POST", checkoutOrder);
            Response.Cookies.Delete("sessionId");
            return order;
         }
             [HttpPost]
        [Route("/Shop/Contact")]
        public async Task<IActionResult> Contact(Contact model)
        {
            await ApiHelper.HttpPost<CreateContactResult>($@"{Common.ApiUrl}Contact", "POST", model);
            return View();
        }
    }
}
