using Microsoft.Extensions.Options;
using OnlineShopWeb.Domain;
using TransactionAdapter.DTOs;
using TransactionAdapter.Mapping;
using Utility.Misc;
using Utility.Misc.Options;

namespace TransactionAdapter;

public class TransactionAdapter : ITransactionAdapter
{
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly string _apiUrl;
    public TransactionAdapter(IHttpClientWrapper httpClientWrapper,
        IOptions<ApiTransactionOptions> options)
    {
        _httpClientWrapper = httpClientWrapper;
        _apiUrl = options.Value.ApiUrl;
    }

    public async Task<List<Transaction>> GetTransactionList(string id)
    {
        var received = await _httpClientWrapper.Get<List<TransactionObjectsDto>>(_apiUrl, "transaction", "list", id);
        return received.MapToTransaction();
    }

    public async Task<Transaction> AddTransaction(string userId, List<ProductInCartDto> productInCartDtoList,
        List<TransactionToCouponsDto>? couponDtoList)
    {
        var received = await _httpClientWrapper.Post<object, TransactionObjectsDto>(_apiUrl, "transaction",
            new
            {
                UserId = userId,
                AddProductsInCartDto = productInCartDtoList,
                AddCouponsDto = couponDtoList
            });

        return received.MapToTransaction();
    }
}