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
        [HttpGet("GetByParentId")]
        public async Task<IEnumerable<CategoryRespone>> GetByParentId([FromQuery]  int parentId)
        {
            return await categoryService.GetByParentId(parentId);
        }

        [HttpGet("GetCategoryById")]
        public async Task<CategoryRespone> GetCategoryById([FromQuery] int id)
        {
            return await categoryService.GetCategoryById(id);
        }

        [HttpPost]
        public async Task<CreateCategoryResult> Create(CreateCategory create)
        {
            return await categoryService.Create(create);
        }
        [HttpPut]
        public async Task<UpdateCategoryResult> Update(UpdateCategory update)
        {
            return await categoryService.Update(update);
        }
        [HttpPut]
        [Route("ChangeStatus")]
        public async Task<ChangeStatusCategoryResult> ChangeStatus(ChangeStatusCategory changeStatus)
        {
            return await categoryService.ChangeStatus(changeStatus);
        }
    }
}
