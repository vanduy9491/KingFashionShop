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

namespace KingFashionShop.Service.ProductService
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<IEnumerable<ProductRespone>> Get()
        {
            var products = await SqlMapper.QueryAsync<ProductRespone>(
                                 cnn: connection,
                                 sql: "sp_GetAllProduct",
                                 commandType: CommandType.StoredProcedure);
            return products;
        }
        public async Task<IEnumerable<ProductRespone>> Get(int catId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@categoryId", catId);
            var products = await SqlMapper.QueryAsync<ProductRespone>(
                cnn: connection,param: parameters, sql:"sp_GetListProduct", commandType: CommandType.StoredProcedure
                );
            return products;
        }
        public async Task<Product> GetProductByName(string title, int id = 0)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@title", title);
            parameters.Add("@id", id);
            var product = await SqlMapper.QueryFirstOrDefaultAsync<Product>(
                                cnn: connection,
                                sql: "sp_GetProductByName",
                                param: parameters,
                                commandType: CommandType.StoredProcedure);
            return product;
        }

        public async Task<CreateProductResult> Create(CreateProduct create)
        {
            try
            {
                var foundProduct = await GetProductByName(create.Title);

                if (foundProduct == null)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@userId", create.UserId);
                    parameters.Add("@title", create.Title);
                    parameters.Add("@metaTitle", create.MetaTitle);
                    parameters.Add("@slug", create.Slug);
                    parameters.Add("@summary", create.Summary);
                    parameters.Add("@type", create.Type);
                    parameters.Add("@SKU", create.SKU);
                    parameters.Add("@price", create.Price);
                    parameters.Add("@discount", create.Discount);
                    parameters.Add("@quantity", create.Quantity);
                    parameters.Add("@shop", create.Shop);
                    parameters.Add("@createdAt", create.CreatedAt);
                    parameters.Add("@updatedAt", create.UpdateAt);
                    parameters.Add("@publishedAt", create.PublishedAt);
                    parameters.Add("@startsAt", create.StartsAt);
                    parameters.Add("@endsAt", create.EndsAt);
                    parameters.Add("@content", create.Content);
                    parameters.Add("@categoryId", create.CategoryId);
                    parameters.Add("@photo", create.Photo);
                    var product = await SqlMapper.QueryFirstOrDefaultAsync<Product>(
                                            cnn: connection,
                                            sql: "sp_CreateProduct",
                                            param: parameters,
                                            commandType: CommandType.StoredProcedure
                                        );
                    return new CreateProductResult()
                    {
                        IsExitst = false,
                        Product = product
                    };
                }
                return new CreateProductResult()
                {
                    Product = foundProduct,
                    IsExitst = true
                };
            }
            catch (Exception ex)
            {
                return new CreateProductResult()
                {
                    Product = new Product()
                };
            }
        }

        public async Task<Product> GetProduct(int proId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@proId", proId);
            var product = await SqlMapper.QueryFirstOrDefaultAsync<Product>(
                                cnn: connection,
                                sql: "sp_GetProductById",
                                param: parameters,
                                commandType: CommandType.StoredProcedure);
            return product;
        }

        public async Task<IEnumerable<ProductResult>> GetProductsTopCategory(int limit)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@limit", limit);
            var products = await SqlMapper.QueryAsync<ProductResult>(
                cnn: connection, param: parameters, sql: "sp_GetProductsTopCategory", commandType: CommandType.StoredProcedure
                );
            return products;
        }


       
    }
}
