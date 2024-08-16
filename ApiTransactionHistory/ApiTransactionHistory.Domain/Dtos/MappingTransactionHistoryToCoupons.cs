namespace ApiTransactionHistory.Domain.Dtos;

public static class MappingTransactionHistoryToCoupons
{
    public static TransactionHistoryToCoupons MapToTransactionHistoryToCoupons(
        this TransactionHistoryToCouponsDto transactionHistoryToCouponsDto)
    {
        return new TransactionHistoryToCoupons
        {
            Id = transactionHistoryToCouponsDto.Id,
            TransactionHistoryId = transactionHistoryToCouponsDto.TransactionHistoryId,
            TransactionHistory = transactionHistoryToCouponsDto.TransactionHistoryDto.MapToTransactionHistory(),
            CouponsId = new List<int>(transactionHistoryToCouponsDto.CouponsDtoId.ToList())
        };
    }
}
