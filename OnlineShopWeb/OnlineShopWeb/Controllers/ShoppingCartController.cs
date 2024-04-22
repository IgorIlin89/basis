﻿using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Models;
using System.Text.Json;
using OnlineShopWeb.Domain;
using ExtensionMethods;

namespace OnlineShopWeb.Controllers;

public class ShoppingCartController : Controller
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly ICouponRepository _couponRepository;
    public ShoppingCartController(IShoppingCartRepository shoppingCartRepository
        , ICouponRepository couponRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
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
        var model = JsonSerializer.Deserialize<ShoppingCartDictionaryModel>(HttpContext.Request.Cookies["ShoppingCartDictionaryModel"]);

        if (ModelState.IsValid)
        {

            if (model.ShoppingCartModelDictionary.ContainsKey(productToShoppingCart.ProductId.Value))
            {
                model.ShoppingCartModelDictionary[productToShoppingCart.ProductId.Value].count++;
            }
            else
            {
                model.ShoppingCartModelDictionary.Add(productToShoppingCart.ProductId.Value, new ShoppingCartModel
                {
                    ProductInCart = productToShoppingCart,
                    count = 1
                });
            }

            HttpContext.Response.Cookies.Append("ShoppingCartDictionaryModel", JsonSerializer.Serialize(model));
            return RedirectToAction("Index", "Product");
        }else{
            return RedirectToAction("Index", "Product");
        }
    }

    [HttpGet]
    public IActionResult DeleteFromShoppingCart(int key)
    {
        var model = JsonSerializer.Deserialize<ShoppingCartDictionaryModel>(HttpContext.Request.Cookies["ShoppingCartDictionaryModel"]);

        if (ModelState.IsValid)
        {
            model.ShoppingCartModelDictionary[key].count--;

            if (model.ShoppingCartModelDictionary[key].count == 0)
            {
                model.ShoppingCartModelDictionary.Remove(key);
            }

            HttpContext.Response.Cookies.Append("ShoppingCartDictionaryModel", JsonSerializer.Serialize(model));
            return RedirectToAction("Index", "ShoppingCart");
        }else{
            return RedirectToAction("Index", "ShoppingCart");
        }
    }

    [HttpPost]
    public IActionResult AddCoupon(string couponCode)
    {
        var model = JsonSerializer.Deserialize<ShoppingCartDictionaryModel>(HttpContext.Request.Cookies["ShoppingCartDictionaryModel"]);


        if (ModelState.IsValid)
        {
            var coupon = _couponRepository.GetCouponByCode(couponCode);

            if (coupon == null)
            {
                ModelState.AddModelError("model", "The CouponCode does not exist");
                return View("Views/ShoppingCart/Index.cshtml", model);
            }
            else if ((coupon.StartDate > DateTime.Now || coupon.EndDate < DateTime.Now))
            {
                ModelState.AddModelError("model", "The CouponCode is expired");
                return View("Views/ShoppingCart/Index.cshtml", model);
            }
            else if (coupon.MaxNumberOfUses == 0)
            {
                ModelState.AddModelError("model", "All coupons with this code are allready taken");
                return View("Views/ShoppingCart/Index.cshtml", model);
            }
            else if (model.CouponModelDictionary.ContainsKey(coupon.Id))
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
        }else
        {
            return View("Views/ShoppingCart/Index.cshtml", model);
        }
    }

    [HttpGet]
    public IActionResult DeleteCoupon(int couponId)
    {
        var model = JsonSerializer.Deserialize<ShoppingCartDictionaryModel>(HttpContext.Request.Cookies["ShoppingCartDictionaryModel"]);

        if (ModelState.IsValid)
        {
            var coupon = _couponRepository.GetCouponById(couponId);

            model.CouponModelDictionary.Remove(couponId);

            coupon.MaxNumberOfUses++;
            _couponRepository.EditCoupon(coupon);

            HttpContext.Response.Cookies.Append("ShoppingCartDictionaryModel", JsonSerializer.Serialize(model));
            return View("Views/ShoppingCart/Index.cshtml", model);
        }else
        {
            return View("Views/ShoppingCart/Index.cshtml", model);
        }

    }

    [HttpGet]
    public IActionResult BuyAllItemsInShoppingCart()
    {
        var model = JsonSerializer.Deserialize<ShoppingCartDictionaryModel>(HttpContext.Request.Cookies["ShoppingCartDictionaryModel"]);

        if (ModelState.IsValid)
        {

            if (model.ShoppingCartModelDictionary.Count == 0)
            {
                ModelState.AddModelError("model", "You have selected no products to buy");
                return View("Views/ShoppingCart/Index.cshtml", model);
            }

            string couponIds = "";
            foreach (var element in model.CouponModelDictionary)
            {
                couponIds += element.Value.CouponId.ToString();
                couponIds += ",";
            }

            foreach (var element in model.ShoppingCartModelDictionary)
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

            HttpContext.Response.Cookies.Append("ShoppingCartDictionaryModel", JsonSerializer.Serialize(new ShoppingCartDictionaryModel()));
            return RedirectToAction("Index", "TransactionHistory");
        }else
        {
            return RedirectToAction("Index", "TransactionHistory");
        }
    }


}
