using OnlineShopWeb.Domain;
using OnlineShopWeb.TransferObjects.Models;

namespace OnlineShopWeb.TransferObjects.Mapping;

public static class TransactionMapping
{
    public static TransactionModel MapToModel(this Transaction transaction) =>
        new TransactionModel
        {
            Id = transaction.Id,
            UserId = transaction.UserId,
            PaymentDate = transaction.PaymentDate,
            FinalPrice = transaction.FinalPrice
        };

    public static ICollection<TransactionModel> MapToModelList(this ICollection<Transaction> transactionList) =>
        transactionList.Select(o => o.MapToModel()).ToList();
}
