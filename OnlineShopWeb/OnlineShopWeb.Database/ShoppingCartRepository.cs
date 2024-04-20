using Microsoft.EntityFrameworkCore;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Database;

internal class ShoppingCartRepository : IShoppingCartRepository
{
    private OnlineShopWebDbContext _dbContext;

    public ShoppingCartRepository(OnlineShopWebDbContext onlineShopWebDbContext)
    {
        _dbContext = onlineShopWebDbContext;
    }

    public void AddProductToCart(int productId, int userId, int couponId)
    {
        _dbContext.ShoppingCart.Add(new ShoppingCart
        {
            ProductId = productId,
            UserId = userId,
            CouponId = couponId
        });
        _dbContext.SaveChanges();
    }

    public void DeleteProductFromCart(int shoppingCartId)
    {
        var shoppingCartItem = GetShoppingCartItemById(shoppingCartId);
        _dbContext.ShoppingCart.Remove(shoppingCartItem);
        _dbContext.SaveChanges();
    }

    public void BuyShoppingCartItem(int shoppingCartId, int? couponId)
    {
        var shoppingCartItem = GetShoppingCartItemById(shoppingCartId);
        _dbContext.ShoppingCart.Remove(shoppingCartItem);
        _dbContext.TransactionHistory.Add(new TransactionHistory
        {
            UserId = shoppingCartItem.UserId,
            ProductId = shoppingCartItem.ProductId,
            CouponId = couponId,
            PaymentDate = DateTime.Now
        });
        _dbContext.SaveChanges();
    }

    public void CookieBuyShoppingCartItem(TransactionHistory transactionHistory)
    {
        _dbContext.TransactionHistory.Add(transactionHistory);
        _dbContext.SaveChanges();
    }

    public List<ShoppingCart> GetProductsInCartList(int userId)
    {
        return _dbContext.ShoppingCart.Where(o => o.UserId == userId).ToList();
    }

    public ShoppingCart? GetShoppingCartItemById(int id)
    {
        return _dbContext.ShoppingCart.FirstOrDefault(o => o.Id == id);
    }


}
