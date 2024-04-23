using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Models;
using System.Text.Json;
using OnlineShopWeb.Domain;
using OnlineShopWeb.ExtensionMethods;

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
        var model = GetShoppingCart();
        return View(model);
    }

    [HttpGet]
    public IActionResult AddToShoppingCart(string productJson)
    {
        var productToShoppingCart = JsonSerializer.Deserialize<ProductModel>(productJson);

        if (ModelState.IsValid)
        {
            var model = GetShoppingCart();

            if (model.ShoppingCartModelList.FirstOrDefault(o => o.ProductModelInCart.ProductId == productToShoppingCart.ProductId) is not null)
            {
                model.ShoppingCartModelList.FirstOrDefault(o => o.ProductModelInCart.ProductId == productToShoppingCart.ProductId).count++;
            }
            else
            {
                model.ShoppingCartModelList.Add(new ProductInCart
                {
                    ProductModelInCart = productToShoppingCart,
                    count = 1
                });
            }

            HttpContext.AppendShoppingCart(model);
            return RedirectToAction("Index", "Product");
        }else{
            return RedirectToAction("Index", "Product");
        }
    }

    [HttpGet]
    public IActionResult DeleteFromShoppingCart(int productId)
    {
        var model = GetShoppingCart();
        //var productToDelete = JsonSerializer.Deserialize<ProductModel>(productId);


        if (ModelState.IsValid)
        {
            var productInShoppingCart = model.ShoppingCartModelList.FirstOrDefault(o => o.ProductModelInCart.ProductId == productId);
            productInShoppingCart.count--;

            if (productInShoppingCart.count == 0)
            {
                model.ShoppingCartModelList.Remove(productInShoppingCart);
            }

            HttpContext.AppendShoppingCart(model);
            return RedirectToAction("Index", "ShoppingCart");
        }else{
            return RedirectToAction("Index", "ShoppingCart");
        }
    }

    [HttpPost]
    public string JsAddCoupon(string couponCode)
    {
        var coupon = _couponRepository.GetCouponByCode("TestInRange");
        return JsonSerializer.Serialize(coupon);
    }

    [HttpPost]
    public IActionResult AddCoupon(string couponCode)
    {
        var model = GetShoppingCart();

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
            else if (model.CouponModelList.Where(o => o.CouponId == coupon.Id) is not null)
            {
                ModelState.AddModelError("model", "This coupon is allready in the cart");
                return View("Views/ShoppingCart/Index.cshtml", model);
            }
            else
            {
                model.CouponModelList.Add(new CouponModel
                {
                    CouponId = coupon.Id,
                    Code = coupon.Code,
                    AmountOfDiscount = coupon.AmountOfDiscount,
                    TypeOfDiscount = coupon.TypeOfDiscount,
                });

                coupon.MaxNumberOfUses--;
                _couponRepository.EditCoupon(coupon);

                HttpContext.AppendShoppingCart(model);
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
        var model = GetShoppingCart();

        if (ModelState.IsValid)
        {
            var coupon = _couponRepository.GetCouponById(couponId);

            //model.CouponModelList.Remove(couponId);

            coupon.MaxNumberOfUses++;
            _couponRepository.EditCoupon(coupon);

            HttpContext.AppendShoppingCart(model);
            return View("Views/ShoppingCart/Index.cshtml", model);
        }else
        {
            return View("Views/ShoppingCart/Index.cshtml", model);
        }

    }

    [HttpGet]
    public IActionResult BuyAllItemsInShoppingCart()
    {
        var model = GetShoppingCart();

        if (ModelState.IsValid)
        {
            if (model.ShoppingCartModelList.Count == 0)
            {
                ModelState.AddModelError("model", "You have selected no products to buy");
                return View("Views/ShoppingCart/Index.cshtml", model);
            }

            string couponIds = "";
            foreach (var element in model.CouponModelList)
            {
                couponIds += element.CouponId.ToString();
                couponIds += ";";
            }

            foreach (var element in model.ShoppingCartModelList)
            {
                for (; element.count != 0; element.count--)
                {
                    _shoppingCartRepository.BuyShoppingCartItem(new TransactionHistory
                    {
                        UserId = HttpContext.Name(),
                        ProductId = element.ProductModelInCart.ProductId.Value,
                        CouponIds = couponIds,
                        PaymentDate = DateTime.Now,
                    });
                }
            }

            HttpContext.AppendShoppingCart(new ShoppingCartListModel());
            return RedirectToAction("Index", "TransactionHistory");
        }else
        {
            return RedirectToAction("Index", "TransactionHistory");
        }
    }

    private ShoppingCartListModel GetShoppingCart()
    {
        return HttpContext.GetShoppingCart() is null ?
            new ShoppingCartListModel() :
            JsonSerializer.Deserialize<ShoppingCartListModel>(HttpContext.GetShoppingCart());
    }
}
