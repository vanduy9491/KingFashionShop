using KingFashion.Models.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserService(UserManager<User> userManager,
                            SignInManager<User> signInManager,
                            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public async Task<LoginResult> Login(Login LoginUser)
        {
            var user = await userManager.FindByNameAsync(LoginUser.Email);
            if (user == null)
            {
                return new LoginResult()
                {
                    Id = string.Empty,
                    Email = string.Empty,
                    Message = "Người dùng không tồn tại."
                };
            }
            var signInResult = await signInManager.PasswordSignInAsync(user, LoginUser.Password, LoginUser.RememberMe, false);
            if (signInResult.Succeeded)
            {
                var roles = await userManager.GetRolesAsync(user);
                return new LoginResult()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Message = "Đăng nhập thành công",
                    Roles = roles.ToArray()
                };
            }
            return new LoginResult()
            {
                Id = string.Empty,
                Email = string.Empty,
                Message = "Đã xảy ra lỗi, vui lòng thử lại sau."
            };
        }
        public async Task<RegisterResult> Register(Register register)
        {
            var registerResult = new RegisterResult();
            var newUser = new User()
            {
                UserName = register.Email,
                Email = register.Email,
                NormalizedEmail = register.Email,
                NormalizedUserName = register.Email,
                LockoutEnabled = false
            };
            var user = await userManager.CreateAsync(newUser, register.Password);
            if (user.Succeeded)
            {
                var registerUser = await userManager.FindByEmailAsync(register.Email);
                var assignUserRoles = await userManager.AddToRoleAsync(newUser, "Customer");
                if (assignUserRoles.Succeeded)
                {
                    registerResult.Message = "Register succeed.";
                    registerResult.Id = registerUser.Id;
                }

            }
            foreach (IdentityError error in user.Errors)
            {
                registerResult.Message += $"<p>{error.Description}</p>";
            }
            return registerResult;
        }

        public void Sighout()
        {
            signInManager.SignOutAsync().Wait();
        }
    }
}
