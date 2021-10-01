using KingFashionShop.Commons.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response.ProductRespones
{
    public class ChangeShopResult
    {
        public bool Success { get; set; }
        public string Message => Success ? ResponseMessage.Product.ChangeStatus : ResponseMessage.Fail;
    }
}
