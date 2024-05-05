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

    public void BuyShoppingCartItems(List<TransactionHistory> transactionHistoryList)
    {
        foreach (var element in transactionHistoryList)
        {
            _dbContext.TransactionHistory.Add(element);
        }
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
