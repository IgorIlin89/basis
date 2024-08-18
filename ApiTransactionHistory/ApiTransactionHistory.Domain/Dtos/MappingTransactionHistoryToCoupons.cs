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

    public static TransactionHistoryToCouponsDto MapToDto(
        this TransactionHistoryToCoupons transactionHistoryToCoupons)
    {
        return new TransactionHistoryToCouponsDto
        {
            Id = transactionHistoryToCoupons.Id,
            TransactionHistoryId = transactionHistoryToCoupons.TransactionHistoryId,
            TransactionHistoryDto = transactionHistoryToCoupons.TransactionHistory.MapToDto(),
            //CouponsDtoId = new List<int>(transactionHistoryToCoupons.CouponsId.ToList()),
            CouponsDtoId = transactionHistoryToCoupons.CouponsId.ToList()

        };

    }
}
