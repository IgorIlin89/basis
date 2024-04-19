using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Html;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        if (HttpContext.Session.GetInt32("UserId") is null)
        {
            return RedirectToAction("Index", "Product");
        }

        var model = new ShoppingCartListModel();
        var list = _shoppingCartRepository.GetProductsInCartList(HttpContext.Session.GetInt32("UserId").Value);

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
    public IActionResult AddToShoppingCart(int productId, int userId)
    {
        _shoppingCartRepository.AddProductToCart(productId, userId, 1);
        return RedirectToAction("Index", "Product");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        _shoppingCartRepository.DeleteProductFromCart(id);
        return RedirectToAction("Index", "ShoppingCart");
    }

    [HttpPost]
    public IActionResult BuyAllItemsInShoppingCart(string jsonShoppingCartModelList, string? couponCode)
    {
        //var shoppingCartModelList = JsonSerializer.Deserialize(jsonShoppingCartModelList, List<ShoppingCartModel>);
        var coupon = _couponRepository.GetCouponByCode(couponCode);

        int? couponId = coupon is null ? null : coupon.Id;
        
        //List<ShoppingCartModel> shoppingCartModelList = JsonSerializer.Deserialize<List<ShoppingCartModel>>(jsonShoppingCartModelList);
        var shoppingCartModelList = _shoppingCartRepository.GetProductsInCartList(HttpContext.Session.GetInt32("UserId").Value);



        foreach (var element in shoppingCartModelList)
        {
            _shoppingCartRepository.BuyShoppingCartItem(element.Id, couponId);
        }


        return RedirectToAction("Index", "Product");
    }


}
