namespace OnlineShopWeb.NServiceBus.Messages.V2;

public class TestCommand : ICommand
{
    public Transaction Transaction { get; set; }
}
