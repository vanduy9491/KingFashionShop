using Dapper;
using KingFashionShop.Domain.Models;
using KingFashionShop.Domain.Response.ProductRespones;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using KingFashion.Models.Products;
using KingFashionShop.Domain.ProductRespones;

namespace KingFashionShop.Service.TransactionService
{
    public class TransactionService : BaseService, ITransactionService
    {
        public TransactionService(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<CreateTransaction> CreateTransaction(CreateTransaction create)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@userId", create.UserId);
            parameters.Add("@orderId", create.OrderId);
            parameters.Add("@code", create.Code);
            parameters.Add("@type", create.Type);
            parameters.Add("@mode", create.Mode);
            parameters.Add("@status", create.Status);
            parameters.Add("@createdAt", create.CreatedAt);          
            parameters.Add("@content", create.Content);

            return await SqlMapper.QueryFirstOrDefaultAsync<CreateTransaction>(
                 cnn: connection,
                 param: parameters,
                 sql: "sp_CreateTransaction",
                 commandType: CommandType.StoredProcedure
                 );
        }
    }
}
