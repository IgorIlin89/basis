namespace ApiTransactionHistory.Domain.Dtos;

public static class MappingTransactionHistory
{
    //public class MappingTransactionHistoryAccessToken
    //{
    //}

    //public static bool AccessAuthorization(Object token)
    //{
    //    //if(typeof(TransactionHistory) token)
    //    //return true;
    //}

    private static void CallSetFinalPrice(TransactionHistory transactionHistory)
    {
        transactionHistory.CalculateFinalPrice();
    }

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
        //transactionHistory.FinalPrice = addTransactionHistoryDto.FinalPrice;

        if (addTransactionHistoryDto.CouponsDto is not null)
        {
            transactionHistory.Coupons = addTransactionHistoryDto.CouponsDto.MapToTransactionHistoryToCoupons();
        }

        transactionHistory.ProductsInCart = addTransactionHistoryDto.ProductsInCartDto.MapToProductInCartList();

        CallSetFinalPrice(transactionHistory);

        return transactionHistory;
    }

    public static TransactionHistory MapToTransactionHistory(this TransactionHistoryDto addTransactionHistoryDto)
    {
        var transactionHistory = new TransactionHistory();

        transactionHistory.Id = addTransactionHistoryDto.Id;
        transactionHistory.TransactionHistoryToCouponsId = addTransactionHistoryDto.TransactionHistoryToCouponsId;
        transactionHistory.UserId = addTransactionHistoryDto.UserId;
        transactionHistory.PaymentDate = addTransactionHistoryDto.PaymentDate;
        //transactionHistory.FinalPrice = addTransactionHistoryDto.FinalPrice;
        if (addTransactionHistoryDto.CouponsDto is not null)
        {
            transactionHistory.Coupons = addTransactionHistoryDto.CouponsDto.MapToTransactionHistoryToCoupons();
        }
        transactionHistory.ProductsInCart = addTransactionHistoryDto.ProductsInCartDto.MapToProductInCartList();

        CallSetFinalPrice(transactionHistory);

        return transactionHistory;
    }

    //public static List<TransactionHistoryDto> MapToDtoList(
    //    this ICollection<TransactionHistory> transactionHistories)
    //{

    //}

    public static TransactionHistoryDto MapToDto(this TransactionHistory transactionHistory)
    {
        var result = new TransactionHistoryDto();

        result.Id = transactionHistory.Id;
        result.TransactionHistoryToCouponsId = transactionHistory.TransactionHistoryToCouponsId;
        result.UserId = transactionHistory.UserId;
        result.PaymentDate = transactionHistory.PaymentDate;
        result.FinalPrice = transactionHistory.FinalPrice;
        if (transactionHistory.Coupons is not null)
        {
            result.CouponsDto = transactionHistory.Coupons.MapToDto();
        }

        if (transactionHistory.ProductsInCart is not null)
        {
            result.ProductsInCartDto = transactionHistory.ProductsInCart.MapToDtoList();
        }

        return result;

        //return new TransactionHistoryDto
        //{
        //    Id = transactionHistory.Id,
        //    TransactionHistoryToCouponsId = transactionHistory.TransactionHistoryToCouponsId,
        //    UserId = transactionHistory.UserId,
        //    PaymentDate = transactionHistory.PaymentDate,
        //    FinalPrice = transactionHistory.FinalPrice,
        //    CouponsDto => transactionHistory.Coupons == null ? null : transactionHistory.Coupons.MapToDto(),
        //    ProductsInCartDto = transactionHistory.ProductsInCart.MapToDtoList()
        //};
    }

    public static List<TransactionHistoryDto> MapToDtoList(
        this ICollection<TransactionHistory> transactionHistories)
    {
        var result = new List<TransactionHistoryDto>();

        foreach (var element in transactionHistories)
        {
            result.Add(element.MapToDto());
        }

        return result;
    }
}
