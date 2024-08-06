using Microsoft.Extensions.Options;
using OnlineShopWeb.TransferObjects.Dtos;
using OnlineShopWeb.Misc;
using OnlineShopWeb.Adapters.Interfaces;

namespace OnlineShopWeb.Adapters;

public class TransactionHistoryAdapter : ITransactionHistoryAdapter
{
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly string _apiUrl;
    public TransactionHistoryAdapter(IHttpClientWrapper httpClientWrapper,
        IOptionsSnapshot<HttpClientWrapperOptions> options)
    {
        _httpClientWrapper = httpClientWrapper;
        _apiUrl = options.Get("ApiClientOptions").ApiUrl;
    }

    public async Task<List<TransactionHistoryObjectsDto>> GetTransactionHistoryList(string id)
    {
        return await _httpClientWrapper.Get<List<TransactionHistoryObjectsDto>>(_apiUrl, "transactionhistory", "list", id);
    }

    public async Task<TransactionHistoryDto> AddTransactionHistory(TransactionHistoryDto transactionHistoryDto)
    {
        return await _httpClientWrapper.Post<TransactionHistoryDto, TransactionHistoryDto>(_apiUrl, "transactionhistory",
            transactionHistoryDto);
    }
}