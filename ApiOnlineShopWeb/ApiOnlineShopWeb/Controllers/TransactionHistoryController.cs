using Microsoft.AspNetCore.Mvc;
using ApiOnlineShopWeb.Dtos;
using ApiOnlineShopWeb.Domain;
using ApiOnlineShopWeb.Database.Interfaces;
using ApiOnlineShopWeb.Dtos.Mapping;

namespace ApiOnlineShopWeb.Controllers;

public class TransactionHistoryController(ITransactionHistoryRepository _transactionHistoryRepositry
    , IUserRepository _userRepository
    , ICouponRepository _couponRepository
    , IProductRepository _productRepository) : ControllerBase
{
    [Route("transactionhistory/list/{id}")]
    [HttpGet]
    public async Task<ActionResult> GetTransactionHistoryList(int id)
    {
        var transactionHistoryList = _transactionHistoryRepositry.GetTransactionHistoryList(id);

        if (transactionHistoryList == null)
        {
            return NotFound();
        }

        var response = transactionHistoryList.Select(element =>
            new TransactionHistoryObjectsDto
            {
                UserId = element.UserId,
                User = element.User.MapToDto(),
                PaymentDate = element.PaymentDate,
                FinalPrice = element.FinalPrice,
                Coupons = element.Coupons.Select(o => o.MapToDto()).ToList(),
                //TODO ProductsInCart = element.ProductsInCart
            }
        );

        return Ok(response);
    }

    [Route("transactionhistory")]
    [HttpPost]
    public async Task<ActionResult> BuyShoppingCartItems([FromBody] TransactionHistoryDto transactionHistoryDto)
    {
        var user = _userRepository.GetUserById(transactionHistoryDto.UserId);
        decimal finalPrice = 0;

        var productsInCartList = new List<ProductInCart>();

        foreach (var element in transactionHistoryDto.ProductsInCartDto)
        {
            //var product = _productRepository.GetProductById(element.ProductId);

            productsInCartList.Add(new ProductInCart
            {
                Count = element.ProductCount,
                //Product = product,
                ProductId = element.ProductId//product.Id
            });
            //finalPrice += element.ProductCount * product.Price;
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
            //User = user,
            UserId = transactionHistoryDto.UserId,//user.Id,
            PaymentDate = DateTime.Now,
            FinalPrice = finalPrice,
            Coupons = couponList,
            ProductsInCart = productsInCartList
        };

        _transactionHistoryRepositry.BuyShoppingCartItems(transactionHistory);

        return Ok();
    }
}
