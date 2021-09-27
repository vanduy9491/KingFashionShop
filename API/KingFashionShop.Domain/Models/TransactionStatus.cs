using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Models
{
    public enum TransactionStatus
    {
        New = 1 ,
        Cancelled = 2,
        Failed = 3,
        Pending = 4,
        Declined = 5,
        Rejected = 6,
        Success = 7
    }
}
