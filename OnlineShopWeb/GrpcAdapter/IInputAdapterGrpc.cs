using OnlineShopWeb.Domain;

namespace GrpcAdapter;

public interface IInputAdapterGrpc
{
    Task<Transaction> AddTransactionRpc(string userId, IReadOnlyCollection<ProductInCart> productInCartist, IReadOnlyCollection<TransactionCoupon> couponList);
}