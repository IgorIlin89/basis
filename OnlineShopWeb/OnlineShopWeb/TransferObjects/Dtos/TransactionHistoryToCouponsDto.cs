﻿namespace OnlineShopWeb.TransferObjects.Dtos;

public class TransactionHistoryToCouponsDto
{
    public int Id { get; set; }
    public int TransactionHistoryId { get; set; }
    public TransactionHistoryDto TransactionHistoryDto { get; set; }
    public ICollection<int> CouponsDtoId { get; set; }
}
