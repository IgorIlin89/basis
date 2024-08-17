namespace ApiTransactionHistory.Application.Commands;

public record GetTransactionHistoryListCommand
{
    public int Id;
    public GetTransactionHistoryListCommand(int id)
    {
        Id = id;
    }
}
