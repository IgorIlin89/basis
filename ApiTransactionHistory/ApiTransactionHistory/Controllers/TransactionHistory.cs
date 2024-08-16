using ApiTransactionHistory.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiTransactionHistory.Controllers;

public class TransactionHistory : ControllerBase
{
    [Route("transactionhistory/list/{id}")]
    [HttpGet]
    public async Task<ActionResult> GetTransactionHistoryList(int id)
    {
        return Ok();
    }

    [Route("transactionhistory")]
    [HttpPost]
    public async Task<ActionResult> BuyShoppingCartItems([FromBody] AddTransactionHistoryDto addTransactionHistoryDto)
    {
        return Ok();
    }
}
