using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Models
{
    public enum OrderStatus
    {
        New = 1 , 
        CheckOut = 3,
        Paid = 4,
        Failed = 5,
        Shipped = 6,
        Delivered = 7,
        Returned = 8,
        Complete = 9
    }
}
