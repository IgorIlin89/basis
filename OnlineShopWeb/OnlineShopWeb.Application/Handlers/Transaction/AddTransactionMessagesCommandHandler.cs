using NServiceBus;
using OnlineShopWeb.Application.Commands.Transaction;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Messages.V2;

namespace OnlineShopWeb.Application.Handlers.Transaction;

public class AddTransactionMessagesCommandHandler(IMessageSession _messageSession)
    : IAddTransactionMessagesCommandHandler
{
    public async void Handle(AddTransactionCommandHttp command,
        CancellationToken cancellationToken)
    {
        var addTransactionObject = AddTransaction.Create(
            Int32.Parse(command.UserId),
            command.ProductInCarts,
            command.Coupons);

        var message = new OnlineShopWeb.Messages.V2.Events.AddTransactionEvent
        {
            UserId = Int32.Parse(command.UserId),
            AddProductsInCartDto = (List<AddProductInCartDto>)command.ProductInCarts,
            AddCouponsDto = (List<TransactionCouponDto>)command.Coupons
        };

        await _messageSession.Publish(message, cancellationToken);
    }
}
