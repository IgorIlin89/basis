using NServiceBus;
using OnlineShopWeb.Application.Commands.Transaction;
using OnlineShopWeb.Application.Handlers.Transaction.Mapping;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain.Commands;

namespace OnlineShopWeb.Application.Handlers.Transaction;

public class AddTransactionMessagesCommandHandler(IMessageSession _messageSession)
    : IAddTransactionMessagesCommandHandler
{
    public async void Handle(AddTransactionCommandHttp command,
        CancellationToken cancellationToken)
    {
        var addTransactionObject = AddTransactionCommand.Create(
            Int32.Parse(command.UserId),
            command.ProductInCarts,
            command.Coupons);

        var message = new OnlineShopWeb.Messages.V2.Events.AddTransactionEvent
        {
            UserId = Int32.Parse(command.UserId),
            AddProductsInCartDto = command.ProductInCarts.MapToDto(),
            AddCouponsDto = command.Coupons.MapToDto()
        };

        await _messageSession.Publish(message, cancellationToken);
    }
}
