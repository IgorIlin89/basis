using GrpcAdapter.Mapping;

namespace GrpcAdapter;

public class InputAdapterGrpc : IInputAdapterGrpc
{
    private readonly GrpcTestService.Contracts.TransactionService.TransactionServiceClient _client;
    public InputAdapterGrpc(GrpcTestService.Contracts.TransactionService.TransactionServiceClient client)
    {
        _client = client;
    }

    public async Task<OnlineShopWeb.Domain.Transaction> AddTransactionRpc(
        string userId,
        IReadOnlyCollection<OnlineShopWeb.Domain.ProductInCart> productInCartist,
        IReadOnlyCollection<OnlineShopWeb.Domain.TransactionCoupon> couponList)
    {
        var transactionToAdd = new GrpcTestService.Contracts.AddTransaction();
        transactionToAdd.UserId = Int32.Parse(userId);
        transactionToAdd.ProductsInCart.AddRange(productInCartist.MapToGrpcObject());
        transactionToAdd.Coupons.AddRange(couponList.MapToGrpcObject());

        //List<int> list a=
        //var channel = GrpcChannel.ForAddress("https://localhost:5072");

        var transactionDto = await _client.AddTransactionRpcAsync(transactionToAdd);

        return transactionDto.MapToDomain();
    }

    //public async Task<IReadOnlyCollection<OnlineShopWeb.Domain.Transaction>> GetTransactionsRpc(string userId)
    //{
    //    var getTransactionsListRequest = new GrpcTestService.Contracts.GetTransactionsRequest();
    //    getTransactionsListRequest.UserId = userId;

    //    var transactionListDto = await _client.GetTransactionsRpcAsync(getTransactionsListRequest);

    //    return transactionListDto.MapToDomain();
    //}
}
