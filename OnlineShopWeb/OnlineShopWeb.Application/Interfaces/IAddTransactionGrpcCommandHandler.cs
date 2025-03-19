using OnlineShopWeb.Application.Commands.Transaction;

namespace OnlineShopWeb.Application.Interfaces;

public interface IAddTransactionGrpcCommandHandler
{
    Task<Domain.Transaction> Handle(AddTransactionCommandGrpc command, CancellationToken cancellationToken);
}