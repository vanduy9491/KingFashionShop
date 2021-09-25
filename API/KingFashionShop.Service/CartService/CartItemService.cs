using Dapper;
using KingFashion.Models.Products;
using KingFashionShop.Domain.Models;
using KingFashionShop.Domain.Response.Cart;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace KingFashionShop.Service.CartService
{
    public class CartItemService : BaseService, ICartItemService
    {
        public ProductService.ProductService productService { get; set; }
        public CartItemService(IConfiguration configuration) : base(configuration)
        {
            productService = new ProductService.ProductService(configuration);
        }

        public Task<CartItem> GetByCartId(string cartId)
        {
            throw new NotImplementedException();
        }

        public async Task<CartItem> CreateCartItem(CartItem cartItem)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@productId", cartItem.ProductId);
            parameters.Add("@cardId", cartItem.CartId);
            parameters.Add("@SKU", cartItem.SKU);
            parameters.Add("@price", cartItem.Price);
            parameters.Add("@discount", cartItem.Discount);
            parameters.Add("@quantity", cartItem.Quantity);
            parameters.Add("@active", cartItem.Active);
            parameters.Add("@createAt", cartItem.CreatedAt);
            parameters.Add("@content", cartItem.CartId);

            return await SqlMapper.QueryFirstOrDefaultAsync<CartItem>(
                  cnn: connection,
                  param: parameters,
                  sql: "sp_CreateCartItem",
                  commandType: CommandType.StoredProcedure
                  );
        }

        public async Task<CartItem> UpdateCart(CartItem cartItem)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id",cartItem.Id);
            parameters.Add("@productId", cartItem.ProductId);
            parameters.Add("@cartId", cartItem.CartId);
            parameters.Add("@SKU", cartItem.SKU);
            parameters.Add("@price", cartItem.Price);
            parameters.Add("@discount", cartItem.Discount);
            parameters.Add("@quantity", cartItem.Quantity);
            parameters.Add("@active", cartItem.Active);
            parameters.Add("@updatedAt", cartItem.UpdatedAt);
            parameters.Add("@content", cartItem.Content);

            return await SqlMapper.QueryFirstOrDefaultAsync<CartItem>(
                cnn: connection,
                param: parameters,
                sql: "sp_GetCartItemsByCartId",
                commandType: CommandType.StoredProcedure
                );
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByCartId(int cartId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@cartId", cartId);
            return await SqlMapper.QueryAsync<CartItem>(
                cnn: connection,
                param: parameters,
                sql: "sp_GetCartItemsByCartId",
                commandType: CommandType.StoredProcedure
                );
        }
    }
}
