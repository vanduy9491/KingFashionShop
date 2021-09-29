using KingFashion.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Services
{
    public interface IUserService
    {
        Task<LoginResult> Login(Login LoginUser);
        void Sighout();
        Task<RegisterResult> Register(Register register);
    }
}
