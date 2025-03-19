using GrpcAdapter;
using OnlineShopWeb.Application.Commands.Transaction;
using OnlineShopWeb.Application.Interfaces;

namespace OnlineShopWeb.Application.Handlers.Transaction;

public class AddTransactionGrpcCommandHandler(IInputAdapterGrpc GrpcAdapter) : IAddTransactionGrpcCommandHandler
{
    public async Task<Domain.Transaction> Handle(AddTransactionCommandGrpc command,
        CancellationToken cancellationToken)
    {
        var result = await GrpcAdapter.AddTransactionRpc(
            command.UserId,
            command.ProductsInCart.ToList(),
            command.TransactionCoupons);

        return result;
    }
}
