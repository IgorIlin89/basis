using OnlineShopWeb.Domain;
using TransactionAdapter.DTOs;

namespace TransactionAdapter.Mapping;

public static class TransactionMapping
{
    public static User MapToUser(this UserDto userDto) =>
    new User
    {
        Id = userDto.UserId is null ? 0 : userDto.UserId.Value,
        EMail = userDto.EMail,
        Password = userDto.Password,
        GivenName = userDto.GivenName,
        Surname = userDto.Surname,
        Age = userDto.Age,
        Country = userDto.Country,
        City = userDto.City,
        Street = userDto.Street,
        HouseNumber = userDto.HouseNumber,
        PostalCode = userDto.PostalCode,
    };

    public static Coupon MapToDto(this CouponDto couponDto) =>
        new Coupon
        {
            Id = couponDto.CouponId.Value,
            Code = couponDto.Code,
            AmountOfDiscount = couponDto.AmountOfDiscount,
            TypeOfDiscount = (TypeOfDiscount)couponDto.TypeOfDiscount,
            MaxNumberOfUses = couponDto.MaxNumberOfUses,
            StartDate = couponDto.StartDate,
            EndDate = couponDto.EndDate
        };

    public static ICollection<Coupon> MapToCouponList(this ICollection<CouponDto> couponDtoList) =>
        couponDtoList.Select(o => o.MapToDto()).ToList();

    //Here question concerning product inclusion
    public static ProductInCart MapToProductInCart(this ProductInCartDto productInCartDto) =>
        new ProductInCart
        {
            Id = productInCartDto.Id,
            Count = productInCartDto.Count,
            ProductId = productInCartDto.ProductId,
            TransactionHistoryId = productInCartDto.TransactionId
        };

    public static ICollection<ProductInCart> MapToProductInCartList(this ICollection<ProductInCartDto> productsInCartCollection) =>
        productsInCartCollection.Select(o => o.MapToProductInCart()).ToList();

    public static Transaction MapToTransaction(this TransactionObjectsDto transactionObjectsDto) =>
        new Transaction
        {
            Id = transactionObjectsDto.Id,
            UserId = transactionObjectsDto.UserId,
            User = transactionObjectsDto.User.MapToUser(),
            PaymentDate = transactionObjectsDto.PaymentDate,
            FinalPrice = transactionObjectsDto.FinalPrice,
            Coupons = transactionObjectsDto.Coupons.MapToCouponList(),
            ProductsInCart = transactionObjectsDto.ProductsInCart.MapToProductInCartList()
        };

    public static List<Transaction> MapToTransaction(this List<TransactionObjectsDto> transactionDtoList) =>
        transactionDtoList.Select(o => o.MapToTransaction()).ToList();

    public static AddTransactionToCouponsDto MapToDto(this AddTransactionToCoupons addTransactionToCoupons) =>
        new AddTransactionToCouponsDto
        {
            CouponId = addTransactionToCoupons.CouponId,
            Code = addTransactionToCoupons.Code,
            AmountOfDiscount = addTransactionToCoupons.AmountOfDiscount,
            TypeOfDiscountDto = (TypeOfDiscountDto)addTransactionToCoupons.TypeOfDiscountDto
        };

    public static ICollection<AddTransactionToCouponsDto> MapToDtoList(this ICollection<AddTransactionToCoupons> list) =>
        list.Select(o => o.MapToDto()).ToList();

    public static TypeOfDiscount MapToDomain(this TypeOfDiscountDto dto) =>
        dto switch
        {
            TypeOfDiscountDto.Percentage => TypeOfDiscount.Percentage,
            TypeOfDiscountDto.Total => TypeOfDiscount.Total,
            _ => throw new NotImplementedException()
        };

    public static AddTransactionToCoupons MapToDomain(this AddTransactionToCouponsDto dto) =>
    new AddTransactionToCoupons
    {
        CouponId = dto.CouponId,
        Code = dto.Code,
        AmountOfDiscount = dto.AmountOfDiscount,
        TypeOfDiscountDto = dto.TypeOfDiscountDto.MapToDomain()
    };

    public static ICollection<AddTransactionToCoupons> MapToDomainList(this ICollection<AddTransactionToCouponsDto> dtoList) =>
        dtoList.Select(o => o.MapToDomain()).ToList();

    public static AddProductInCartDto MapToDto(this AddProductInCart addProductInCart) =>
        new AddProductInCartDto
        {
            Count = addProductInCart.Count,
            ProductId = addProductInCart.ProductId,
            PricePerProduct = addProductInCart.PricePerProduct,
            TransactionId = addProductInCart.TransactionId
        };

    public static ICollection<AddProductInCartDto> MapToDtoList(this ICollection<AddProductInCart> list) =>
        list.Select(o => o.MapToDto()).ToList();

    public static AddProductInCart MapToDomain(this AddProductInCartDto addProductInCart) =>
    new AddProductInCart
    {
        Count = addProductInCart.Count,
        ProductId = addProductInCart.ProductId,
        PricePerProduct = addProductInCart.PricePerProduct,
        TransactionId = addProductInCart.TransactionId
    };

    public static ICollection<AddProductInCart> MapToDomainList(this ICollection<AddProductInCartDto> dtoList) =>
        dtoList.Select(o => o.MapToDomain()).ToList();

    public static AddTransactionDto MapToDto(this AddTransaction addTransaction) =>
        new AddTransactionDto
        {
            UserId = addTransaction.UserId,
            AddProductsInCartDto = addTransaction.AddProductsInCart.MapToDtoList(),
            AddCouponsDto = addTransaction.AddCoupons.MapToDtoList()
        };
}