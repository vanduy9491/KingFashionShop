﻿using KingFashion.Commons;
using KingFashion.Helpers;
using KingFashion.Models.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Controllers
{
    public class ProductController : Controller
    {
        private static int categoryId;
        private static int productId;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [Route("/Product/Index/{catId}")]
        public async Task<IActionResult> Index(int catId)
        {
            categoryId = catId;

            var data = await ApiHelper.HttpGet<List<Product>>(@$"{Common.ApiUrl}Product/catId?catId={catId}");
            return View(data);
        }
        [HttpGet]
        [Route("/Product/{catId}")]
        public async Task<IActionResult> productById(int proId)
        {
            productId = proId;

            var data = await ApiHelper.HttpGet<List<Product>>(@$"{Common.ApiUrl}Product/GetProduct/{proId}");
            return View(data);
        }

        [HttpGet]
        [Route("/Product/GetProductsTopCategory")]
        public async Task<IActionResult> GetProductsTopCategory(int limit)
        {
            var data = await ApiHelper.HttpGet<List<Product>>(@$"{Common.ApiUrl}/api/Product/GetProductsTopCategory?limit=" + limit);
            return View(data);
        }



        [HttpGet("/Product/Create")]
        public IActionResult Create()
        {
            ViewBag.CatId = categoryId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProduct model)
        {

            if (ModelState.IsValid)
            {
                string filename = "no-img.jpg";
                string fileAllName = "";


                if (model.Photo != null && model.Photo.Count > 0)
                {

                    fileAllName = String.Empty;
                    foreach (IFormFile images in model.Photo)
                    {
                        //filename = String.Empty;
                        string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                        fileAllName += $"{DateTime.Now.ToString("ddMMyyyyhhmmss")}_{images.FileName} ";
                        filename = $"{DateTime.Now.ToString("ddMMyyyyhhmmss")}_{images.FileName} ";
                        var filePath = Path.Combine(uploadFolder, filename);
                        var fileAllPath = Path.Combine(uploadFolder, fileAllName);
                        if (fileAllName.Split(" ").Length > 2)
                        {
                            images.CopyTo(new FileStream(fileAllPath, FileMode.Create));
                        }
                        images.CopyTo(new FileStream(filePath, FileMode.Create));

                    }
                }
                var newProduct = new Product()
                {
                    Photo = fileAllName,
                    Title = model.Title,
                    Price = model.Price,
                    Summary = model.Summary,
                    Quantity = model.Quantity,
                    MetaTitle = model.MetaTitle,
                    Slug = model.Slug,
                    Type = model.Type,
                    Discount = model.Discount,
                    Shop = model.Shop,
                    CreatedAt = model.CreatedAt,
                    UpdateAt = model.UpdateAt,
                    PublishedAt = model.PublishedAt,
                    StartsAt = model.StartsAt,
                    EndsAt = model.EndsAt,
                    Content = model.Content,
                    CategoryId = model.CategoryId,
                };
                await ApiHelper.HttpPost<CreateProductResult>(@$"{Common.ApiUrl}Product", "POST", newProduct);
                return RedirectToAction("Index", "Product", new { catId = categoryId });
            }

            return View();
        }
        [HttpGet("/Product/Update/{proId}")]
        public async Task<IActionResult> Update(int proId)
        {
            return View(await ApiHelper.HttpGet<Product>(@$"{Common.ApiUrl}Product/GetProduct/{proId}"));
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProduct update)
        {
            if (ModelState.IsValid)
            {
                if (update != null)
                {
                    await ApiHelper.HttpPost<UpdateProductResult>(@$"{Common.ApiUrl}Product", "PUT", update);
                    return RedirectToAction("Index", "Product", new { catId = categoryId });
                };
            }
            return View(update);
    }
    [HttpPut]
    [Route("/Product/ChangeShop")]
    public async Task<IActionResult> ChangeStatus([FromBody] ChangeShop model)
    {
        return Ok(await ApiHelper.HttpPost<ChangeShopResult>(@$"{Common.ApiUrl}Product/ChangeShop", "PUT", model));
    }
    [HttpGet]
    [Route("/Product/View/{proId}")]
    public async Task<IActionResult> ViewDetails(int proId)
    {
        return View(await ApiHelper.HttpGet<Product>(@$"{Common.ApiUrl}Product/GetProduct/{proId}"));
    }
}
}
