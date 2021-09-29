using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Models.Carts
{
    public enum CartStatus
    {
        New = 1,
        Cart = 2,
        CheckOut = 3,
        Paid = 4,
        Complete = 5,
        Abadoned = 6
    }
}
