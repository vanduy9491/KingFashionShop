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

            var data = await ApiHelper.HttpGet<List<Product>>(@$"{Common.ApiUrl}Product/{catId}");
            return View(data);
        }

        [HttpGet("/Product/Create")]
        public async Task<IActionResult> Create()
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
        [HttpPut]
        [Route("/Product/Update")]
        public async Task<IActionResult> Update([FromBody] UpdateProduct model)
        {
            return Ok(await ApiHelper.HttpPost<UpdateProductResult>(@$"{Common.ApiUrl}Product", "PUT", model));
        }
    }
}
