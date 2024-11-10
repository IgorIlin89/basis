﻿using Microsoft.AspNetCore.Mvc;
using NServiceBus;
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
    private readonly ITransactionAdapter _transactionHistoryAdapter;
    private readonly IMessageSession _messageSession;

    public ShoppingCartController(IHttpClientWrapper clientWrapper,
        IUserAdapter userAdapter, IProductCouponAdapter productCouponAdapter,
        ITransactionAdapter transactionHistoryAdapter,
        IMessageSession messageSession)
    {
        _httpClientWrapper = clientWrapper;
        _userAdapter = userAdapter;
        _productCouponAdapter = productCouponAdapter;
        _transactionHistoryAdapter = transactionHistoryAdapter;
        _messageSession = messageSession;
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
            couponId = couponDto.CouponId,
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

            var productsInCartList = new List<AddProductInCartDto>();
            var couponDtoList = new List<AddTransactionToCouponsDto>();
            var transactionDto = new AddTransactionDto();

            var serviceBusProductInCartDto = new List<OnlineShopWeb.Messages.V1.AddProductInCartDto>();
            var serviceBusCouponsDto = new List<OnlineShopWeb.Messages.V1.AddTransactionToCouponsDto>();
            var serviceBusTransactionDto = new OnlineShopWeb.Messages.V1.Events.AddTransactionEvent();

            CreateTransferObject(model, ref productsInCartList, ref couponDtoList, ref transactionDto,
                ref serviceBusProductInCartDto, ref serviceBusCouponsDto, ref serviceBusTransactionDto);

            var httpBody = new StringContent(
                    JsonSerializer.Serialize(transactionDto),
                    Encoding.UTF8,
                    Application.Json);

            if (nServiceBus)
            {
                await _messageSession.Publish(serviceBusTransactionDto);
            }
            else
            {
                await _transactionHistoryAdapter.AddTransaction(transactionDto);
            }



            // If this doesnt work, THAN make it event based
            // In Ui make 2 buttons, 1 is immedaite, one make it so he´waits 1 min 
            //with transaction SEPA-Lastschrift

            HttpContext.AppendShoppingCart(new ShoppingCartListModel());

            return RedirectToAction("Index", "TransactionHistory");
        }
        else
        {
            return RedirectToAction("Index", "TransactionHistory");
        }
    }

    private void CreateTransferObject(ShoppingCartListModel model, ref List<AddProductInCartDto> productsInCartList,
        ref List<AddTransactionToCouponsDto> couponDtoList,
        ref AddTransactionDto transactionDto,
        ref List<OnlineShopWeb.Messages.V1.AddProductInCartDto> serviceBusProductInCartDto,
        ref List<OnlineShopWeb.Messages.V1.AddTransactionToCouponsDto> serviceBusCouponsDto,
        ref OnlineShopWeb.Messages.V1.Events.AddTransactionEvent serviceBusTransactionDto)
    {


        foreach (var element in model.ShoppingCartModelList)
        {
            serviceBusProductInCartDto.Add(new OnlineShopWeb.Messages.V1.AddProductInCartDto
            {
                Count = element.Count,
                ProductId = element.ProductModelInCart.ProductId.Value,
                PricePerProduct = element.ProductModelInCart.Price,
            });
        }

        foreach (var element in model.ShoppingCartModelList)
        {
            productsInCartList.Add(new AddProductInCartDto
            {
                Count = element.Count,
                ProductId = element.ProductModelInCart.ProductId.Value,
                PricePerProduct = element.ProductModelInCart.Price,
            });
        }

        foreach (var element in model.CouponModelList)
        {
            couponDtoList.Add(new AddTransactionToCouponsDto
            {
                CouponId = element.CouponId.Value,
                Code = element.Code,
                AmountOfDiscount = element.AmountOfDiscount,
                TypeOfDiscountDto = (TypeOfDiscountDto)element.TypeOfDiscount
            });
        }

        foreach (var element in model.CouponModelList)
        {
            serviceBusCouponsDto.Add(new OnlineShopWeb.Messages.V1.AddTransactionToCouponsDto
            {
                CouponId = element.CouponId.Value,
                Code = element.Code,
                AmountOfDiscount = element.AmountOfDiscount,
                TypeOfDiscountDto = (OnlineShopWeb.Messages.V1.TypeOfDiscountDto)element.TypeOfDiscount
            });
        }

        transactionDto = new AddTransactionDto
        {
            UserId = HttpContext.Name(),
            AddProductsInCartDto = productsInCartList,
            AddCouponsDto = couponDtoList
        };

        //transactionDto.UserId = HttpContext.Name();
        //transactionDto.AddProductsInCartDto = serviceBusProductInCartDto;
        //transactionDto.AddCouponsDto = serviceBusCouponsDto;

        serviceBusTransactionDto.UserId = HttpContext.Name();
        serviceBusTransactionDto.AddProductsInCartDto = serviceBusProductInCartDto;
        serviceBusTransactionDto.AddCouponsDto = serviceBusCouponsDto;
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
