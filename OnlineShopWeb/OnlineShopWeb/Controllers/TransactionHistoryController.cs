using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;
using OnlineShopWeb.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineShopWeb.Controllers;

public class TransactionHistoryController : Controller
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _connectionString;
    private readonly string _connectToGetTransactionHistoryList;

    public TransactionHistoryController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ApiURL");
        _connectToGetTransactionHistoryList = configuration.
            GetConnectionString("ApiTransactionHistoryControllerGetTransactionHistoryList");
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var model = new TransactionHistoryListModel();

        var request = await _httpClient.GetAsync(_connectionString + _connectToGetTransactionHistoryList
            + Int32.Parse(HttpContext.User.Identity.Name));

        var response = await request.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };

        var transactionHistoryDtoList = JsonSerializer.Deserialize<List<TransactionHistoryDto>>(response, options);

        var transactionHistoryList = new List<TransactionHistory>();

        foreach (var element in transactionHistoryDtoList)
        {
            transactionHistoryList.Add(new TransactionHistory
            {
                Id = element.Id,
                UserId = element.UserId,
                User = element.User,
                PaymentDate = element.PaymentDate,
                FinalPrice = element.FinalPrice,
                Coupons = element.Coupons,
                ProductsInCart = element.ProductsInCart
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
