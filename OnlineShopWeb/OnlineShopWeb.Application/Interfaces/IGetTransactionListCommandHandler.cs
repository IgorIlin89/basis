using OnlineShopWeb.Application.Commands.Transaction;

namespace OnlineShopWeb.Application.Interfaces;

public interface IGetTransactionListCommandHandler
{
    Task<IReadOnlyCollection<Domain.Transaction>> Handle(GetTransactionListCommand command);
}