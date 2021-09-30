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
    public class BandUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("/BandUser/Get")]
        public async Task<IActionResult> Get()
        {
            var data = await ApiHelper.HttpGet<List<ViewUsers>>(@$"{Common.ApiUrl}User/Band");
            return Ok(data);
        }
        [HttpPut]
        [Route("/BandUser/ChangeIsDeleted")]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeIsDeletedUser model)
        {
            return Ok(await ApiHelper.HttpPost<ChangeIsDeletedUserResult>(@$"{Common.ApiUrl}User/ChangeIsDeleted", "PUT", model));
        }
    }
}
