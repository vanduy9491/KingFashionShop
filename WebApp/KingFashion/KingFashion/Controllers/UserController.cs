using KingFashion.Commons;
using KingFashion.Helpers;
using KingFashion.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpGet]
        [Route("/User/Get")]
        public async Task<IActionResult> Get()
        {
            var data = await ApiHelper.HttpGet<List<ViewUsers>>(@$"{Common.ApiUrl}User/NoBand");
            return Ok(data);
        }
        [HttpGet]
        [Route("/User/GetByUserId")]
        public async Task<IActionResult> GetByUserId([FromQuery] string userId)
        {
            var data = await ApiHelper.HttpGet<List<ViewUsers>>(@$"{Common.ApiUrl}User/{userId})");
            return Ok(data);
        }
        [HttpPut]
        [Route("/User/ChangeIsDeleted")]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeIsDeletedUser model)
        {
            return Ok(await ApiHelper.HttpPost<ChangeIsDeletedUserResult>(@$"{Common.ApiUrl}User/ChangeIsDeleted", "PUT", model));
        }
    }
}
