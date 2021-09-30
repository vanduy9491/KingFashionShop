using KingFashion.Models.Users;
using KingFashion.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.Login(login);
                if (login.IsDeleted != true)
                {
                    if (result.Success && result.Roles.Length > 0)
                    {
                        if (result.Roles.Contains("SystemAdmin"))
                        {
                            return RedirectToAction("Index", "DashBoard");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Shop");
                        }
                    }
                    ViewBag.Error = result.Message;
                    return View();
                }
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.Register(register);
                if (result.Success)
                {
                    return RedirectToAction("Login", "Account");
                }
                ViewBag.Error = result.Message;
                return View();
            }
            return View();
        }
        public IActionResult Signout()
        {
            userService.Sighout();
            return RedirectToAction("Login", "Account");
        }

    }
}
