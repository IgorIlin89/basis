namespace ApiTransactionHistory.Domain;

public class TransactionHistory
{
    public int Id { get; set; }
    //userid is not null in database
    public DateTimeOffset PaymentDate { get; set; }
    public decimal FinalPrice { get; set; }
}
