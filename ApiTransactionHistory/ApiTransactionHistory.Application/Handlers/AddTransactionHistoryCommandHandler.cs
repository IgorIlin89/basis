using ApiTransactionHistory.Application.Commands;
using ApiTransactionHistory.Database.Interfaces;
using ApiTransactionHistory.Domain;

namespace ApiTransactionHistory.Application.Handlers;

public class AddTransactionHistoryCommandHandler(IUnitOfWork UnitOfWork,
    IApiTransactionHistoryRepository apiTransactionHistoryRepository) : IAddTransactionHistoryCommandHandler
{
    public TransactionHistory Handle(AddTransactionHistoryCommand command)
    {
        command.TransactionHistoryToAdd.FinalPrice = CalculateFinalPrice(
            command.TransactionHistoryToAdd.ProductsInCart,
            command.TransactionHistoryToAdd.Coupons);

        var result = apiTransactionHistoryRepository.Add(command.TransactionHistoryToAdd);

        UnitOfWork.SaveChanges();

        return result;
    }

    private decimal CalculateFinalPrice(ICollection<ProductInCart> productsInCart,
        TransactionHistoryToCoupons? coupons)
    {
        decimal result = new();

        foreach (var element in productsInCart)
        {
            result += element.PricePerProduct * element.Count;
        }

        return result;
    }
}
