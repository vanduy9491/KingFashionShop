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
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }

    }
}
