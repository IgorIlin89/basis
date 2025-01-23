using OnlineShopWeb.Application.Commands.Transaction;

namespace OnlineShopWeb.Application.Interfaces;

public interface IAddTransactionMessagesCommandHandler
{
    void Handle(AddTransactionCommandMessages command, CancellationToken cancellationToken);
}