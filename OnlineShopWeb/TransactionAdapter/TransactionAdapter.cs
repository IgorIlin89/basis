using OnlineShopWeb.Domain;
using OnlineShopWeb.Domain.Interfaces;
using TransactionAdapter.DTOs;
using TransactionAdapter.Mapping;
using Utility.Misc;
using Utility.Misc.Options;

namespace TransactionAdapter;

internal class TransactionAdapter : ITransactionAdapter
{
    private readonly IHttpClientWrapper _httpClientWrapper;
    private (string ApiUrl, string ApiKey) _connectionData;

    public TransactionAdapter(IHttpClientWrapper httpClientWrapper,
        ApiTransactionOptions options)
    {
        _httpClientWrapper = httpClientWrapper;
        _connectionData.ApiUrl = options.ApiUrl;
        _connectionData.ApiKey = options.ApiKey;
    }

    public async Task<IReadOnlyCollection<Transaction>> GetTransactionList(string id)
    {
        var received = await _httpClientWrapper.Get<IReadOnlyCollection<TransactionDto>>(_connectionData, "transaction", "list", id);
        return received.MapToDomain();
    }

    public async Task<Transaction> AddTransaction(string userId, IReadOnlyCollection<ProductInCart> productInCartList,
        IReadOnlyCollection<TransactionCoupon> couponList)
    {
        var received = await _httpClientWrapper.Post<object, TransactionDto>(_connectionData, "transaction",
            new
            {
                UserId = userId,
                AddProductsInCartDto = productInCartList.MapToDto(),
                AddCouponsDto = couponList.MapToDto()
            });

        return received.MapToDomain();
    }
}