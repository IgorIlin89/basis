using ApiTransactionHistory.Application.Commands;
using ApiTransactionHistory.Application.Handlers;
using ApiTransactionHistory.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiTransactionHistory.Controllers;

public class TransactionHistory(
    IGetTransactionHistoryListCommandHandler getTransactionHistoryListCommandHandler,
    IAddTransactionHistoryCommandHandler addTransactionHistoryCommandHandler) : ControllerBase
{
    [Route("transactionhistory/list/{id}")]
    [HttpGet]
    public async Task<ActionResult> GetTransactionHistoryList(int id)
    {
        var command = new GetTransactionHistoryListCommand(id);
        var result = getTransactionHistoryListCommandHandler.Handle(command);
        return Ok(result.MapToDtoList());
    }

    [Route("transactionhistory")]
    [HttpPost]
    public async Task<ActionResult> BuyShoppingCartItems([FromBody] AddTransactionHistoryDto addTransactionHistoryDto)
    {
        var command = new AddTransactionHistoryCommand(addTransactionHistoryDto);
        var result = addTransactionHistoryCommandHandler.Handle(command);
        return Ok(result.MapToDto());
    }
}
