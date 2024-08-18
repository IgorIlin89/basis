namespace OnlineShopWeb.NServiceBus.Messages;

public class TestCommand : ICommand
{
    public TransactionHistory Transaction { get; set; }
}
