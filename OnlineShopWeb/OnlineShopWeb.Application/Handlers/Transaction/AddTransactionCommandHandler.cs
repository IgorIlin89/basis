using OnlineShopWeb.Application.Commands.Transaction;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain;
using TransactionAdapter;
using TransactionAdapter.Mapping;

namespace OnlineShopWeb.Application.Handlers.Transaction;

public class AddTransactionCommandHandler(ITransactionAdapter transactionAdapter) : IAddTransactionCommandHandler
{
    public async Task<Domain.Transaction> Handle(AddTransactionCommand command)
    {
        //TODO remove addTransaction
        var addTransaction = new AddTransaction
        {
            UserId = Int32.Parse(command.UserId),
            AddProductsInCart = command.AddProductsInCartDto.MapToDomainList(),
            AddCoupons = command.AddCouponsDto.MapToDomainList()
        };

        var result = await transactionAdapter.AddTransaction(addTransaction);
        return result;
    }
}
