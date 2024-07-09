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

        var response = new List<TransactionHistoryObjectsDto>();

        foreach (var element in transactionHistoryList)
        {
            response.Add(new TransactionHistoryObjectsDto
            {
                UserId = element.UserId,
                User = element.User,
                PaymentDate = element.PaymentDate,
                FinalPrice = element.FinalPrice,
                Coupons = element.Coupons,
                ProductsInCart = element.ProductsInCart
            });
        }

        return Ok(response);
    }

    [Route("buyshoppingcartitems")]
    [HttpPost]
    public async Task<ActionResult> BuyShoppingCartItems([FromBody] TransactionHistoryDto transactionHistoryDto)
    {

        var user = _userRepository.GetUserById(transactionHistoryDto.UserId);
        decimal finalPrice = 0;

        List<ProductInCart> productsInCartList = new List<ProductInCart>();

        foreach (var element in transactionHistoryDto.ProductsInCartDto)
        {
            var product = _productRepository.GetProductById(element.ProductId);

            productsInCartList.Add(new ProductInCart
            {
                Count = element.ProductCount,
                Product = product,
                ProductId = product.Id
            });
            finalPrice += element.ProductCount * product.Price;
        }

        List<Coupon> couponList = new List<Coupon>();

        foreach (var element in transactionHistoryDto.CouponCodes)
        {
            var coupon = _couponRepository.GetCouponByCode(element);
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
            UserId = user.Id,
            PaymentDate = DateTime.Now,
            FinalPrice = finalPrice,
            Coupons = couponList,
            ProductsInCart = productsInCartList
        };

        _transactionHistoryRepositry.BuyShoppingCartItems(transactionHistory);

        return Ok();
    }
}
