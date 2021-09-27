using KingFashion.Models.Categorys;
using KingFashion.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KingFashion.Models
{
    public class ShopAll
    {
        public List<CategoryResult> Categories { get; set; }
        public List<ProductResult> Products { get; set; }

    }
}
