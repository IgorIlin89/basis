using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Adapters.Interfaces;
using OnlineShopWeb.Domain;
using OnlineShopWeb.TransferObjects.Models;
using OnlineShopWeb.TransferObjects.Models.ListModels;

namespace OnlineShopWeb.Controllers;

public class TransactionHistoryController(ITransactionHistoryAdapter transactionHistoryAdapter) : Controller
{
    //public IHttpClientWrapper _httpClientWrapper;
    //private readonly HttpClient _httpClient = new HttpClient();

    //public TransactionHistoryController(IHttpClientWrapper clientWrapper)
    //{
    //    _httpClientWrapper = clientWrapper;
    //}

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var model = new TransactionHistoryListModel();

        var transactionHistoryDtoList = await transactionHistoryAdapter.GetTransactionHistoryList(
            HttpContext.User.Identity.Name);

        var transactionHistoryList = new List<TransactionHistory>();

        foreach (var element in transactionHistoryDtoList)
        {
            transactionHistoryList.Add(new TransactionHistory
            {
                Id = element.Id,
                UserId = element.UserId,
                //TODOUser = element.User,
                PaymentDate = element.PaymentDate,
                FinalPrice = element.FinalPrice,
                //TODOCoupons = element.Coupons,
                //TODOProductsInCart = element.ProductsInCart
            });
        }

        foreach (var element in transactionHistoryList)
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
}
