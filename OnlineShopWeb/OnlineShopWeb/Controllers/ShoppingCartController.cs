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
        }
        else
        {
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
        }
        else
        {
            return RedirectToAction("Index", "ShoppingCart");
        }
    }

    [HttpPost]
    public IActionResult AddCoupon([FromBody] string couponCode)
    {
        var coupon = _couponRepository.GetCouponByCode(couponCode);

        if (coupon == null)
        {
            return Ok(new
            {
                isValid = false,
                validationError = "The CouponCode does not exist"
            });
        }
        else if ((coupon.StartDate > DateTime.Now || coupon.EndDate < DateTime.Now))
        {
            return Ok(new
            {
                isValid = false,
                validationError = "The CouponCode is expired"
            });
        }
        else if (coupon.MaxNumberOfUses == 0)
        {
            return Ok(new
            {
                isValid = false,
                validationError = "All coupons with this code are allready taken"
            });
        }

        return Ok(new
        {
            isValid = true,
            amountOfDiscount = coupon.AmountOfDiscount,
            typeOfDiscount = coupon.TypeOfDiscount
        });
    }

    [HttpPost]
    public IActionResult CouponTableVC([FromBody] ShoppingCartListModel shoppingCart)
    {
        //var model = JsonSerializer.Deserialize<ShoppingCartListModel>(shoppingCart);
        return ViewComponent("CouponTable", shoppingCart);
    }

    [HttpGet]
    public IActionResult BuyAllItemsInShoppingCart()
    {
        var model = GetShoppingCart();
        var listOfItemsToBuy = new List<TransactionHistory>();

        if (ModelState.IsValid)
        {
            if (model.ShoppingCartModelList.Count == 0)
            {
                ModelState.AddModelError("model", "You have selected no products to buy");
                return View("Views/ShoppingCart/Index.cshtml", model);
            }

            List<Coupon> couponList = new List<Coupon>();

            foreach (var element in model.CouponModelList)
            {
                var coupon = _couponRepository.GetCouponByCode(element.Code);
                couponList.Add(coupon);
            }

            foreach (var element in model.ShoppingCartModelList)
            {
                listOfItemsToBuy.Add(new TransactionHistory
                {
                    UserId = HttpContext.Name(),
                    ProductId = element.ProductModelInCart.ProductId.Value,
                    Coupons = couponList,
                    Count = element.count,
                    PaymentDate = DateTime.Now,
                });
            }

            _shoppingCartRepository.BuyShoppingCartItems(listOfItemsToBuy);

            HttpContext.AppendShoppingCart(new ShoppingCartListModel());
            return RedirectToAction("Index", "TransactionHistory");
        }
        else
        {
            return RedirectToAction("Index", "TransactionHistory");
        }
    }

    private ShoppingCartListModel GetShoppingCart()
    {
        if (HttpContext.GetShoppingCart() is null)
        {
            var model = new ShoppingCartListModel();
            HttpContext.AppendShoppingCart(model);
            return model;
        }
        else
        {
            return JsonSerializer.Deserialize<ShoppingCartListModel>(HttpContext.GetShoppingCart());
        }
    }
}
