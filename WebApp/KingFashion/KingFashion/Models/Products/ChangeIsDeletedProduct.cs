using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Models.Products
{
    public class ChangeIsDeletedProduct
    {
        public int ProductId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
