namespace ApiTransactionHistory.Domain;

public class AddTransactionHistory
{
    public DateTimeOffset PaymentDate { get; set; }
    public decimal FinalPrice { get; set; }
    public int UserId { get; set; }
}
