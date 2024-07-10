using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Models;
using System.Text.Json;
using OnlineShopWeb.Domain;
using OnlineShopWeb.ExtensionMethods;
using OnlineShopWeb.Dtos;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineShopWeb.Controllers;

public class ShoppingCartController : Controller
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _connectionString;
    public readonly string _connectToGetCouponByCode;
    private readonly string _connectToGetUserById;
    private readonly string _connectToBuyShoppingCartItems;

    public ShoppingCartController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ApiClientOptions");
        _connectToGetCouponByCode = configuration.GetConnectionString("ApiCouponControllerGetCouponByCode");
        _connectToGetUserById = configuration.GetConnectionString("ApiUserControllerGetUserById");
        _connectToBuyShoppingCartItems = configuration.GetConnectionString("ApiTransactionHistoryControllerBuyShoppingCartItems");
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
    public async Task<ActionResult> AddCoupon([FromBody] string couponCode)
    {
        var request = await _httpClient.GetAsync(_connectionString + _connectToGetCouponByCode + couponCode);
        var response = await request.Content.ReadAsStringAsync();

        var couponDto = JsonSerializer.Deserialize<CouponDto>(response);

        //var coupon = _couponRepository.GetCouponByCode(couponCode);

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
                    ProductCount = element.Count,
                    ProductId = element.ProductModelInCart.ProductId.Value
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
                ProductsInCartDto = productsInCartList,
                CouponCodes = couponCodeList,
            };

            var httpBody = new StringContent(
                    JsonSerializer.Serialize(transactionHistoryDto),
                    Encoding.UTF8,
                    Application.Json);

            var requestToBuyItems = _httpClient.PostAsync(_connectionString + _connectToBuyShoppingCartItems, httpBody);

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
