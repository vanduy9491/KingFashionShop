using System;
using System.Collections.Generic;
using System.Text;
using KingFashionShop.Commons.Messages;
using KingFashionShop.Domain.Models;

namespace KingFashionShop.Domain.Response.Categories
{
    public class CreateCategoryResult
    {
        public Category Category { get; set; }
        public bool IsExitst { get; set; }
        public bool Success => Category != null && Category.Id > 0 && !IsExitst;
        public string Message => Success ? ResponseMessage.Category.Create : (IsExitst ? ResponseMessage.Category.Exits : ResponseMessage.Fail);
    }
}
