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
    public class CartService : BaseService, ICartService
    {
        public ProductService.ProductService productService { get; set; }

        public CartItemService cartItemService { get; set; }
        public CartService(IConfiguration configuration) : base(configuration)
        {
            productService = new ProductService.ProductService(configuration);
            cartItemService = new CartItemService(configuration);
        }

        public async Task<CartResponse> GetBySessionId(string sessionId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@sessionId", sessionId);
            var cart = await SqlMapper.QueryFirstOrDefaultAsync<Cart>(
                cnn: connection,
                param: parameters,
                sql: "sp_GetCartBySessionId",
                commandType: CommandType.StoredProcedure
                );
            if (cart == null)
            {
                return null;
            }
            IEnumerable<CartItem> cartItems = await cartItemService.GetCartItemsByCartId(cart.Id);
            List<CartItemResponse> cartItemsList = new List<CartItemResponse>();
            foreach (var item in cartItems)
            {
                var product = await productService.GetProduct(item.ProductId);
                cartItemsList.Add(new CartItemResponse(item, product));
            }

            return new CartResponse(cart, cartItemsList);

        }

        public Task CheckOut(string sessionId)
        {
            return null;
        }

        public Task<CartResponse> UpdateCart(UpdateCart updateCart)
        {
            return null;
        }

        public async Task<CartResponse> AddCart(AddCart addCart)
        {
            var cart = await GetBySessionId(addCart.sessionId);
            Product product = await productService.GetProduct(addCart.productId);
            if (cart == null)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@sessionId", addCart.sessionId);
                parameters.Add("@token", Guid.NewGuid().ToString());
                parameters.Add("@status", CartStatus.New);
                parameters.Add("@firstName", null);
                parameters.Add("@middleName", null);
                parameters.Add("@lastName", null);
                parameters.Add("@mobile", null);
                parameters.Add("@email", null);
                parameters.Add("@line1", null);
                parameters.Add("@line2", null);
                parameters.Add("@city", null);
                parameters.Add("@province", null);
                parameters.Add("@country", null);
                var newCart = await SqlMapper.QueryFirstOrDefaultAsync<Cart>(
                       cnn: connection,
                       param: parameters,
                       sql: "sp_CreateCartBySessionId",
                       commandType: CommandType.StoredProcedure
                       );
                var newCartItem = new CartItem()
                {
                    Price = product.Price,
                    Discount = product.Discount,
                    CartId = newCart.Id,
                    CreatedAt = DateTime.Now,
                    ProductId = product.Id,
                    Quantity = 1
                };
                newCartItem = await cartItemService.CreateCartItem(newCartItem);
                CartItemResponse cartItemResult = new CartItemResponse(newCartItem,product);

                cart = new CartResponse(newCart, new List<CartItemResponse>() { cartItemResult });
            }
            else
            {
                var cartItems = await cartItemService.GetCartItemsByCartId(cart.Id);
                if (cartItems == null || cartItems.AsList().Count == 0)
                {
                    var newCartItem = new CartItem()
                    {
                        Price = product.Price,
                        Discount = product.Discount,
                        CartId = cart.Id,
                        CreatedAt = DateTime.Now,
                        ProductId = product.Id,
                        Quantity = 1
                    };
                    newCartItem = await cartItemService.CreateCartItem(newCartItem);

                    CartItemResponse cartItemResult = new CartItemResponse(newCartItem, product);
                    cart.Items = new List<CartItemResponse>() { cartItemResult };
                }
                else
                {
                  var  cartItemsResult  =new List<CartItemResponse>() ;
                    foreach (var item in cartItems)
                    {
                        if (item.ProductId == addCart.productId)
                        {
                           
                            item.Quantity += 1;
                            item.Price = product.Price * item.Quantity;
                            item.Discount = product.Discount * item.Quantity;
                            item.UpdatedAt = DateTime.Now;
                            var updateCartItem =   await cartItemService.UpdateCart(item);
                            cartItemsResult.Add(new CartItemResponse(updateCartItem,product));
                            break;
                        }
                    }
                    cart.Items = cartItemsResult;
                }

            }
            return cart;
        }

        public Task<CartResponse> CreateCart(UpdateCart updateCart)
        {
            throw new NotImplementedException();
        }
    }
}
