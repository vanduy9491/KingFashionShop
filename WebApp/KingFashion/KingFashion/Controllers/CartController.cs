using KingFashion.Commons;
using KingFashion.Helpers;
using KingFashion.Models;
using KingFashion.Models.Cart;
using KingFashion.Models.Categorys;
using KingFashion.Models.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Controllers
{
    public class CartController : Controller
    {
        // get Cart
        [Route("/Cart")]
        public async Task<IActionResult> Cart()
        {
            ShopAll shopAll = new ShopAll();
            shopAll.Categories = await ApiHelper.HttpGet<List<CategoryResult>>(@$"{Common.ApiUrl}Category");
            shopAll.Products = await ApiHelper.HttpGet<List<ProductResult>>(@$"{Common.ApiUrl}Product");
            return View(shopAll);
        }
        //get Cart
        [HttpPost]
        [Route("/Cart/Add")]
        public async Task<IActionResult> Create(CreateCart model)
        {
            return Ok(await ApiHelper.HttpPost<CreateCartResult>(@$"{Common.ApiUrl}Cart", "POST", model));
        }
    }
}
