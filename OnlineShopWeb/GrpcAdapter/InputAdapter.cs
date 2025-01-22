namespace GrpcAdapter;

//For server
//using GRPC contracts
//public class InputAdapter : GrpcTestService.Contracts.AddTransactionService.AddTransactionServiceBase
//{
//    //
//    //public AddTransactionService
//    public override async Task<Transaction> AddTransactionRpc(AddTransaction request, ServerCallContext context)
//    {
//        //handler call

//        return new Transaction
//        {

//        };
//    }
//}


//public class InputAdapter
//{
//    private readonly GrpcTestService.Contracts.AddTransactionService.AddTransactionServiceClient _client;
//    public InputAdapter(GrpcTestService.Contracts.AddTransactionService.AddTransactionServiceClient client)
//    {
//        _client = client;
//    }

//    public async Task<OnlineShopWeb.Domain.Transaction> AddTransactionRpc(string userId,
//        List<ProductInCartDto> productInCartDtoList,
//        List<TransactionToCouponsDto>? couponDtoList)
//    {
//        //handler call^^
//        var transactionDto = await _client.AddTransactionRpcAsync(userId, productInCartDtoList,
//            couponDtoList);

//        return transactionDomain
//    }
//}