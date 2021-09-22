using KingFashionShop.Domain.Models;
using KingFashionShop.Domain.Response.ProductRespones;
using KingFashionShop.Service.ProductService;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("{catId}")]
        public async Task<IEnumerable<ProductRespone>> Get(int catId)
        {
            return await productService.Get(catId);
        }
        [HttpGet("GetAllProduct")]
        public async Task<IEnumerable<ProductRespone>> GetAllProduct()
        {
            return await productService.GetAllProduct();
        }

        [HttpPost]
        public async Task<CreateProductResult> Create(CreateProduct create)
        {
            return await productService.Create(create);
        }
        [HttpGet("GetProduct/{proId}")]
        public async Task<Product> GetProduct(int proId)
        {
            return await productService.GetProduct(proId);
        }
    }
}
