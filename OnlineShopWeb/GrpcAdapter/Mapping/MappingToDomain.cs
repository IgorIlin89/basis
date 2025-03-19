using Google.Protobuf.Collections;
using GrpcTestService.Contracts;

namespace GrpcAdapter.Mapping;

public static class MappingToDomain
{
    public static OnlineShopWeb.Domain.TypeOfDiscountTransactionCoupon MapToDomain(this int typeOfDiscount)
        => typeOfDiscount switch
        {
            1 => OnlineShopWeb.Domain.TypeOfDiscountTransactionCoupon.Percentage,
            2 => OnlineShopWeb.Domain.TypeOfDiscountTransactionCoupon.Total,
            _ => throw new NotImplementedException()
        };

    public static OnlineShopWeb.Domain.TransactionCoupon MapToDomain(this TransactionCoupon coupon)
        => new OnlineShopWeb.Domain.TransactionCoupon
        {
            Code = coupon.Code,
            AmountOfDiscount = coupon.AmountOfDiscount,
            TypeOfDiscount = coupon.TypeOfDiscount.MapToDomain(),
        };

    public static IReadOnlyCollection<OnlineShopWeb.Domain.TransactionCoupon> MapToDomain(this RepeatedField<TransactionCoupon> list)
        => list.Select(o => o.MapToDomain()).ToList();

    public static OnlineShopWeb.Domain.ProductInCart MapToDomain(this ProductInCart productInCart)
        => OnlineShopWeb.Domain.ProductInCart.Create(
            productInCart.ProductId,
            productInCart.Count,
            Decimal.Parse(productInCart.PricePerProduct));
    public static IReadOnlyCollection<OnlineShopWeb.Domain.ProductInCart> MapToDomain(this RepeatedField<ProductInCart> list)
        => list.Select(o => o.MapToDomain()).ToList();

    public static OnlineShopWeb.Domain.Transaction MapToDomain(this Transaction transaction)
        => OnlineShopWeb.Domain.Transaction.Create(
            transaction.Id,
            transaction.UserId,
            DateTimeOffset.Parse(transaction.PaymentDate),
            Decimal.Parse(transaction.FinalPrice),
            transaction.ProductsInCart.MapToDomain(),
            transaction.CouponList.MapToDomain()
            );


    public static IReadOnlyCollection<OnlineShopWeb.Domain.Transaction> MapToDomain(this TransactionList list)
        => list.TransactionList_.Select(o => o.MapToDomain()).ToList();
}