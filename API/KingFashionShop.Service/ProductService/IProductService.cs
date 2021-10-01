using KingFashion.Models.Products;
using KingFashionShop.Domain.Models;
using KingFashionShop.Domain.ProductRespones;
using KingFashionShop.Domain.Response.ProductRespones;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KingFashionShop.Service.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductRespone>> Get();
        Task<IEnumerable<ProductRespone>> Get(int catId);
        Task<CreateProductResult> Create(CreateProduct create);
        Task<Product> GetProduct(int proId);
        Task<IEnumerable<ProductResult>> GetProductsTopCategory(int topCategoryId, int limit, int boundary);
        Task<IEnumerable<ProductRespone>> GetAllProduct();
        Task<IEnumerable<ProductRespone>> GetProductById(int productId);
        Task<BoundaryList<Product>> GetProductByCategoryId(int categoryId, bool? isCategoryParent, int boundary, int limit);
        Task<UpdateProductResult> Update(UpdateProduct update);
        Task<ChangeShopResult> ChangeShop(ChangeShop changeShop);
    }
}
