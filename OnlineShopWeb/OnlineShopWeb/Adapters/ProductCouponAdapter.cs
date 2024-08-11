using Microsoft.Extensions.Options;
using OnlineShopWeb.Adapters.Interfaces;
using OnlineShopWeb.Misc;
using OnlineShopWeb.TransferObjects.Dtos;

namespace OnlineShopWeb.Adapters;

public class ProductCouponAdapter : IProductCouponAdapter
{
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly string _apiUrl;
    public ProductCouponAdapter(IHttpClientWrapper httpClientWrapper,
        IOptionsSnapshot<HttpClientWrapperOptions> options)
    {
        _httpClientWrapper = httpClientWrapper;
        _apiUrl = options.Get("ApiCouponProductClientOptions").ApiUrl;
    }

    public async Task<List<ProductDto>> GetProductList()
    {
        return await _httpClientWrapper.Get<List<ProductDto>>(_apiUrl, "product", "list");
    }

    public async Task<List<CouponDto>> GetCouponList()
    {
        return await _httpClientWrapper.Get<List<CouponDto>>(_apiUrl, "coupon", "list");
    }

    public async void ProductDelete(string id)
    {
        _httpClientWrapper.Delete(_apiUrl, "product", id);
    }

    public async void CouponDelete(string id)
    {
        _httpClientWrapper.Delete(_apiUrl, "coupon", id);
    }

    public async Task<ProductDto> GetProductById(string id)
    {
        return await _httpClientWrapper.Get<ProductDto>(_apiUrl, "product", id);
    }

    public async Task<CouponDto> GetCouponById(string id)
    {
        return await _httpClientWrapper.Get<CouponDto>(_apiUrl, "coupon", id);
    }

    public async Task<CouponDto> GetCouponByCode(string couponCode)
    {
        return await _httpClientWrapper.Get<CouponDto>("coupon", "code", couponCode);
    }

    public async Task<ProductDto> ProductUpdate(ProductDto productDto)
    {
        return await _httpClientWrapper.Put<ProductDto, ProductDto>(_apiUrl, "product", productDto);
    }

    public async Task<CouponDto> CouponUpdate(CouponDto couponDto)
    {
        return await _httpClientWrapper.Put<CouponDto, CouponDto>(_apiUrl, "coupon", couponDto);
    }

    public async Task<ProductDto> ProductAdd(ProductDto productDto)
    {
        return await _httpClientWrapper.Post<ProductDto, ProductDto>(_apiUrl, "product", productDto);
    }

    public async Task<CouponDto> CouponAdd(CouponDto couponDto)
    {
        return await _httpClientWrapper.Post<CouponDto, CouponDto>(_apiUrl, "coupon", couponDto);
    }
}
