using OnlineShopWeb.Application.Commands.Transaction;
using OnlineShopWeb.Application.Interfaces;
using TransactionAdapter;

namespace OnlineShopWeb.Application.Handlers.Transaction;

public class AddTransactionCommandHandler(ITransactionAdapter transactionAdapter) : IAddTransactionCommandHandler
{
    public async Task<Domain.Transaction> Handle(AddTransactionCommandReview command)
    {
        var result = await transactionAdapter.AddTransaction(command.UserId,
            command.ProductInCartDtos, command.TransactionToCouponsDto);
        return result;
    }
}