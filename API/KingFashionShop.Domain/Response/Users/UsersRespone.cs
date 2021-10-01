using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response.Users
{
    public class UsersRespone
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
    }
}
