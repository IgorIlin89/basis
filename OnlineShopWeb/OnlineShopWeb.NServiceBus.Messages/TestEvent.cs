namespace OnlineShopWeb.NServiceBus.Messages;

public class TestEvent : IEvent
{
    public int Count { get; set; }
}
