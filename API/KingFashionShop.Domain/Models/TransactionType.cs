using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Models
{
    public enum TransactionModel
    {
        Offline = 1,
        CashOnDelivery = 2,
        Cheque = 3,
        Draft = 4,
        Wired = 5,
        Online = 6
    }
       
}
