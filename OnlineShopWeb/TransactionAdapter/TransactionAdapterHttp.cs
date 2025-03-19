using Microsoft.Extensions.Options;
using OnlineShopWeb.Domain;
using TransactionAdapter.DTOs;
using TransactionAdapter.Mapping;
using Utility.Misc;
using Utility.Misc.Options;

namespace TransactionAdapter;

public class TransactionAdapterHttp : ITransactionAdapter
{
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly string _apiUrl;
    public TransactionAdapterHttp(IHttpClientWrapper httpClientWrapper,
        IOptions<ApiTransactionOptions> options)
    {
        _httpClientWrapper = httpClientWrapper;
        _apiUrl = options.Value.ApiUrl;
    }

    public async Task<IReadOnlyCollection<Transaction>> GetTransactionList(string id)
    {
        var received = await _httpClientWrapper.Get<IReadOnlyCollection<TransactionDto>>(_apiUrl, "transaction", "list", id);
        return received.MapToDomain();
    }

    public async Task<Transaction> AddTransaction(string userId, IReadOnlyCollection<ProductInCart> productInCartList,
        IReadOnlyCollection<TransactionCoupon> couponList)
    {
        var received = await _httpClientWrapper.Post<object, TransactionDto>(_apiUrl, "transaction",
            new
            {
                UserId = userId,
                AddProductsInCartDto = productInCartList.MapToDto(),
                AddCouponsDto = couponList.MapToDto()
            });

        return received.MapToDomain();
    }
}