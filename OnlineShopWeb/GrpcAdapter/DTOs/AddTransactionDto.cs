namespace GrpcAdapter.DTOs;

public class AddTransactionDto
{
    public int UserId { get; init; }
    public ICollection<AddProductInCartDto> AddProductsInCartDto { get; init; }
    public ICollection<AddTransactionToCouponsDto>? AddCouponsDto { get; init; }
}

