using OnlineShopWeb.Domain;

namespace TransactionAdapter;

public interface ITransactionAdapter
{
    Task<Transaction> AddTransaction(string userId, IReadOnlyCollection<ProductInCart> productInCartList,
        IReadOnlyCollection<TransactionCoupon> couponList);
    Task<IReadOnlyCollection<Transaction>> GetTransactionList(string id);
}