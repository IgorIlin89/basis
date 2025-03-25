using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Application.Commands.Coupon;
using OnlineShopWeb.Application.Commands.Transaction;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.ExtensionMethods;
using OnlineShopWeb.Models;
using OnlineShopWeb.Models.Mapping;
using System.Text.Json;

namespace OnlineShopWeb.Controllers;

public class ShoppingCartController(IGetCouponByCodeCommandHandler getCouponByCodeCommandHandler,
    IAddTransactionCommandHandler addTransactionCommandHandler,
    IAddTransactionMessagesCommandHandler addTransactionMessagesCommandHandler,
    IAddTransactionGrpcCommandHandler addTransactionGrpcCommandHandler)
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

        return Ok(new
        {
            isValid = true,
            amountOfDiscount = coupon.AmountOfDiscount,
            typeOfDiscount = coupon.TypeOfDiscount
        });
    }

    [HttpPost]
    public IActionResult CouponTableVC([FromBody] ShoppingCartModel shoppingCart)
    {
        return ViewComponent("CouponTable", shoppingCart);
    }

    [HttpGet]
    public async Task<ActionResult> BuyAllItemsInShoppingCartGrpc(
    CancellationToken cancellationToken)
    {
        //Convert to Domain object
        if (ModelState.IsValid)
        {
            var model = GetShoppingCart();

            if (model.ShoppingCartModelList.Count == 0)
            {
                ModelState.AddModelError("model", "You have selected no products to buy");
                return View("Views/ShoppingCart/Index.cshtml", model);
            }

            var command = new AddTransactionCommandGrpc(HttpContext.GetUserId().ToString(),
                model.ShoppingCartModelList.MapToDomainList(),
                model.CouponModelList.MapToDomainList()
                    );

            await addTransactionGrpcCommandHandler.Handle(command, cancellationToken);
        }

        HttpContext.AppendShoppingCart(new ShoppingCartModel());

        return RedirectToAction("Index", "Transaction");
    }

    [HttpGet]
    public async Task<ActionResult> BuyAllItemsInShoppingCartServiceBus(
        CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            var model = GetShoppingCart();

            if (model.ShoppingCartModelList.Count == 0)
            {
                ModelState.AddModelError("model", "You have selected no products to buy");
                return View("Views/ShoppingCart/Index.cshtml", model);
            }

            var commandToMessages = new AddTransactionCommandHttp(HttpContext.GetUserId().ToString(),
                    model.ShoppingCartModelList.MapToDomainList(),
                    model.CouponModelList.MapToDomainList());

            addTransactionMessagesCommandHandler.Handle(commandToMessages, cancellationToken);
        }

        HttpContext.AppendShoppingCart(new ShoppingCartModel());

        return RedirectToAction("Index", "Transaction");
    }

    [HttpGet]
    public async Task<ActionResult> BuyAllItemsInShoppingCartHttp(
        CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            var model = GetShoppingCart();

            if (model.ShoppingCartModelList.Count == 0)
            {
                ModelState.AddModelError("model", "You have selected no products to buy");
                return View("Views/ShoppingCart/Index.cshtml", model);
            }

            var commandToAdapter = new AddTransactionCommandHttp(HttpContext.GetUserId().ToString(),
                    model.ShoppingCartModelList.MapToDomainList(),
                    model.CouponModelList.MapToDomainList());

            await addTransactionCommandHandler.Handle(commandToAdapter, cancellationToken);
        }

        HttpContext.AppendShoppingCart(new ShoppingCartModel());

        return RedirectToAction("Index", "Transaction");

        //TODO
        //with transaction SEPA-Lastschrift
    }

    private ShoppingCartModel GetShoppingCart()
    {
        if (HttpContext.GetShoppingCart() is null)
        {
            var model = new ShoppingCartModel();
            HttpContext.AppendShoppingCart(model);
            return model;
        }
        else
        {
            return JsonSerializer.Deserialize<ShoppingCartModel>(HttpContext.GetShoppingCart());
        }
    }
}
