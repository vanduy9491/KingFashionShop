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

        public async Task<Cart> GetBySessionId(string sessionId)
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
                return null;
            List<CartItem> cartItems = (await cartItemService.GetCartItemsByCartId(cart.Id)).AsList();
            foreach (var cartItem in cartItems)
            {
                cartItem.Product = await productService.GetProduct(cartItem.ProductId);
            }
            cart.CartItems = cartItems;
            return cart;

        }

        public async Task<Cart> AddCart(AddCart addCart)
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
                newCartItem.Product = pAdd;
                newCart.CartItems = new List<CartItem>() { newCartItem };
                cart = newCart;
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
                    newCartItem.Product = pAdd;
                    cart.CartItems = new List<CartItem>() { newCartItem };
                }
                else
                {
                    var newCartItems = new List<CartItem>();
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
                            updateCartItem.Product = pAdd;
                            newCartItems.Add(updateCartItem);
                        }
                        else
                        {
                            var p = await productService.GetProduct(item.ProductId);
                            item.Product = p;
                            newCartItems.Add(item);
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
                        newCartItem.Product = pAdd;
                        newCartItems.Add(newCartItem);

                    }
                    cart.CartItems = newCartItems;
                }

            }
            return cart;
        }

        public async Task<Cart> ChangeItem(ChangeCart changeCart)
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
            var newCartItems = new List<CartItem>();
            foreach (var item in cartItems)
            {
                if (item.ProductId == changeCart.productId && item.CartId == cart.Id)
                {
                    item.Quantity = changeCart.quantity;
                    item.Price = product.Price;
                    item.Discount = product.Discount;
                    item.UpdatedAt = DateTime.Now;
                    var cartItem = await cartItemService.UpdateCartItem(item);
                    cartItem.Product = product;
                    newCartItems.Add(cartItem);
                }
                else
                {
                    var p = await productService.GetProduct(item.ProductId);
                    item.Product = p;
                    newCartItems.Add(item);
                }
            }
            cart.CartItems = newCartItems;
            return cart;
        }

        public async Task<Cart> Remove(RemoveCart removeCart)
        {
            var cart = await GetBySessionId(removeCart.sessionId);
            if (cart == null)
                return null;
            await cartItemService.RemoveCartItem(removeCart.productId, cart.Id);
            var cartItems = await cartItemService.GetCartItemsByCartId(cart.Id);
            var newCartItems = new List<CartItem>();
            if (cartItems != null)
                foreach (var item in cartItems)
                {
                    Product product = await productService.GetProduct(item.ProductId);
                    item.Product = product;
                    newCartItems.Add(item);
                }
            cart.CartItems = cartItems;
            return cart;
        }

        public async Task<Cart> UpdateCart(Cart cart)
        {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", cart.Id);
                parameters.Add("@userId", cart.UserId);
                parameters.Add("@token", cart.Token);
                parameters.Add("@status", cart.Status);
                parameters.Add("@firstName", cart.FirstName);
                parameters.Add("@middleName", cart.MiddleName);
                parameters.Add("@lastName", cart.LastName);
                parameters.Add("@mobile", cart.Mobile);
                parameters.Add("@email", cart.Email);
                parameters.Add("@line1", cart.Line1);
                parameters.Add("@line2", cart.Line2);
                parameters.Add("@city", cart.City);
                parameters.Add("@province", cart.Province);
                parameters.Add("@country", cart.Country);

                
              

            return await SqlMapper.QueryFirstOrDefaultAsync<Domain.Models.Cart>(
                cnn: connection,
                param: parameters,
                sql: "sp_UpdateCartBySessionId",
                commandType: CommandType.StoredProcedure
                );
        }
    }
}
