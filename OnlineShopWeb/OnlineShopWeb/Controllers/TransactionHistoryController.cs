using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;

namespace OnlineShopWeb.Controllers;

public class TransactionHistoryController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ICouponRepository _couponRepository;
    private readonly ITransactionHistoryRepository _transactionHistoryRepository;
    public TransactionHistoryController(IProductRepository productRepository
        , ICouponRepository couponRepository
        , ITransactionHistoryRepository transactionHistory)
    {
        _productRepository = productRepository;
        _couponRepository = couponRepository;
        _transactionHistoryRepository = transactionHistory;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var model = new TransactionHistoryListModel();
        var list = _transactionHistoryRepository.GetTransactionHistoryList(Int32.Parse(HttpContext.User.Identity.Name));

        foreach (var element in list)
        {
            var couponIds = "";
            foreach (var coupon in element.Coupons)
            {
                couponIds += coupon.Id;
                couponIds += ";";
            }

            var productNames = "";
            foreach (var product in element.ProductsInCart)
            {
                productNames += product.Product.Name;
            }

            model.TransactionHistoryModelList.Add(new TransactionHistoryModel
            {
                Id = element.Id,
                UserId = element.UserId,
                UserName = element.User.GivenName + element.User.Surname,
                ProductNames = productNames,
                CouponIds = couponIds,
                PaymentDate = element.PaymentDate,
                FinalPrice = element.FinalPrice
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
