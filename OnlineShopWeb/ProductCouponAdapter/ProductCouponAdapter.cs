using OnlineShopWeb.Domain;
using OnlineShopWeb.Domain.Interfaces;
using ProductCouponAdapter.DTOs;
using ProductCouponAdapter.Mapping;
using Utility.Misc;
using Utility.Misc.Options;

namespace ProductCouponAdapter;

internal class ProductCouponAdapter : IProductCouponAdapter
{
    private readonly IHttpClientWrapper _httpClientWrapper;
    private (string ApiUrl, string ApiKey) _connectionData;

    public ProductCouponAdapter(IHttpClientWrapper httpClientWrapper,
        ApiCouponProductOptions options)
    {
        _httpClientWrapper = httpClientWrapper;
        _connectionData.ApiUrl = options.ApiUrl;
        _connectionData.ApiKey = options.ApiKey;
    }

    public async Task<List<Product>> GetProductList()
    {
        var received = await _httpClientWrapper.Get<List<ProductDto>>(_connectionData, "product", "list");
        return received.MapToList();
    }

    public async Task<List<Coupon>> GetCouponList()
    {
        var received = await _httpClientWrapper.Get<List<CouponDto>>(_connectionData, "coupon", "list");
        return received.MapToCouponList();
    }

    public async void ProductDelete(string id)
    {
        _httpClientWrapper.Delete(_connectionData, "product", id);
    }

    public async void CouponDelete(string id)
    {
        _httpClientWrapper.Delete(_connectionData, "coupon", id);
    }

    public async Task<Product> GetProductById(string id)
    {
        var received = await _httpClientWrapper.Get<ProductDto>(_connectionData, "product", id);
        return received.MapToProduct();
    }

    public async Task<Coupon> GetCouponById(string id)
    {
        var received = await _httpClientWrapper.Get<CouponDto>(_connectionData, "coupon", id);
        return received.MapToDomain();
    }

    public async Task<Coupon> GetCouponByCode(string couponCode)
    {
        var received = await _httpClientWrapper.Get<CouponDto>(_connectionData, "coupon", "code", couponCode);
        return received.MapToDomain();
    }

    public async Task<Product> ProductUpdate(Product product)
    {
        var received = await _httpClientWrapper.Put<ProductDto, ProductDto>(_connectionData, "product", product.MapToDto());
        return received.MapToProduct();
    }

    public async Task<Coupon> CouponUpdate(Coupon coupon)
    {
        var received = await _httpClientWrapper.Put<CouponDto, CouponDto>(_connectionData, "coupon", coupon.MapToDto());
        return received.MapToDomain();
    }

    public async Task<Product> ProductAdd(Product product)
    {
        var received = await _httpClientWrapper.Post<ProductDto, ProductDto>(_connectionData, "product", product.MapToDto());
        return received.MapToProduct();
    }

    public async Task<Coupon> CouponAdd(Coupon coupon)
    {
        var received = await _httpClientWrapper.Post<CouponDto, CouponDto>(_connectionData, "coupon", coupon.MapToDto());
        return received.MapToDomain();
    }
}
