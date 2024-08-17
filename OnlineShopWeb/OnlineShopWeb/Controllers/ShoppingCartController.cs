using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Adapters.Interfaces;
using OnlineShopWeb.ExtensionMethods;
using OnlineShopWeb.Misc;
using OnlineShopWeb.TransferObjects.Dtos;
using OnlineShopWeb.TransferObjects.Models;
using OnlineShopWeb.TransferObjects.Models.ListModels;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineShopWeb.Controllers;

public class ShoppingCartController : Controller
{
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly IUserAdapter _userAdapter;
    private readonly IProductCouponAdapter _productCouponAdapter;
    private readonly ITransactionHistoryAdapter _transactionHistoryAdapter;

    public ShoppingCartController(IHttpClientWrapper clientWrapper,
        IUserAdapter userAdapter, IProductCouponAdapter productCouponAdapter,
        ITransactionHistoryAdapter transactionHistoryAdapter)
    {
        _httpClientWrapper = clientWrapper;
        _userAdapter = userAdapter;
        _productCouponAdapter = productCouponAdapter;
        _transactionHistoryAdapter = transactionHistoryAdapter;
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
    public async Task<ActionResult> AddCoupon([FromBody] string couponCode)
    {
        var couponDto = await _productCouponAdapter.GetCouponByCode(couponCode);

        if (couponDto == null)
        {
            return Ok(new
            {
                isValid = false,
                validationError = "The CouponCode does not exist"
            });
        }
        else if ((couponDto.StartDate > DateTime.Now || couponDto.EndDate < DateTime.Now))
        {
            return Ok(new
            {
                isValid = false,
                validationError = "The CouponCode is expired"
            });
        }
        else if (couponDto.MaxNumberOfUses == 0)
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
            amountOfDiscount = couponDto.AmountOfDiscount,
            typeOfDiscount = couponDto.TypeOfDiscount
        });
    }

    [HttpPost]
    public IActionResult CouponTableVC([FromBody] ShoppingCartListModel shoppingCart)
    {
        return ViewComponent("CouponTable", shoppingCart);
    }

    [HttpGet]
    public async Task<ActionResult> BuyAllItemsInShoppingCart()
    {
        var model = GetShoppingCart();

        if (ModelState.IsValid)
        {
            if (model.ShoppingCartModelList.Count == 0)
            {
                ModelState.AddModelError("model", "You have selected no products to buy");
                return View("Views/ShoppingCart/Index.cshtml", model);
            }

            List<ProductInCartDto> productsInCartList = new List<ProductInCartDto>();

            foreach (var element in model.ShoppingCartModelList)
            {
                productsInCartList.Add(new ProductInCartDto
                {
                    Count = element.Count,
                    ProductId = element.ProductModelInCart.ProductId.Value,
                    PricePerProduct = element.ProductModelInCart.Price,
                });
            }

            List<string> couponCodeList = new List<string>();

            foreach (var element in model.CouponModelList)
            {
                couponCodeList.Add(element.Code);
            }

            var transactionHistoryDto = new TransactionHistoryDto
            {
                UserId = HttpContext.Name(),
                PaymentDate = DateTime.Now,
                ProductsInCartDto = productsInCartList,
            };

            var httpBody = new StringContent(
                    JsonSerializer.Serialize(transactionHistoryDto),
                    Encoding.UTF8,
                    Application.Json);

            await _transactionHistoryAdapter.AddTransactionHistory(transactionHistoryDto);

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
