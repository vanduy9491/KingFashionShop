using KingFashionShop.Domain.Models;
using KingFashionShop.Domain.Response.ProductRespones;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KingFashionShop.Service.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductRespone>> Get(int catId);
        Task<CreateProductResult> Create(CreateProduct create);
        Task<Product> GetProduct(int proId);

    }
}
