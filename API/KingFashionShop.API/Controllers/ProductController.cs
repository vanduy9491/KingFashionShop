using KingFashion.Models.Products;
using KingFashionShop.Domain.Models;
using KingFashionShop.Domain.ProductRespones;
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

        [HttpGet]
        public async Task<IEnumerable<ProductRespone>> Get()
        {
            return await productService.Get();
        }
        [HttpGet("{productId}")]
        public async Task<IEnumerable<ProductRespone>> GetProductById(int productId)
        {
            return await productService.GetProductById(productId);
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
        [HttpGet("GetProductsTopCategory")]
        public async Task<IEnumerable<ProductResult>> GetProductsTopCategory([FromQuery] int? limit)
        {
            if (!limit.HasValue)
                limit = -1;
            return await productService.GetProductsTopCategory(limit.Value);
        }
        
        [HttpGet("GetProductByCategoryId")]
        public async Task<BoundaryList<Product>> GetProductByCategoryId([FromQuery] int categoryId, [FromQuery] bool? isCategoryParent ,[FromQuery] int boundary, [FromQuery] int limit)
        {
            return await productService.GetProductByCategoryId(categoryId, isCategoryParent ,boundary, limit);
        }
        [HttpPut]
        public async Task<UpdateProductResult> Update(UpdateProduct update)
        {
            return await productService.Update(update);

        }
    }
}
