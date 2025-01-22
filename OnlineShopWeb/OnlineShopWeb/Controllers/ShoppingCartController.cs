using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Application.Commands.Coupon;
using OnlineShopWeb.Application.Commands.Transaction;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.ExtensionMethods;
using OnlineShopWeb.TransferObjects.Mapping;
using OnlineShopWeb.TransferObjects.Models;
using OnlineShopWeb.TransferObjects.Models.ListModels;
using System.Text.Json;

namespace OnlineShopWeb.Controllers;

public class ShoppingCartController(IGetCouponByCodeCommandHandler getCouponByCodeCommandHandler,
    IAddTransactionCommandHandler addTransactionCommandHandler,
    IAddTransactionMessagesCommandHandler addTransactionMessagesCommandHandler)
    : Controller
{
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
        var command = new GetCouponByCodeCommand(couponCode);
        var coupon = await getCouponByCodeCommandHandler.Handle(command);

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
            couponId = coupon.Id,
            amountOfDiscount = coupon.AmountOfDiscount,
            typeOfDiscount = coupon.TypeOfDiscount
        });
    }

    [HttpPost]
    public IActionResult CouponTableVC([FromBody] ShoppingCartListModel shoppingCart)
    {
        return ViewComponent("CouponTable", shoppingCart);
    }

    [HttpGet]
    public async Task<ActionResult> BuyAllItemsInShoppingCartServiceBus()
    {
        return await BuyAllItemsInShoppingCart(true);
    }

    [HttpGet]
    public async Task<ActionResult> BuyAllItemsInShoppingCartSynchronous()
    {
        return await BuyAllItemsInShoppingCart(false);
    }

    [HttpGet]
    public async Task<ActionResult> BuyAllItemsInShoppingCart(Boolean nServiceBus)
    {
        var model = GetShoppingCart();

        if (ModelState.IsValid)
        {
            if (model.ShoppingCartModelList.Count == 0)
            {
                ModelState.AddModelError("model", "You have selected no products to buy");
                return View("Views/ShoppingCart/Index.cshtml", model);
            }

            var command = new AddTransactionCommandHttp(HttpContext.User.Identity.Name,
                    model.ShoppingCartModelList.MapToDtoListAdapter(),
                    model.CouponModelList is null ? null : model.CouponModelList.MapToDtoList());

            if (nServiceBus)
            {
                var commandToNserviceBus = new OnlineShopWeb.Messages.V1.Events.AddTransactionEvent
                {
                    UserId = HttpContext.Name(),
                    PaymentDate = DateTimeOffset.UtcNow,
                    AddProductsInCartDto = model.ShoppingCartModelList.MapToServiceBusList(),
                    AddCouponsDto = model.CouponModelList is null ? null : model.CouponModelList.MapToServiceBusList()
                };

                var commandToMessages = new AddTransactionCommandMessages(HttpContext.Name().ToString(),
                    model.ShoppingCartModelList.MapToServiceBusList(),
                    model.CouponModelList is null ? null : model.CouponModelList.MapToServiceBusList());

                addTransactionMessagesCommandHandler.Handle(commandToMessages);
                //await _messageSession.Publish(commandToNserviceBus);
            }
            else
            {
                var commandToAdapter = new AddTransactionCommandHttp(HttpContext.User.Identity.Name,
                    model.ShoppingCartModelList.MapToDtoListAdapter(),
                    model.CouponModelList is null ? null : model.CouponModelList.MapToDtoList());

                await addTransactionCommandHandler.Handle(commandToAdapter);
            }

            //TODO
            //with transaction SEPA-Lastschrift

            HttpContext.AppendShoppingCart(new ShoppingCartListModel());

            return RedirectToAction("Index", "Transaction");
        }
        else
        {
            return RedirectToAction("Index", "Transaction");
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
