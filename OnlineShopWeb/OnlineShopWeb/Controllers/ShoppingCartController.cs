using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Models;
using System.Text.Json;
using OnlineShopWeb.Domain;
using OnlineShopWeb.ExtensionMethods;

namespace OnlineShopWeb.Controllers;

public class ShoppingCartController : Controller
{
    private readonly ITransactionHistoryRepository _transactionHistoryRepository;
    private readonly ICouponRepository _couponRepository;
    private readonly IUserRepository _userRepository;
    public ShoppingCartController(ICouponRepository couponRepository
        , IUserRepository userRepository
        , ITransactionHistoryRepository transactionHistoryRepository)
    {
        _transactionHistoryRepository = transactionHistoryRepository;
        _couponRepository = couponRepository;
        _userRepository = userRepository;
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
                model.ShoppingCartModelList.FirstOrDefault(o => o.ProductModelInCart.ProductId == productToShoppingCart.ProductId).Count++;
            }
            else
            {
                model.ShoppingCartModelList.Add(new ProductInCartModel
                {
                    ProductModelInCart = productToShoppingCart,
                    Count = 1
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

    [HttpPost]
    public IActionResult DeleteFromShoppingCart([FromBody] int productId)
    {
        var model = GetShoppingCart();
        //var productToDelete = JsonSerializer.Deserialize<ProductModel>(productId);

        if (ModelState.IsValid)
        {
            var productInShoppingCart = model.ShoppingCartModelList.FirstOrDefault(o => o.ProductModelInCart.ProductId == productId);
            productInShoppingCart.Count--;

            if (productInShoppingCart.Count == 0)
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
        var user = _userRepository.GetUserById(HttpContext.Name());
        decimal finalPrice = 0;
        //decimal priceBeforeCoupons = 0;

        if (ModelState.IsValid)
        {
            if (model.ShoppingCartModelList.Count == 0)
            {
                ModelState.AddModelError("model", "You have selected no products to buy");
                return View("Views/ShoppingCart/Index.cshtml", model);
            }

            List<ProductInCart> productsInCartList = new List<ProductInCart>();

            foreach (var element in model.ShoppingCartModelList)
            {
                productsInCartList.Add(new ProductInCart
                {
                    Product = new Product
                    {
                        Name = element.ProductModelInCart.Name,
                        Producer = element.ProductModelInCart.Producer,
                        Category = element.ProductModelInCart.Category,
                        Picture = element.ProductModelInCart.Picture,
                        Price = element.ProductModelInCart.Price
                    },
                    Count = element.Count,
                    //ProductId = element.ProductModelInCart.ProductId.Value

                });
                finalPrice += element.Count * element.ProductModelInCart.Price;
            }

            List<Coupon> couponList = new List<Coupon>();

            foreach (var element in model.CouponModelList)
            {
                var coupon = _couponRepository.GetCouponByCode(element.Code);
                couponList.Add(coupon);

                if (coupon.TypeOfDiscount == TypeOfDiscount.Percentage)
                {
                    finalPrice *= ((100m - (decimal)coupon.AmountOfDiscount) / 100);
                }
                else if (coupon.TypeOfDiscount == TypeOfDiscount.Total)
                {
                    finalPrice -= (decimal)coupon.AmountOfDiscount;
                }
            }

            var transactionHistory = new TransactionHistory
            {
                User = user,
                PaymentDate = DateTime.Now,
                FinalPrice = finalPrice,
                ProductsInCart = productsInCartList,
                Coupons = couponList,
            };

            _transactionHistoryRepository.BuyShoppingCartItems(transactionHistory);

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
