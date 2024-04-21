using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Models;
using System.Text.Json;
using OnlineShopWeb.Domain;
using ExtensionMethods;

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
        var model = JsonSerializer.Deserialize<ShoppingCartDictionaryModel>(HttpContext.Request.Cookies["ShoppingCartDictionaryModel"]);
        return View(model);
    }

    // [FromBody] not working, why?
    [HttpGet]
    public IActionResult AddToShoppingCart(string product)
    {
        var productToShoppingCart = JsonSerializer.Deserialize<ProductModel>(product);
        var dictionary = JsonSerializer.Deserialize<ShoppingCartDictionaryModel>(HttpContext.Request.Cookies["ShoppingCartDictionaryModel"]);

        if (dictionary.ShoppingCartModelDictionary.ContainsKey(productToShoppingCart.ProductId.Value))
        {
            dictionary.ShoppingCartModelDictionary[productToShoppingCart.ProductId.Value].count++;
        }
        else
        {
            dictionary.ShoppingCartModelDictionary.Add(productToShoppingCart.ProductId.Value, new ShoppingCartModel
            {
                ProductInCart = productToShoppingCart,
                count = 1
            });
        }

        HttpContext.Response.Cookies.Append("ShoppingCartDictionaryModel", JsonSerializer.Serialize(dictionary));
        return RedirectToAction("Index", "Product");
    }

    [HttpGet]
    public IActionResult DeleteFromShoppingCart(int key)
    {
        var dictionary = JsonSerializer.Deserialize<ShoppingCartDictionaryModel>(HttpContext.Request.Cookies["ShoppingCartDictionaryModel"]);

        dictionary.ShoppingCartModelDictionary[key].count--;

        if (dictionary.ShoppingCartModelDictionary[key].count == 0)
        {
            dictionary.ShoppingCartModelDictionary.Remove(key);
        }

        HttpContext.Response.Cookies.Append("ShoppingCartDictionaryModel", JsonSerializer.Serialize(dictionary));
        return RedirectToAction("Index", "ShoppingCart");
    }

    [HttpPost]
    public IActionResult AddCoupon(string couponCode)
    {
        var model = JsonSerializer.Deserialize<ShoppingCartDictionaryModel>(HttpContext.Request.Cookies["ShoppingCartDictionaryModel"]);
        var coupon = _couponRepository.GetCouponByCode(couponCode);

        if (coupon == null)
        {
            ModelState.AddModelError("model", "The CouponCode does not exist");
            return View("Views/ShoppingCart/Index.cshtml", model);
        }

        if ((coupon.StartDate > DateTime.Now || coupon.EndDate < DateTime.Now))
        {
            ModelState.AddModelError("model", "The CouponCode is expired");
            return View("Views/ShoppingCart/Index.cshtml", model);
        }

        if (coupon.MaxNumberOfUses == 0)
        {
            ModelState.AddModelError("model", "All coupons with this code are allready taken");
            return View("Views/ShoppingCart/Index.cshtml", model);
        }

        if (model.CouponModelDictionary.ContainsKey(coupon.Id))
        {
            ModelState.AddModelError("model", "This coupon is allready in the cart");
            return View("Views/ShoppingCart/Index.cshtml", model);
        }
        else
        {
            model.CouponModelDictionary.Add(coupon.Id, new CouponModel
            {
                CouponId = coupon.Id,
                Code = coupon.Code,
                AmountOfDiscount = coupon.AmountOfDiscount,
                TypeOfDiscount = coupon.TypeOfDiscount,
            });

            coupon.MaxNumberOfUses--;
            _couponRepository.EditCoupon(coupon);

            HttpContext.Response.Cookies.Append("ShoppingCartDictionaryModel", JsonSerializer.Serialize(model));
            return View("Views/ShoppingCart/Index.cshtml", model);
        }
    }

    [HttpGet]
    public IActionResult DeleteCoupon(int couponId)
    {
        var model = JsonSerializer.Deserialize<ShoppingCartDictionaryModel>(HttpContext.Request.Cookies["ShoppingCartDictionaryModel"]);
        var coupon = _couponRepository.GetCouponById(couponId);

        model.CouponModelDictionary.Remove(couponId);

        coupon.MaxNumberOfUses++;
        _couponRepository.EditCoupon(coupon);

        HttpContext.Response.Cookies.Append("ShoppingCartDictionaryModel", JsonSerializer.Serialize(model));
        return View("Views/ShoppingCart/Index.cshtml", model);

    }

    [HttpGet]
    public IActionResult BuyAllItemsInShoppingCart()
    {
        var dictionary = JsonSerializer.Deserialize<ShoppingCartDictionaryModel>(HttpContext.Request.Cookies["ShoppingCartDictionaryModel"]);

        string couponIds = "";

        foreach(var element in dictionary.CouponModelDictionary)
        {
            couponIds += element.Value.CouponId.ToString();
            couponIds += ",";
        }

        foreach(var element in dictionary.ShoppingCartModelDictionary)
        {
            for (; element.Value.count != 0; element.Value.count--)
            {
                _shoppingCartRepository.BuyShoppingCartItem(new TransactionHistory
                {
                    UserId = HttpContext.Name(),
                    ProductId = element.Value.ProductInCart.ProductId.Value,
                    CouponIds = couponIds,
                    PaymentDate = DateTime.Now,
                });
            }
        }

        return RedirectToAction("Index", "Product");
    }


}
