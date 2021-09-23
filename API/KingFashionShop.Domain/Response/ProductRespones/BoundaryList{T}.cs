using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.ProductRespones
{
   public class BoundaryList<T>
    {
        public IEnumerable<T> Items { get; set; }
        public object Boundary { get; set; }

    }
}
