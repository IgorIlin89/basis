using ApiTransactionHistory.Application.Commands;
using ApiTransactionHistory.Domain;

namespace ApiTransactionHistory.Application.Handlers;

public interface IGetTransactionHistoryListCommandHandler
{
    List<TransactionHistory> Handle(GetTransactionHistoryListCommand command);
}