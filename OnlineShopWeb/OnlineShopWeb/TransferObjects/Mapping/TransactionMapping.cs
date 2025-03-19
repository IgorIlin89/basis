using OnlineShopWeb.Domain;
using OnlineShopWeb.TransferObjects.Models;

namespace OnlineShopWeb.TransferObjects.Mapping;

public static class TransactionMapping
{

    public static OnlineShopWeb.Domain.ProductInCart MapToDomain(this ProductInCartModel model)
        => OnlineShopWeb.Domain.ProductInCart.Create(model.ProductModelInCart.ProductId.Value,
            model.Count,
            model.ProductModelInCart.Price);
    public static List<OnlineShopWeb.Domain.ProductInCart> MapToDomainList(
        this List<ProductInCartModel> model)
        => model.Select(o => o.MapToDomain()).ToList();

    public static TransactionModel MapToModel(this Transaction transaction) =>
        new TransactionModel
        {
            Id = transaction.Id,
            UserId = transaction.UserId,
            PaymentDate = transaction.PaymentDate,
            FinalPrice = transaction.FinalPrice
        };

    public static IReadOnlyCollection<TransactionModel> MapToModelList(this IReadOnlyCollection<Transaction> transactionList) =>
        transactionList.Select(o => o.MapToModel()).ToList();

    public static GrpcAdapter.DTOs.AddProductInCartDto MapToDtoGrpc(this ProductInCartModel model) =>
        new GrpcAdapter.DTOs.AddProductInCartDto
        {
            Count = model.Count,
            ProductId = model.ProductModelInCart.ProductId is null ? 0 : model.ProductModelInCart.ProductId.Value,
        };

    public static ICollection<GrpcAdapter.DTOs.AddProductInCartDto> MapToDtoGrpcList(
        this ICollection<ProductInCartModel> modelList) =>
        modelList.Select(o => o.MapToDtoGrpc()).ToList();

    public static TransactionAdapter.DTOs.ProductInCartDto MapToDtoTransactionAdapter(this ProductInCartModel model) =>
        new TransactionAdapter.DTOs.ProductInCartDto
        {
            Count = model.Count,
            ProductId = model.ProductModelInCart.ProductId is null ? 0 : model.ProductModelInCart.ProductId.Value,
        };

    public static List<TransactionAdapter.DTOs.ProductInCartDto> MapToDtoListHttp(
        this IReadOnlyCollection<ProductInCartModel> modelList) =>
        modelList.Select(o => o.MapToDtoTransactionAdapter()).ToList();

    public static OnlineShopWeb.Messages.V1.TypeOfDiscountDto MapToDtoMessages(this TypeOfDiscountCouponModel model) =>
        model switch
        {
            TypeOfDiscountCouponModel.Percentage => OnlineShopWeb.Messages.V1.TypeOfDiscountDto.Percentage,
            TypeOfDiscountCouponModel.Total => OnlineShopWeb.Messages.V1.TypeOfDiscountDto.Total,
            _ => throw new NotImplementedException(),
        };

    public static OnlineShopWeb.Messages.V1.AddTransactionToCouponsDto MapToServiceBus(this CouponModel model) =>
    new OnlineShopWeb.Messages.V1.AddTransactionToCouponsDto
    {
        CouponId = model.CouponId is null ? 0 : model.CouponId.Value,
        Code = model.Code,
        AmountOfDiscount = model.AmountOfDiscount,
        TypeOfDiscountDto = model.TypeOfDiscount.MapToDtoMessages()
    };

    public static List<OnlineShopWeb.Messages.V1.AddTransactionToCouponsDto> MapToServiceBusList(
        this List<CouponModel> modelList) =>
        modelList.Select(o => o.MapToServiceBus()).ToList();

    public static OnlineShopWeb.Messages.V1.AddProductInCartDto MapToServiceBus(this ProductInCartModel model) =>
    new OnlineShopWeb.Messages.V1.AddProductInCartDto
    {
        Count = model.Count,
        PricePerProduct = model.ProductModelInCart.Price,
        ProductId = model.ProductModelInCart.ProductId is null ? 0 : model.ProductModelInCart.ProductId.Value
    };

    public static List<OnlineShopWeb.Messages.V1.AddProductInCartDto> MapToServiceBusList(
        this List<ProductInCartModel> modelList) =>
        modelList.Select(o => o.MapToServiceBus()).ToList();

    public static TransactionAdapter.DTOs.TypeOfDiscountTransactionCouponDto MapToDtoAdapter(this TypeOfDiscountCouponModel model) =>
        model switch
        {
            TypeOfDiscountCouponModel.Percentage => TransactionAdapter.DTOs.TypeOfDiscountTransactionCouponDto.Percentage,
            TypeOfDiscountCouponModel.Total => TransactionAdapter.DTOs.TypeOfDiscountTransactionCouponDto.Total,
            _ => throw new NotImplementedException()
        };

    public static GrpcAdapter.DTOs.TypeOfDiscountDto MapToDtoGrpcAdapter(this TypeOfDiscountCouponModel model) =>
        model switch
        {
            TypeOfDiscountCouponModel.Percentage => GrpcAdapter.DTOs.TypeOfDiscountDto.Percentage,
            TypeOfDiscountCouponModel.Total => GrpcAdapter.DTOs.TypeOfDiscountDto.Total,
            _ => throw new NotImplementedException()
        };

    ///
    public static GrpcAdapter.DTOs.TransactionToCouponsDto MapToDtoGrpcAdapter(this CouponModel model) =>
        new GrpcAdapter.DTOs.TransactionToCouponsDto
        {
            CouponId = model.CouponId is null ? 0 : model.CouponId.Value,
            Code = model.Code,
            AmountOfDiscount = model.AmountOfDiscount,
            TypeOfDiscountDto = model.TypeOfDiscount.MapToDtoGrpcAdapter()
        };

    public static List<GrpcAdapter.DTOs.TransactionToCouponsDto>? MapToDtoListGrpc(
        this List<CouponModel> modelList) =>
        modelList?.Select(o => o.MapToDtoGrpcAdapter()).ToList();

    public static TypeOfDiscountTransactionCoupon MapToDomain(this TypeOfDiscountTransactionCouponModel model)
        => model switch
        {
            TypeOfDiscountTransactionCouponModel.Percentage => TypeOfDiscountTransactionCoupon.Percentage,
            TypeOfDiscountTransactionCouponModel.Total => TypeOfDiscountTransactionCoupon.Total,
            _ => throw new NotImplementedException(),
        };

    public static TransactionCoupon MapToDomain(this TransactionCouponModel model)
        => new TransactionCoupon
        {
            Code = model.Code,
            AmountOfDiscount = model.AmountOfDiscount,
            TypeOfDiscount = model.TypeOfDiscount.MapToDomain(),
        };

    public static IReadOnlyCollection<TransactionCoupon> MapToDomainList(this IReadOnlyCollection<TransactionCouponModel> list)
        => list.Select(o => o.MapToDomain()).ToList();

}