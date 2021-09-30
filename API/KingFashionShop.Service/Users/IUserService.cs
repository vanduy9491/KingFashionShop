using KingFashionShop.Domain.Response.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KingFashionShop.Service.Users
{
    public interface IUserService
    {
        public Task<IEnumerable<UsersRespone>> getallUserNoBand();
        public Task<IEnumerable<UsersRespone>> getallUserBand();
        Task<UsersRespone> GetByUserId(string userId);
        Task<ChangeIsDeletedUserResult> ChangeIsDeleted(ChangeIsDeletedUser changeIsDeleted);
    }
}
