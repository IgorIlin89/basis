namespace OnlineShopWeb.Application.Commands.Transaction;

public record GetTransactionListCommand
{
    public readonly string UserId;

    public GetTransactionListCommand(string userId)
    {
        UserId = userId;
    }
}
