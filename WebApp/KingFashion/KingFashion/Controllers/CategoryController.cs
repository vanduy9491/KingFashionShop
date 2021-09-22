using KingFashion.Commons;
using KingFashion.Helpers;
using KingFashion.Models.Categorys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("/Category/Get")]
        public async Task<IActionResult> Get()
        {
            var data = await ApiHelper.HttpGet<List<Category>>(@$"{Common.ApiUrl}Category");
            return Ok(data);
        }
        [HttpGet]
        [Route("/Category/GetByParentId")]
        public async Task<IActionResult> GetByParentId([FromQuery] int parentId)
        {
            var data = await ApiHelper.HttpGet<List<Category>>(@$"{Common.ApiUrl}Category/GetByParentId?parentId="+ parentId);
            return Ok(data);
        }
        [HttpGet]
        [Route("/Category/GetCategoryById")]
        public async Task<Category> GetCategoryById([FromQuery] int id)
        {
            return await ApiHelper.HttpGet<Category>(@$"{Common.ApiUrl}Category/GetCategoryById?id=" + id);
        }

        [HttpPost]
        [Route("/Category/Create")]
        public async Task<IActionResult> Create([FromBody] CreateCategory model)
        {
            return Ok(await ApiHelper.HttpPost<CreateCategoryResult>(@$"{Common.ApiUrl}Category", "POST", model));
        }
        [HttpPut]
        [Route("/Category/Update")]
        public async Task<IActionResult> Update([FromBody] UpdateCategory model)
        {
            return Ok(await ApiHelper.HttpPost<UpdateCategoryResult>(@$"{Common.ApiUrl}Category", "PUT", model));
        }
        [HttpPut]
        [Route("/Category/ChangeStatus")]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusCategory model)
        {
            return Ok(await ApiHelper.HttpPost<ChangeStatusCategoryResult>(@$"{Common.ApiUrl}Category/ChangeStatus", "PUT", model));
        }
    }
}
