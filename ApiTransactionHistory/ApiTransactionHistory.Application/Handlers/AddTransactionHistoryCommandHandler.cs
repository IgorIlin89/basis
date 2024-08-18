using ApiTransactionHistory.Application.Commands;
using ApiTransactionHistory.Database.Interfaces;
using ApiTransactionHistory.Domain;

namespace ApiTransactionHistory.Application.Handlers;

public class AddTransactionHistoryCommandHandler(IUnitOfWork UnitOfWork,
    IApiTransactionHistoryRepository apiTransactionHistoryRepository) : IAddTransactionHistoryCommandHandler
{
    public TransactionHistory Handle(AddTransactionHistoryCommand command)
    {
        //command.TransactionHistoryToAdd.CalculateFinalPrice();

        var result = apiTransactionHistoryRepository.Add(command.TransactionHistoryToAdd);

        UnitOfWork.SaveChanges();

        return result;
    }
}
