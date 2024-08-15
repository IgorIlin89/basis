﻿using ApiTransactionHistory.Domain.Dtos;

namespace ApiTransactionHistory.Domain;

public class TransactionHistory
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
    public decimal FinalPrice { get; set; }
    public TransactionHistoryToCoupons? Coupons { get; set; }
    public ICollection<ProductInCartDto> ProductsInCart { get; set; }
}
