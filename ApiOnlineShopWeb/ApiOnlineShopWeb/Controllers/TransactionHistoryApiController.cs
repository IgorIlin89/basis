using Microsoft.AspNetCore.Mvc;
using ApiOnlineShopWeb.Dtos;
using ApiOnlineShopWeb.Domain;
using ApiOnlineShopWeb.Database.Interfaces;
using System.Text.Json;

namespace ApiOnlineShopWeb.Controllers;

public class TransactionHistoryApiController(ITransactionHistoryRepository _transactionHistoryRepositry) : ControllerBase
{
    [Route("transactionhistorylist{id}")]
    [HttpGet]
    public async Task<ActionResult> GetTransactionHistoryList(int id)
    {
        var transactionHistoryList = _transactionHistoryRepositry.GetTransactionHistoryList(id);

        var transactionHistoryDtoList = new List<TransactionHistoryDto>();

        foreach (var element in transactionHistoryList)
        {
            new TransactionHistoryDto
            {
                Id = element.Id,
                UserId = element.UserId,
                User = element.User,
                PaymentDate = element.PaymentDate,
                FinalPrice = element.FinalPrice,
                Coupons = element.Coupons,
                ProductsInCart = element.ProductsInCart
            };
        }

        var response = JsonSerializer.Serialize(transactionHistoryDtoList);

        return Ok(response);


    }
}
