using OnlineShopWeb.Application.Commands.Transaction;

namespace OnlineShopWeb.Application.Interfaces;

public interface IAddTransactionCommandHandler
{
    Task<Domain.Transaction> Handle(AddTransactionCommandHttp command,
        CancellationToken cancellationToken);
}