﻿namespace OnlineShopWeb.TransferObjects.Dtos;

public class TransactionHistoryDto
{
    public int Id { get; set; }
    public int? TransactionHistoryToCouponsId { get; set; }
    public int UserId { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
    public decimal? FinalPrice { get; set; }
    public TransactionHistoryToCouponsDto? CouponsDto { get; set; }
    public ICollection<ProductInCartDto> ProductsInCartDto { get; set; }
}
