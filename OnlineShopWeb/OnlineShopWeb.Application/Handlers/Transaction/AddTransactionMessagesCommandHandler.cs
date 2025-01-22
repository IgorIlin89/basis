using NServiceBus;
using OnlineShopWeb.Application.Commands.Transaction;
using OnlineShopWeb.Application.Interfaces;

namespace OnlineShopWeb.Application.Handlers.Transaction;

public class AddTransactionMessagesCommandHandler(IMessageSession _messageSession)
    : IAddTransactionMessagesCommandHandler
{
    public async void Handle(AddTransactionCommandMessages command)
    {
        var message = new OnlineShopWeb.Messages.V1.Events.AddTransactionEvent
        {
            UserId = Int32.Parse(command.UserId),
            PaymentDate = DateTimeOffset.UtcNow,
            AddProductsInCartDto = command.ProductInCartDtos,
            AddCouponsDto = command.TransactionToCouponsDto
        };

        await _messageSession.Publish(message, new CancellationToken());
    }
}
