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
        public async Task<IEnumerable<ProductRespone>> GetAllProduct()
        {
            
            var products = await SqlMapper.QueryAsync<ProductRespone>(
                cnn: connection, sql: "sp_GetAllProduct", commandType: CommandType.StoredProcedure  
                );
            return products;
        }

        public async Task<IEnumerable<ProductRespone>> GetProductById(int productId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@proId", productId);
            var products = await SqlMapper.QueryAsync<ProductRespone>(
                cnn: connection, param: parameters, sql: "sp_GetProductById", commandType: CommandType.StoredProcedure
                );
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

        public async Task<Product> GetProduct(int Id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@proId", Id);
            var product = await SqlMapper.QueryFirstOrDefaultAsync<Product>(
                                cnn: connection,
                                sql: "sp_GetProductById",
                                param: parameters,
                                commandType: CommandType.StoredProcedure);
            return product;
        }

        public async Task<IEnumerable<ProductResult>> GetProductsTopCategory(int topCategoryId,int limit, int boundary)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@limit", limit);
            parameters.Add("@boundary", boundary);
            parameters.Add("@topCategoryId", topCategoryId);
            var products = await SqlMapper.QueryAsync<ProductResult>(
                cnn: connection, param: parameters, sql: "sp_GetProductsTopCategory", commandType: CommandType.StoredProcedure
                );
            return products;
        }
        


        public async Task<BoundaryList<Product>> GetProductByCategoryId(int categoryId, bool isCategoryParent, int boundary, int limit)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@categoryId", categoryId);
            parameters.Add("@boundary", boundary);
            parameters.Add("@limit", limit);
            parameters.Add("@isCategoryParent", isCategoryParent);
            var items = await SqlMapper.QueryAsync<Product>(
                                cnn: connection,
                                sql: "sp_GetProductByCategoryId",
                                param: parameters,
                                commandType: CommandType.StoredProcedure);
            boundary = boundary != 0 ? boundary += limit : limit;
            return new BoundaryList<Product>() { Boundary = boundary, Items = items };
        }
        
        public async Task<UpdateProductResult> Update(UpdateProduct update)
        {
            UpdateProductResult updateProduct = new UpdateProductResult()
            {
                IsExist = false,
            };
            try
            {
                var product = await GetProduct(update.Id);
                if (product == null)
                    return updateProduct;

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", update.Id);
                parameters.Add("@userId", update.UserId);
                parameters.Add("@title", update.Title);
                parameters.Add("@metaTitle", update.MetaTitle);
                parameters.Add("@slug", update.Slug);
                parameters.Add("@summary", update.Summary);
                parameters.Add("@type", update.Type);
                parameters.Add("@SKU", update.SKU);
                parameters.Add("@price", update.Price);
                parameters.Add("@discount", update.Discount);
                parameters.Add("@quantity", update.Quantity);
                parameters.Add("@shop", update.Shop);
                parameters.Add("@createdAt", update.CreatedAt);
                parameters.Add("@updatedAt", update.UpdateAt);
                parameters.Add("@publishedAt", update.PublishedAt);
                parameters.Add("@startsAt", update.StartsAt);
                parameters.Add("@endsAt", update.EndsAt);
                parameters.Add("@content", update.Content);
                parameters.Add("@categoryId", update.CategoryId);
                parameters.Add("@photo", update.Photo);
                updateProduct.Product = await SqlMapper.QueryFirstOrDefaultAsync<Product>(
                                            cnn: connection,
                                            sql: "sp_UpdateProduct",
                                            param: parameters,
                                            commandType: CommandType.StoredProcedure
                                        );

                return updateProduct;
            }
            catch (Exception ex)
            {
                return new UpdateProductResult();
            };
        }
    }
}
