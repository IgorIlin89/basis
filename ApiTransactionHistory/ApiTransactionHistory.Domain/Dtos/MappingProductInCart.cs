namespace ApiTransactionHistory.Domain.Dtos;

public static class MappingProductInCart
{
    public static ProductInCart MapToProductInCart(this ProductInCartDto productInCartDto)
    {
        return new ProductInCart
        {
            Id = productInCartDto.Id,
            ProductId = productInCartDto.ProductId,
            Count = productInCartDto.Count,
            PricePerProduct = productInCartDto.PricePerProduct,
            TransactionHistoryId = productInCartDto.TransactionHistoryId
        };
    }

    public static List<ProductInCart> MapToProductInCartList(this ICollection<ProductInCartDto> productsInCart)
    {
        var listProductsInCart = new List<ProductInCart>();

        foreach (var element in productsInCart)
        {
            listProductsInCart.Add(element.MapToProductInCart());
        }

        return listProductsInCart;
    }

    public static ProductInCartDto MapToDto(this ProductInCart productInCart)
    {
        return new ProductInCartDto
        {
            Id = productInCart.Id,
            ProductId = productInCart.ProductId,
            Count = productInCart.Count,
            PricePerProduct = productInCart.PricePerProduct,
            TransactionHistoryId = productInCart.TransactionHistoryId
        };
    }

    public static List<ProductInCartDto> MapToDtoList(
        this ICollection<ProductInCart> productsInCartDto)
    {
        var result = new List<ProductInCartDto>();

        foreach (var element in productsInCartDto)
        {
            result.Add(element.MapToDto());
        }

        return result;
    }
}
