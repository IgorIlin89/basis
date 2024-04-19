using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;

namespace OnlineShopWeb.Controllers;

public class TransactionHistoryController : Controller
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICouponRepository _couponRepository;
    private readonly ITransactionHistoryRepository _transactionHistoryRepository;
    public TransactionHistoryController(IShoppingCartRepository shoppingCartRepository
        , IProductRepository productRepository
        , ICouponRepository couponRepository
        , ITransactionHistoryRepository transactionHistory)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _productRepository = productRepository;
        _couponRepository = couponRepository;
        _transactionHistoryRepository = transactionHistory;
    }

    [HttpGet]
    public IActionResult Index()
    {
        if (HttpContext.Session.GetInt32("UserId") is null)
        {
            return RedirectToAction("Index", "Product");
        }

        var model = new TransactionHistoryListModel();
        var list = _transactionHistoryRepository.GetTransactionHistoryList(HttpContext.Session.GetInt32("UserId").Value);

        foreach (var element in list)
        {
            model.TransactionHistoryModelList.Add(new TransactionHistoryModel
            {
                Id = element.Id,
                UserId = element.UserId,
                ProductId = element.ProductId,
                CouponId = element.CouponId,
                ProductName = _productRepository.GetProduct(element.ProductId).Name,
                PaymentDate = element.PaymentDate,
                
            });
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        _transactionHistoryRepository.DeleteTransactionFromHistory(id);
        return RedirectToAction("Index", "TransactionHistory");
    }
}
