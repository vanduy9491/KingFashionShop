using KingFashionShop.Commons.Messages;
using KingFashionShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response.ProductRespones
{
    public class UpdateProductResult
    {
        public Product Product{ get; set; }
        public bool IsExist { get; set; }
        public bool Success => Product != null && Product.Id > 0 && !IsExist;
        public string Message => Success ? ResponseMessage.Product.Update :
                                (IsExist ? ResponseMessage.Product.Exits : ResponseMessage.Fail);
    }
}
