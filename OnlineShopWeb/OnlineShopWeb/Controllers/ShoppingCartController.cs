using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Models;
using System.Text.Json;
using OnlineShopWeb.Domain;
using ExtensionMethods;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

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
        //var list = _shoppingCartRepository.GetProductsInCartList(HttpContext.Name());

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

        // It would be great to put the string of the name into a variable

        // I set the Cookie at Sign in now, no need to check anymore if it allready exists.
        //var dictionary = (HttpContext.Request.Cookies["ShoppingCartDictionaryModel"] is null) ? 
        //    new ShoppingCartDictionaryModel() : 
        //    JsonSerializer.Deserialize<ShoppingCartDictionaryModel>(HttpContext.Request.Cookies["ShoppingCartDictionaryModel"]);


        //HttpContext.Response.Cookies.Delete("ShoppingCartDictionaryModel"); seems not necessary, watched in debugger
        //didnt work with a list ==> c# cannot convert lambda expression to type because it is not a delegate type
        //if(shoppingCartList.ShoppingCartModelList.Contains( o => o.ProductInCart.Id == productToShoppingCart.Id))
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

        //dictionary.ShoppingCartModelDictionary = dictionary.ShoppingCartModelDictionary[key].count-- == 0 ?
        //dictionary.ShoppingCartModelDictionary.Remove(key) :
        //dictionary.ShoppingCartModelDictionary[key].count--;
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
