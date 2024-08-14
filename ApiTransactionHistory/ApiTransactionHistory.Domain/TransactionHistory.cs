﻿namespace ApiTransactionHistory.Domain;

public class TransactionHistory
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
    public decimal FinalPrice { get; set; }
}
