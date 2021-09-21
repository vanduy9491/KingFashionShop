using System;
using System.Collections.Generic;
using System.Text;
using KingFashionShop.Commons.Messages;
using KingFashionShop.Domain.Models;

namespace KingFashionShop.Domain.Response.Categories
{
    public class ChangeStatusCategoryResult
    {
        public bool Success { get; set; }
        public string Message => Success ? ResponseMessage.Category.ChangeStatus : ResponseMessage.Fail;
    }
}
