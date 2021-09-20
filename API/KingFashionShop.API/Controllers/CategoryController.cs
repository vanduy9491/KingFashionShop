using KingFashionShop.Domain.Response.Categories;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryRespone>> Get()
        {
            return await categoryService.Get();
        }
        [HttpGet("{id}")]
        public async Task<IEnumerable<CategoryRespone>> GetByParentId(int id)
        {
            return await categoryService.GetByParentId(id);
        }

        [HttpPost]
        public async Task<CreateCategoryResult> Create(CreateCategory create)
        {
            return await categoryService.Create(create);
        }
    }
}
