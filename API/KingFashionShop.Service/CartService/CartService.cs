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
            Product pAdd = await productService.GetProduct(addCart.productId);
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
                parameters.Add("@createdAt", DateTime.Now);

                var newCart = await SqlMapper.QueryFirstOrDefaultAsync<Cart>(
                       cnn: connection,
                       param: parameters,
                       sql: "sp_CreateCartBySessionId",
                       commandType: CommandType.StoredProcedure
                       );
                var newCartItem = new CartItem()
                {
                    Price = pAdd.Price,
                    Discount = pAdd.Discount,
                    CartId = newCart.Id,
                    CreatedAt = DateTime.Now,
                    ProductId = pAdd.Id,
                    Quantity = addCart.quantity
                };
                newCartItem = await cartItemService.CreateCartItem(newCartItem);
                CartItemResponse cartItemResult = new CartItemResponse(newCartItem, pAdd);

                cart = new CartResponse(newCart, new List<CartItemResponse>() { cartItemResult });
            }
            else
            {
                var cartItems = await cartItemService.GetCartItemsByCartId(cart.Id);
                if (cartItems == null || cartItems.AsList().Count == 0)
                {
                    var newCartItem = new CartItem()
                    {
                        Price = pAdd.Price,
                        Discount = pAdd.Discount,
                        CartId = cart.Id,
                        CreatedAt = DateTime.Now,
                        ProductId = pAdd.Id,
                        Quantity = addCart.quantity
                    };
                    newCartItem = await cartItemService.CreateCartItem(newCartItem);

                    CartItemResponse cartItemResult = new CartItemResponse(newCartItem, pAdd);
                    cart.Items = new List<CartItemResponse>() { cartItemResult };
                }
                else
                {
                    var cartItemsResult = new List<CartItemResponse>();
                    var pFound = false;
                    foreach (var item in cartItems)
                    {
                        if (item.ProductId == addCart.productId && item.CartId == cart.Id)
                        {
                            pFound = true;
                            item.Quantity += addCart.quantity;
                            item.Price = pAdd.Price;
                            item.Discount = pAdd.Discount;
                            item.UpdatedAt = DateTime.Now;
                            var updateCartItem = await cartItemService.UpdateCartItem(item);
                            cartItemsResult.Add(new CartItemResponse(updateCartItem, pAdd));
                        }
                        else
                        {
                           var p = await productService.GetProduct(item.ProductId);
                            cartItemsResult.Add(new CartItemResponse(item, p));
                        }
                    }
                    if (!pFound)
                    {
                        var newCartItem = new CartItem()
                        {
                            Price = pAdd.Price,
                            Discount = pAdd.Discount,
                            CartId = cart.Id,
                            CreatedAt = DateTime.Now,
                            ProductId = pAdd.Id,
                            Quantity = addCart.quantity
                        };
                        newCartItem = await cartItemService.CreateCartItem(newCartItem);
                        cartItemsResult.Add(new CartItemResponse(newCartItem, pAdd));

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

        public async Task<CartResponse> ChangeItem(ChangeCart changeCart)
        {
            var cart = await GetBySessionId(changeCart.sessionId);
            if (cart == null)
                return null;
            var cartItems = await cartItemService.GetCartItemsByCartId(cart.Id);
            if (cartItems == null && cartItems.AsList().Count == 0)
                return null;
            Product product = await productService.GetProduct(changeCart.productId);
            if (product == null)
                return null;
            var cartItemsResult = new List<CartItemResponse>();
            foreach (var item in cartItems)
            {
                if (item.ProductId == changeCart.productId && item.CartId == cart.Id)
                {
                    item.Quantity = changeCart.quantity;
                    item.Price = product.Price;
                    item.Discount = product.Discount;
                    item.UpdatedAt = DateTime.Now;
                    var updateCartItem = await cartItemService.UpdateCartItem(item);
                    cartItemsResult.Add(new CartItemResponse(updateCartItem, product));
                }
                else { 
                    var p = await productService.GetProduct(item.ProductId);
                cartItemsResult.Add(new CartItemResponse(item, p));
                }
            }
            cart.Items = cartItemsResult;
            return cart;
        }

        public async Task<CartResponse> Remove(RemoveCart removeCart)
        {
            var cart = await GetBySessionId(removeCart.sessionId);
            if (cart == null)
                return null;
            await cartItemService.RemoveCartItem(removeCart.productId, cart.Id);
            var cartItems = await cartItemService.GetCartItemsByCartId(cart.Id);
            var cartItemsResult = new List<CartItemResponse>();
            if (cartItems != null)
                foreach (var item in cartItems)
                {
                    Product product = await productService.GetProduct(item.ProductId);
                    cartItemsResult.Add(new CartItemResponse(item, product));
                }
            cart.Items = cartItemsResult;
            return cart;
        }
    }
}
