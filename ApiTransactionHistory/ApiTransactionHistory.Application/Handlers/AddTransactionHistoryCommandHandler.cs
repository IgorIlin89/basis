using ApiTransactionHistory.Application.Commands;
using ApiTransactionHistory.Domain;

namespace ApiTransactionHistory.Application.Handlers;

public class AddTransactionHistoryCommandHandler
{
    public TransactionHistory Handle(AddTransactionHistoryCommand command)
    {
        //TODO
        return new TransactionHistory();
    }
}
