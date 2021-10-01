using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response.Users
{
    public class ChangeIsDeletedUser
    {
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
