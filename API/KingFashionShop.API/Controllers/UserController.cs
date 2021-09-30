using KingFashionShop.Domain.Response.Users;
using KingFashionShop.Service.Users;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet("NoBand")]
        public async Task<IEnumerable<UsersRespone>> getUsersNoBand()
        {
            return await userService.getallUserNoBand();
        }
        [HttpGet("Band")]
        public async Task<IEnumerable<UsersRespone>> getUsersBand()
        {
            return await userService.getallUserBand();
        }
        [HttpGet("{id}")]
        public async Task<UsersRespone> GetByUserId(string id)
        {
            return await userService.GetByUserId(id);
        }
        [HttpPut]
        [Route("ChangeIsDeleted")]
        public async Task<ChangeIsDeletedUserResult> ChangeIsDeleted(ChangeIsDeletedUser changeIsDeleted)
        {
            return await userService.ChangeIsDeleted(changeIsDeleted);
        }
    }
}
