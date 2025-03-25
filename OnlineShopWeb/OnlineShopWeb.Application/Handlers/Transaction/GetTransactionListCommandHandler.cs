using OnlineShopWeb.Application.Commands.Transaction;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain.Interfaces;

namespace OnlineShopWeb.Application.Handlers.Transaction;

public class GetTransactionListCommandHandler(ITransactionAdapter transactionAdapter) : IGetTransactionListCommandHandler
{
    public async Task<IReadOnlyCollection<Domain.Transaction>> Handle(GetTransactionListCommand command)
    {
        var result = await transactionAdapter.GetTransactionList(command.UserId);
        return result;
    }
}
