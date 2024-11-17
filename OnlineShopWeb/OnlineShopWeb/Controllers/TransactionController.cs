using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Application.Commands.Transaction;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.TransferObjects.Mapping;
using OnlineShopWeb.TransferObjects.Models.ListModels;

namespace OnlineShopWeb.Controllers;

public class TransactionController(IGetTransactionListCommandHandler getTransactionListCommandHandler) : Controller
{
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var model = new TransactionListModel();

        var command = new GetTransactionListCommand(HttpContext.User.Identity.Name);
        var transactionList = await getTransactionListCommandHandler.Handle(command);

        model.TransactionModelList = (List<TransferObjects.Models.TransactionModel>)transactionList.MapToModelList();

        return View(model);
    }
}
