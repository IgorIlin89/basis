using TransactionAdapter.DTOs;

namespace OnlineShopWeb.Application.Commands.Transaction;

public record AddTransactionCommand
{
    public string UserId;
    public List<AddProductInCartDto> AddProductsInCartDto;
    public List<AddTransactionToCouponsDto>? AddCouponsDto;

    public AddTransactionCommand(string userId, List<AddProductInCartDto> addProductsInCartDto,
        List<AddTransactionToCouponsDto>? addCouponsDto)
    {
        UserId = userId;
        AddProductsInCartDto = addProductsInCartDto;
        AddCouponsDto = addCouponsDto;
    }
}
