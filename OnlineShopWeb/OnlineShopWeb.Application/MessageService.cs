using NServiceBus;
using OnlineShopWeb.NServiceBus.Messages;
using Messaging = NServiceBus;
namespace OnlineShopWeb.Application;

public class MessageService : IMessageService
{
    private readonly Messaging.IMessageSession _messageContext;
    public MessageService()
    {

    }
    //TODO
    //public MessageService(NServiceBus.IMessageSession context)
    //{
    // _messageContext = context;
    //}
    public async Task SendOrder()
    {
        try
        {
            var count = 101;
            await _messageContext.Publish(new TestEvent
            {
                Count = count
            });

            await _messageContext.Send(new TestCommand
            {
                Transaction = new TransactionHistory()
            });
        }
        catch (Exception)
        {

            //throw new DomainException($"Unexpected error while publishing/sending");
        }
    }
}
