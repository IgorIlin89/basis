namespace ApiCouponProduct.Domain.Dtos;

public static class Mapping
{
    public static CouponDto MapToDto(this Coupon coupon)
    {
        return new CouponDto
        {
            CouponId = coupon.Id,
            Code = coupon.Code,
            AmountOfDiscount = coupon.AmountOfDiscount,
            TypeOfDiscount = coupon.TypeOfDiscount,
            MaxNumberOfUses = coupon.MaxNumberOfUses,
            StartDate = coupon.StartDate,
            EndDate = coupon.EndDate
        };
    }

    public static ProductDto MapToDto(this Product product)
    {
        return new ProductDto
        {
            ProductId = product.Id,
            Name = product.Name,
            Producer = product.Producer,
            Category = product.Category,
            Picture = product.Picture,
            Price = product.Price
        };
    }

    public static List<CouponDto> MapToDtoList(this List<Coupon> couponList)
    {
        var couponDtoList = new List<CouponDto>();

        foreach (var element in couponList)
        {
            couponDtoList.Add(element.MapToDto());
        }

        return couponDtoList;
    }

    public static Product MapToProduct(this ProductDto productDto)
    {
        var product = new Product();

        if (productDto.ProductId is not null)
        {
            product.Id = productDto.ProductId.Value;
        }

        product.Name = productDto.Name;
        product.Producer = productDto.Producer;
        product.Category = productDto.Category;
        product.Picture = productDto.Picture;
        product.Price = productDto.Price;

        return product;
    }

    public static Coupon MapToCoupon(this CouponDto couponDto)
    {
        var coupon = new Coupon();

        if (couponDto.CouponId is not null)
        {
            coupon.Id = couponDto.CouponId.Value;
        }

        coupon.Code = couponDto.Code;
        coupon.AmountOfDiscount = couponDto.AmountOfDiscount;
        coupon.TypeOfDiscount = couponDto.TypeOfDiscount;
        coupon.MaxNumberOfUses = couponDto.MaxNumberOfUses;
        coupon.StartDate = couponDto.StartDate;
        coupon.EndDate = couponDto.EndDate;

        return coupon;
    }

    public static List<ProductDto> MapToDtoList(this List<Product> productList)
    {
        var productDtoList = new List<ProductDto>();

        foreach (var element in productList)
        {
            productDtoList.Add(element.MapToDto());
        }

        return productDtoList;
    }
}
