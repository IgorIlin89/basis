using OnlineShopWeb.Application.Commands.Transaction;

namespace OnlineShopWeb.Application.Interfaces;

public interface IGetTransactionListCommandHandler
{
    Task<ICollection<Domain.Transaction>> Handle(GetTransactionListCommand command);
}