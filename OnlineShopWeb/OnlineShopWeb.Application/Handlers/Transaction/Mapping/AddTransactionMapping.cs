using OnlineShopWeb.Domain;
using OnlineShopWeb.Domain.Commands;
using OnlineShopWeb.Messages.V2;
using OnlineShopWeb.Messages.V2.Events;

namespace OnlineShopWeb.Application.Handlers.Transaction.Mapping;

public static class AddTransactionMapping
{
    public static TypeOfDiscountDto MapToDto(this TypeOfDiscountTransactionCoupon domain)
        => domain switch
        {
            TypeOfDiscountTransactionCoupon.Percentage => TypeOfDiscountDto.Percentage,
            TypeOfDiscountTransactionCoupon.Total => TypeOfDiscountDto.Total,
            _ => throw new NotImplementedException(),
        };

    public static AddProductInCartDto MapToDto(this ProductInCart domain)
        => new AddProductInCartDto
        {
            ProductId = domain.ProductId,
            Count = domain.Count,
            PricePerProduct = domain.PricePerProduct
        };

    public static IReadOnlyCollection<AddProductInCartDto> MapToDto(this IReadOnlyCollection<ProductInCart> domain)
    => domain.Select(o => o.MapToDto()).ToList();

    public static TransactionCouponDto MapToDto(this TransactionCoupon domain)
        => new TransactionCouponDto
        {
            Code = domain.Code,
            AmountOfDiscount = domain.AmountOfDiscount,
            TypeOfDiscount = domain.TypeOfDiscount.MapToDto()
        };

    public static IReadOnlyCollection<TransactionCouponDto> MapToDto(this IReadOnlyCollection<TransactionCoupon> domain)
        => domain.Select(o => o.MapToDto()).ToList();

    public static AddTransactionEvent MapToEvent(this AddTransactionCommand domain)
        => new AddTransactionEvent
        {
            UserId = domain.UserId,
            AddProductsInCartDto = domain.ProductsInCart.MapToDto(),
            AddCouponsDto = domain.Coupons.MapToDto()
        };
}
