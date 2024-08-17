using ApiTransactionHistory.Application.Commands;
using ApiTransactionHistory.Database.Interfaces;
using ApiTransactionHistory.Domain;


namespace ApiTransactionHistory.Application.Handlers;

public class GetTransactionHistoryListCommandHandler(IApiTransactionHistoryRepository TransactionHistoryRepository) : IGetTransactionHistoryListCommandHandler
{
    public List<TransactionHistory> Handle(GetTransactionHistoryListCommand command)
    {
        var result = TransactionHistoryRepository.GetList(command.Id);
        return result;
    }
}
