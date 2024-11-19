using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Application.Commands.Transaction;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.TransferObjects.Mapping;

namespace OnlineShopWeb.Controllers;

public class TransactionController(IGetTransactionListCommandHandler getTransactionListCommandHandler) : Controller
{
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var command = new GetTransactionListCommand(HttpContext.User.Identity.Name);
        var transactionList = await getTransactionListCommandHandler.Handle(command);

        return View(transactionList.MapToModelList());
    }
}
