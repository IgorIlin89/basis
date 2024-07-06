using Microsoft.AspNetCore.Mvc;
using ApiOnlineShopWeb.Dtos;
using ApiOnlineShopWeb.Domain;
using ApiOnlineShopWeb.Database.Interfaces;
using System.Text.Json;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace ApiOnlineShopWeb.Controllers;

public class TransactionHistoryApiController(ITransactionHistoryRepository _transactionHistoryRepositry
    , IUserRepository _userRepository
    , ICouponRepository _couponRepository
    , IProductRepository _productRepository) : ControllerBase
{
    [Route("transactionhistorylist{id}")]
    [HttpGet]
    public async Task<ActionResult> GetTransactionHistoryList(int id)
    {
        var transactionHistoryList = _transactionHistoryRepositry.GetTransactionHistoryList(id);

        var transactionHistoryDtoList = new List<TransactionHistoryDto>();

        foreach (var element in transactionHistoryList)
        {
            transactionHistoryDtoList.Add(new TransactionHistoryDto
            {
                UserId = element.UserId,
                User = element.User,
                PaymentDate = element.PaymentDate,
                FinalPrice = element.FinalPrice,
                Coupons = element.Coupons,
                ProductsInCart = element.ProductsInCart
            });
        }

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };

        var response = JsonSerializer.Serialize(transactionHistoryDtoList, options);

        return Ok(response);
    }

    [Route("buyshoppingcartitems")]
    [HttpPost]
    public async Task<ActionResult> BuyShoppingCartItems([FromBody] TransactionHistoryDto transactionHistoryDto)
    {

        var user = _userRepository.GetUserById(transactionHistoryDto.UserId);
        List<Coupon> couponList = new List<Coupon>();

        foreach (var element in transactionHistoryDto.Coupons)
        {
            var coupon = _couponRepository.GetCouponByCode(element.Code);
            couponList.Add(coupon);
        }

        List<ProductInCart> productsInCartList = new List<ProductInCart>();

        foreach (var element in transactionHistoryDto.ProductsInCart)
        {
            var product = _productRepository.GetProductById(element.ProductId);

            productsInCartList.Add(new ProductInCart
            {
                Count = element.Count,
                Product = product,
                ProductId = product.Id
            });
        }

        var transactionHistory = new TransactionHistory
        {
            User = user,
            UserId = user.Id,
            PaymentDate = transactionHistoryDto.PaymentDate,
            FinalPrice = transactionHistoryDto.FinalPrice,
            Coupons = couponList,
            ProductsInCart = productsInCartList
        };

        _transactionHistoryRepositry.BuyShoppingCartItems(transactionHistory);

        return Ok();
    }
}
