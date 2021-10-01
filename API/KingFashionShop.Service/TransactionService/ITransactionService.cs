using KingFashion.Models.Products;
using KingFashionShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KingFashionShop.Service.TransactionService
{
    public interface ITransactionService
    {
        Task<CreateTransaction> CreateTransaction(CreateTransaction create);
    }
}
