using OnlineShopWeb.Domain;
using OnlineShopWeb.TransferObjects.Models;

namespace OnlineShopWeb.TransferObjects.Mapping;

public static class TransactionMapping
{
    public static TransactionModel MapToModel(this Transaction transaction) =>
        new TransactionModel
        {
            Id = transaction.Id,
            UserId = transaction.UserId,
            PaymentDate = transaction.PaymentDate,
            FinalPrice = transaction.FinalPrice
        };

    public static ICollection<TransactionModel> MapToModelList(this ICollection<Transaction> transactionList) =>
        transactionList.Select(o => o.MapToModel()).ToList();

    public static Messages.V1.AddProductInCartDto MapToDtoMessages(this ProductInCartModel model) =>
        new Messages.V1.AddProductInCartDto
        {
            Count = model.Count,
            ProductId = model.ProductModelInCart.ProductId is null ? 0 : model.ProductModelInCart.ProductId.Value,
            PricePerProduct = model.ProductModelInCart.Price,
        };

    public static IReadOnlyCollection<Messages.V1.AddProductInCartDto> MapToDtoMessagesList(
        this IReadOnlyCollection<ProductInCartModel> modelList) =>
        modelList.Select(o => o.MapToDtoMessages()).ToList();

    public static TransactionAdapter.DTOs.ProductInCartDto MapToDtoTransactionAdapter(this ProductInCartModel model) =>
        new TransactionAdapter.DTOs.ProductInCartDto
        {
            Count = model.Count,
            ProductId = model.ProductModelInCart.ProductId is null ? 0 : model.ProductModelInCart.ProductId.Value,
            PricePerProduct = model.ProductModelInCart.Price,
        };

    public static List<TransactionAdapter.DTOs.ProductInCartDto> MapToDtoListAdapter(
        this IReadOnlyCollection<ProductInCartModel> modelList) =>
        modelList.Select(o => o.MapToDtoTransactionAdapter()).ToList();

    public static OnlineShopWeb.Messages.V1.TypeOfDiscountDto MapToDtoMessages(this TypeOfDiscountModel model) =>
        model switch
        {
            TypeOfDiscountModel.Percentage => OnlineShopWeb.Messages.V1.TypeOfDiscountDto.Percentage,
            TypeOfDiscountModel.Total => OnlineShopWeb.Messages.V1.TypeOfDiscountDto.Total,
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


    //TODO If ICollection here error occurs
    public static List<OnlineShopWeb.Messages.V1.AddProductInCartDto> MapToServiceBusList(
        this List<ProductInCartModel> modelList) =>
        modelList.Select(o => o.MapToServiceBus()).ToList();

    public static TransactionAdapter.DTOs.TypeOfDiscountDto MapToDtoAdapter(this TypeOfDiscountModel model) =>
        model switch
        {
            TypeOfDiscountModel.Percentage => TransactionAdapter.DTOs.TypeOfDiscountDto.Percentage,
            TypeOfDiscountModel.Total => TransactionAdapter.DTOs.TypeOfDiscountDto.Total,
            _ => throw new NotImplementedException()
        };

    public static TransactionAdapter.DTOs.TransactionToCouponsDto MapToDtoAdapter(this CouponModel model) =>
        new TransactionAdapter.DTOs.TransactionToCouponsDto
        {
            CouponId = model.CouponId is null ? 0 : model.CouponId.Value,
            Code = model.Code,
            AmountOfDiscount = model.AmountOfDiscount,
            TypeOfDiscountDto = model.TypeOfDiscount.MapToDtoAdapter()
        };

    public static List<TransactionAdapter.DTOs.TransactionToCouponsDto> MapToDtoList(
        this List<CouponModel> modelList) =>
        modelList is null ? null : modelList.Select(o => o.MapToDtoAdapter()).ToList();
}