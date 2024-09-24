﻿using Microsoft.Extensions.Options;
using OnlineShopWeb.Adapters.Interfaces;
using OnlineShopWeb.Misc;
using OnlineShopWeb.TransferObjects.Dtos;

namespace OnlineShopWeb.Adapters;

public class TransactionAdapter : ITransactionHistoryAdapter
{
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly string _apiUrl;
    public TransactionAdapter(IHttpClientWrapper httpClientWrapper,
        IOptionsSnapshot<HttpClientWrapperOptions> options)
    {
        _httpClientWrapper = httpClientWrapper;
        _apiUrl = options.Get("ApiTransaction").ApiUrl;
    }

    public async Task<List<TransactionObjectsDto>> GetTransactionHistoryList(string id)
    {
        return await _httpClientWrapper.Get<List<TransactionObjectsDto>>(_apiUrl, "transaction", "list", id);
    }

    public async Task<AddTransactionDto> AddTransaction(AddTransactionDto transactionHistoryDto)
    {
        return await _httpClientWrapper.Post<AddTransactionDto, AddTransactionDto>(_apiUrl, "transaction",
            transactionHistoryDto);
    }
}