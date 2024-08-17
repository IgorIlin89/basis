using ApiTransactionHistory.Domain;
using ApiTransactionHistory.Domain.Dtos;
namespace ApiTransactionHistory.Application.Commands;

public record AddTransactionHistoryCommand
{
    public readonly TransactionHistory TransactionHistoryToAdd;

    public AddTransactionHistoryCommand(AddTransactionHistoryDto addTransactionHistoryDto)
    {
        TransactionHistoryToAdd = addTransactionHistoryDto.MapToTransactionHistory();
        TransactionHistoryToAdd.PaymentDate = DateTime.Now;
    }
}
