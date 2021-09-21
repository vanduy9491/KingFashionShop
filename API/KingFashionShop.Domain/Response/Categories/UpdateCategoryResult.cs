using KingFashionShop.Commons.Messages;
using KingFashionShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response.Categories
{
    public class UpdateCategoryResult
    {
        public Category Category { get; set; }
        public bool IsExist { get; set; }
        public bool Success => Category != null && Category.ParentId > 0 && !IsExist;
        public string Message => Success ? ResponseMessage.Category.Update :
                                (IsExist ? ResponseMessage.Category.Exits : ResponseMessage.Fail);
    }
}
