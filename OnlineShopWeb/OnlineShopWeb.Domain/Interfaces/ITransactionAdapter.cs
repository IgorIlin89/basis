namespace OnlineShopWeb.Domain.Interfaces;

public interface ITransactionAdapter
{
    Task<Transaction> AddTransaction(string userId, IReadOnlyCollection<ProductInCart> productInCartList,
        IReadOnlyCollection<TransactionCoupon> couponList);
    Task<IReadOnlyCollection<Transaction>> GetTransactionList(string id);
}