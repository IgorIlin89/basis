using OnlineShopWeb.Domain;
using TransactionAdapter.DTOs;

namespace TransactionAdapter.Mapping;

public static class TransactionMapping
{
    public static TransactionCoupon MapToDomain(this TransactionCouponDto couponDto) =>
        new TransactionCoupon
        {
            Code = couponDto.Code,
            AmountOfDiscount = couponDto.AmountOfDiscount,
            TypeOfDiscount = couponDto.TypeOfDiscountDto.MapToDomain(),
        };

    public static IReadOnlyCollection<TransactionCoupon> MapToDomain(this IReadOnlyCollection<TransactionCouponDto> couponDtoList) =>
        couponDtoList.Select(o => o.MapToDomain()).ToList();

    public static ProductInCart MapToDomain(this ProductInCartDto productInCartDto)
        => OnlineShopWeb.Domain.ProductInCart.Create(
            productInCartDto.ProductId,
            productInCartDto.Count,
            productInCartDto.PricePerProduct);


    public static IReadOnlyCollection<ProductInCart> MapToDomain(this IReadOnlyCollection<ProductInCartDto> productsInCartCollection) =>
        productsInCartCollection.Select(o => o.MapToDomain()).ToList();

    public static Transaction MapToDomain(this TransactionDto transactionDto) =>
        Transaction.Create(
            transactionDto.Id,
            transactionDto.UserId,
            transactionDto.PaymentDate,
            transactionDto.FinalPrice,
            transactionDto.ProductsInCartDto.MapToDomain(),
            transactionDto.CouponsDto.MapToDomain());

    public static IReadOnlyCollection<Transaction> MapToDomain(this IReadOnlyCollection<TransactionDto> transactionDtoList) =>
        transactionDtoList.Select(o => o.MapToDomain()).ToList();

    public static TypeOfDiscountTransactionCoupon MapToDomain(this TypeOfDiscountTransactionCouponDto dto) =>
        dto switch
        {
            TypeOfDiscountTransactionCouponDto.Percentage => TypeOfDiscountTransactionCoupon.Percentage,
            TypeOfDiscountTransactionCouponDto.Total => TypeOfDiscountTransactionCoupon.Total,
            _ => throw new NotImplementedException()
        };

    public static TypeOfDiscountTransactionCouponDto MapToDto(this TypeOfDiscountTransactionCoupon dto) =>
        dto switch
        {
            TypeOfDiscountTransactionCoupon.Percentage => TypeOfDiscountTransactionCouponDto.Percentage,
            TypeOfDiscountTransactionCoupon.Total => TypeOfDiscountTransactionCouponDto.Total,
            _ => throw new NotImplementedException()
        };

    public static TransactionCouponDto MapToDto(this TransactionCoupon domain)
        => new TransactionCouponDto
        {
            Code = domain.Code,
            AmountOfDiscount = domain.AmountOfDiscount,
            TypeOfDiscountDto = domain.TypeOfDiscount.MapToDto()
        };

    public static IReadOnlyCollection<TransactionCouponDto> MapToDto(this IReadOnlyCollection<TransactionCoupon> domain)
        => domain.Select(o => o.MapToDto()).ToList();

    public static ProductInCartDto MapToDto(this ProductInCart domain)
        => new ProductInCartDto
        {
            ProductId = domain.ProductId,
            PricePerProduct = domain.PricePerProduct,
            Count = domain.Count,
        };

    public static IReadOnlyCollection<ProductInCartDto> MapToDto(this IReadOnlyCollection<ProductInCart> domainList)
        => domainList.Select(o => o.MapToDto()).ToList();

    public static TransactionDto MapToDto(this Transaction domain)
        => new TransactionDto
        {
            Id = domain.Id,
            UserId = domain.UserId,
            PaymentDate = domain.PaymentDate,
            FinalPrice = domain.FinalPrice,
            ProductsInCartDto = domain.ProductsInCart.MapToDto(),
            CouponsDto = domain.Coupons.MapToDto()
        };

    public static IReadOnlyCollection<TransactionDto> MapToDto(this IReadOnlyCollection<Transaction> domainList)
        => domainList.Select(o => o.MapToDto()).ToList();
}