namespace ApiTransactionHistory.Domain.Dtos;

public static class MappingTransactionHistory
{
    public static TransactionHistory MapToTransactionHistory(this AddTransactionHistoryDto addTransactionHistoryDto)
    {
        var transactionHistory = new TransactionHistory();

        if (addTransactionHistoryDto.Id is not null)
        {
            transactionHistory.Id = addTransactionHistoryDto.Id.Value;
        }

        if (addTransactionHistoryDto.TransactionHistoryToCouponsId is not null)
        {
            transactionHistory.TransactionHistoryToCouponsId = addTransactionHistoryDto.TransactionHistoryToCouponsId;
        }

        transactionHistory.UserId = addTransactionHistoryDto.UserId;
        transactionHistory.PaymentDate = addTransactionHistoryDto.PaymentDate;
        transactionHistory.FinalPrice = addTransactionHistoryDto.FinalPrice;

        if (addTransactionHistoryDto.CouponsDto is not null)
        {
            transactionHistory.Coupons = addTransactionHistoryDto.CouponsDto.MapToTransactionHistoryToCoupons();
        }

        transactionHistory.ProductsInCart = addTransactionHistoryDto.ProductsInCartDto.MapToProductInCartList();

        return transactionHistory;
    }

    public static TransactionHistory MapToTransactionHistory(this TransactionHistoryDto addTransactionHistoryDto)
    {
        var transactionHistory = new TransactionHistory();

        transactionHistory.Id = addTransactionHistoryDto.Id;

        if (addTransactionHistoryDto.TransactionHistoryToCouponsId is not null)
        {
            transactionHistory.TransactionHistoryToCouponsId = addTransactionHistoryDto.TransactionHistoryToCouponsId;
        }

        transactionHistory.UserId = addTransactionHistoryDto.UserId;
        transactionHistory.PaymentDate = addTransactionHistoryDto.PaymentDate;
        transactionHistory.FinalPrice = addTransactionHistoryDto.FinalPrice;

        if (addTransactionHistoryDto.CouponsDto is not null)
        {
            transactionHistory.Coupons = addTransactionHistoryDto.CouponsDto.MapToTransactionHistoryToCoupons();
        }

        transactionHistory.ProductsInCart = addTransactionHistoryDto.ProductsInCartDto.MapToProductInCartList();

        return transactionHistory;
    }
}
