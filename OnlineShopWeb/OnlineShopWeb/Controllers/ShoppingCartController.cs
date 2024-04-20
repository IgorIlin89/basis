using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Html;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Controllers;

public class ShoppingCartController : Controller
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICouponRepository _couponRepository;
    public ShoppingCartController(IShoppingCartRepository shoppingCartRepository
        , IProductRepository productRepository
        , ICouponRepository couponRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _productRepository = productRepository;
        _couponRepository = couponRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var model = new ShoppingCartListModel();
        var list = _shoppingCartRepository.GetProductsInCartList(Int32.Parse(HttpContext.User.Identity.Name));

        foreach (var element in list)
        {
            model.ShoppingCartModelList.Add(new ShoppingCartModel
            {
                Id = element.Id,
                UserId = element.UserId,
                ProductId = element.ProductId,
                CouponId = element.CouponId,
                ProductName = _productRepository.GetProduct(element.ProductId).Name
            });
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult AddToShoppingCart(int productId)
    {
        _shoppingCartRepository.AddProductToCart(productId, Int32.Parse(HttpContext.User.Identity.Name), 1);
        return RedirectToAction("Index", "Product");
    }

    [HttpGet]
    public IActionResult CookieAddToShoppingCart(int productId)
    {
        HttpContext.Response.Cookies.Append(Guid.NewGuid().ToString(), productId.ToString());
        return RedirectToAction("Index", "Product");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        _shoppingCartRepository.DeleteProductFromCart(id);
        return RedirectToAction("Index", "ShoppingCart");
    }

    [HttpGet]
    public IActionResult CookieDelete(string key)
    {
        HttpContext.Response.Cookies.Delete(key);
        return RedirectToAction("Index", "ShoppingCart");
    }

    [HttpPost]
    public IActionResult BuyAllItemsInShoppingCart(string jsonShoppingCartModelList, string? couponCode)
    {
        //var shoppingCartModelList = JsonSerializer.Deserialize(jsonShoppingCartModelList, List<ShoppingCartModel>);
        var coupon = _couponRepository.GetCouponByCode(couponCode);

        int? couponId = coupon is null ? null : coupon.Id;

        //List<ShoppingCartModel> shoppingCartModelList = JsonSerializer.Deserialize<List<ShoppingCartModel>>(jsonShoppingCartModelList);
        var shoppingCartModelList = _shoppingCartRepository.GetProductsInCartList(Int32.Parse(HttpContext.User.Identity.Name));


        foreach (var element in shoppingCartModelList)
        {
            _shoppingCartRepository.BuyShoppingCartItem(element.Id, couponId);
        }


        return RedirectToAction("Index", "Product");
    }


    [HttpPost]
    public IActionResult CookieBuyAllItemsInShoppingCart(string? couponCode)
    {
        var coupon = _couponRepository.GetCouponByCode(couponCode);
        int? couponId = coupon is null ? null : coupon.Id;

        foreach (var element in HttpContext.Request.Cookies)
        {
            int number;
            if (!Int32.TryParse(element.Value, out number))
            {
                continue;
            }
            var transactionHistory = new TransactionHistory
            {
                UserId = Int32.Parse(HttpContext.User.Identity.Name),
                ProductId = Int32.Parse(element.Value),
                CouponId = couponId,
                PaymentDate = DateTime.Now,
            };

            _shoppingCartRepository.CookieBuyShoppingCartItem(transactionHistory);
            HttpContext.Response.Cookies.Delete(element.Key);
        }

        return RedirectToAction("Index", "Product");
    }


}
