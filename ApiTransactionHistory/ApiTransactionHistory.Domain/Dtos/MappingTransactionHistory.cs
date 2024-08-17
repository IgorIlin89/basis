﻿namespace ApiTransactionHistory.Domain.Dtos;

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
        transactionHistory.TransactionHistoryToCouponsId = addTransactionHistoryDto.TransactionHistoryToCouponsId;
        transactionHistory.UserId = addTransactionHistoryDto.UserId;
        transactionHistory.PaymentDate = addTransactionHistoryDto.PaymentDate;
        transactionHistory.FinalPrice = addTransactionHistoryDto.FinalPrice;
        transactionHistory.Coupons = addTransactionHistoryDto.CouponsDto.MapToTransactionHistoryToCoupons();
        transactionHistory.ProductsInCart = addTransactionHistoryDto.ProductsInCartDto.MapToProductInCartList();

        return transactionHistory;
    }

    public static TransactionHistoryDto MapToDto(this TransactionHistory transactionHistory)
    {
        return new TransactionHistoryDto
        {
            Id = transactionHistory.Id,
            TransactionHistoryToCouponsId = transactionHistory.TransactionHistoryToCouponsId,
            UserId = transactionHistory.UserId,
            PaymentDate = transactionHistory.PaymentDate,
            FinalPrice = transactionHistory.FinalPrice,
            CouponsDto = transactionHistory.Coupons.MapToDto(),
            ProductsInCartDto = transactionHistory.ProductsInCart.MapToDtoList()
        };
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
