using ApiTransactionHistory.Application.Commands;
using ApiTransactionHistory.Domain;

namespace ApiTransactionHistory.Application.Handlers;

public interface IAddTransactionHistoryCommandHandler
{
    TransactionHistory Handle(AddTransactionHistoryCommand command);
}