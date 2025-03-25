using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Application.Commands.Transaction;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.ExtensionMethods;
using OnlineShopWeb.Models.Mapping;

namespace OnlineShopWeb.Controllers;

public class TransactionController(
    IGetTransactionListCommandHandler getTransactionListCommandHandler,
    IAuthenticationService authenticationService) : Controller
{
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var command = new GetTransactionListCommand(HttpContext.GetUserId().ToString());
        var transactionList = await getTransactionListCommandHandler.Handle(command);

        return View(transactionList.MapToModelList());
    }
}
