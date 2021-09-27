using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Models.Products
{
    public class ChangeShopResult
    {
        public Product Product { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
