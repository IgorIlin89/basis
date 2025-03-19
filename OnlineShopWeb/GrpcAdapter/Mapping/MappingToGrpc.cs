

using GrpcTestService.Contracts;

namespace GrpcAdapter.Mapping;

public static class MappingToGrpc
{


    public static ProductInCart MapToGrpcObject(this OnlineShopWeb.Domain.ProductInCart domain)
        => new ProductInCart
        {
            Count = domain.Count,
            ProductId = domain.ProductId,
            PricePerProduct = domain.PricePerProduct.ToString()
        };

    public static List<ProductInCart> MapToGrpcObject(this IReadOnlyCollection<OnlineShopWeb.Domain.ProductInCart> list)
        => list.Select(o => o.MapToGrpcObject()).ToList();

    public static int MapToGrpcObject(
        this OnlineShopWeb.Domain.TypeOfDiscountTransactionCoupon domainObject)
        => domainObject switch
        {
            OnlineShopWeb.Domain.TypeOfDiscountTransactionCoupon.Percentage => 1,
            OnlineShopWeb.Domain.TypeOfDiscountTransactionCoupon.Total => 2,
            _ => throw new NotImplementedException(),
        };

    public static TransactionCoupon MapToGrpcObject(
        this OnlineShopWeb.Domain.TransactionCoupon domainObject)
        => new TransactionCoupon
        {
            Code = domainObject.Code,
            AmountOfDiscount = domainObject.AmountOfDiscount,
            TypeOfDiscount = domainObject.TypeOfDiscount.MapToGrpcObject(),
        };

    public static IReadOnlyCollection<TransactionCoupon> MapToGrpcObject(this IReadOnlyCollection<OnlineShopWeb.Domain.TransactionCoupon> list)
        => list.Select(o => o.MapToGrpcObject()).ToList();
}
