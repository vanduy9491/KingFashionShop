using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Models
{
    public enum CartStatus
    {
        New = 1 , 
        Cart = 2,
        CheckOut = 3,
        Paid = 4,
        Complete = 5,
        Abadoned = 6
    }
}
