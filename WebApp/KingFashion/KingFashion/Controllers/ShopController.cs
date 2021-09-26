using KingFashion.Commons;
using KingFashion.Helpers;
using KingFashion.Models;
using KingFashion.Models.Categorys;
using KingFashion.Models.Products;
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
            if (sessionId == null) { 
                string data = DateTime.Now.ToString();
            sessionId = Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
            Response.Cookies.Append("sessionId", sessionId);
            }
            return View(shopAll);
    }
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
        [Route("/Contact")]
        public async Task<IActionResult> Contact()
        {
            ShopAll shopAll = new ShopAll();
            shopAll.Categories = await ApiHelper.HttpGet<List<CategoryResult>>(@$"{Common.ApiUrl}Category");
            shopAll.Products = await ApiHelper.HttpGet<List<ProductResult>>(@$"{Common.ApiUrl}Product");
            return View(shopAll);

        }
        public async Task<IActionResult> ProductDetails()
        {
            ShopAll shopAll = new ShopAll();
            shopAll.Categories = await ApiHelper.HttpGet<List<CategoryResult>>(@$"{Common.ApiUrl}Category");
            shopAll.Products = await ApiHelper.HttpGet<List<ProductResult>>(@$"{Common.ApiUrl}Product");
            return View(shopAll);

        }
        [Route("/CheckOut")]
        public async Task<IActionResult> CheckOut()
        {
            ShopAll shopAll = new ShopAll();
            shopAll.Categories = await ApiHelper.HttpGet<List<CategoryResult>>(@$"{Common.ApiUrl}Category");
            shopAll.Products = await ApiHelper.HttpGet<List<ProductResult>>(@$"{Common.ApiUrl}Product");
            return View(shopAll);

        }

    }
}