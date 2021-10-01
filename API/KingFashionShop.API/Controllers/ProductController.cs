﻿using KingFashion.Models.Products;
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
        [HttpGet("productId")]
        public async Task<IEnumerable<ProductRespone>> GetProductById([FromQuery] int productId)
        {
            return await productService.GetProductById(productId);
        }
        [HttpGet("catId")]
        public async Task<IEnumerable<ProductRespone>> Get([FromQuery] int catId)
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
        [HttpGet("GetProductBy")]
        public async Task<ProductResult> GetProductBy([FromQuery] int productId)
        {
            var product = await productService.GetProduct(productId);
            return new ProductResult(product);
        }
        [HttpGet("GetProductsTopCategory")]
        public async Task<IEnumerable<ProductResult>> GetProductsTopCategory([FromQuery] int? topCategoryId, [FromQuery] int? boundary, [FromQuery] int limit)
        {
            if (!boundary.HasValue)
                boundary = -1;
            if (!topCategoryId.HasValue)
                topCategoryId = -1;
            return await productService.GetProductsTopCategory(topCategoryId.Value, limit, boundary.Value);
        }

        [HttpGet("GetProductByCategoryId")]
        public async Task<BoundaryList<Product>> GetProductByCategoryId([FromQuery] int categoryId, [FromQuery] int boundary, [FromQuery] int limit, [FromQuery] bool? isCategoryParent = false)
        {
            if (!isCategoryParent.HasValue)
                isCategoryParent = false;
            return await productService.GetProductByCategoryId(categoryId, isCategoryParent.Value, boundary, limit);
        }

        [HttpPut]
        public async Task<UpdateProductResult> Update(UpdateProduct update)
        {
            return await productService.Update(update);

        }
        [HttpPut]
        [Route("ChangeShop")]
        public async Task<ChangeShopResult> ChangeShop(ChangeShop changeShop)
        {
            return await productService.ChangeShop(changeShop);
        }
    }
}
