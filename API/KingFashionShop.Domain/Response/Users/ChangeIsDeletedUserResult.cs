using KingFashionShop.Commons.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response.Users
{
    public class ChangeIsDeletedUserResult
    {
        public bool Success { get; set; }
        public string Message => Success ? ResponseMessage.User.ChangeIsDeleted : ResponseMessage.Fail;
    }
}
