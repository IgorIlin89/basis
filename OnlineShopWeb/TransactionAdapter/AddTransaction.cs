using OnlineShopWeb.Domain;

namespace TransactionAdapter;

public class AddTransaction
{
    public int UserId { get; init; }
    public ICollection<AddProductInCart> AddProductsInCart { get; init; }
    public ICollection<AddTransactionToCoupons>? AddCoupons { get; init; }
}
