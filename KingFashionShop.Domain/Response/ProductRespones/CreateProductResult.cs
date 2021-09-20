using KingFashionShop.Commons.Messages;
using KingFashionShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response.ProductRespones
{
    public class CreateProductResult
    {
        public Product Product { get; set; }
        public bool IsExitst { get; set; }
        public bool Success => Product != null && Product.Id > 0 && !IsExitst;
        public string Message => Success ? ResponseMessage.Product.Create : (IsExitst ? ResponseMessage.Product.Exits : ResponseMessage.Fail);
    }
}
