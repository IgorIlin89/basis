using Microsoft.Extensions.Options;
using OnlineShopWeb.Domain;
using ProductCouponAdapter.DTOs;
using ProductCouponAdapter.Mapping;
using Utility.Misc;
using Utility.Misc.Options;

namespace ProductCouponAdapter;
//TODO
// In adapter mapping output to domain
//commandhandler needded in application
public class ProductCouponAdapter : IProductCouponAdapter
{
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly string _apiUrl;
    public ProductCouponAdapter(IHttpClientWrapper httpClientWrapper,
        IOptions<ApiCouponProductOptions> options)
    {
        _httpClientWrapper = httpClientWrapper;
        //TODO check for null
        _apiUrl = options.Value.ApiUrl;
    }

    public async Task<List<Product>> GetProductList()
    {
        var received = await _httpClientWrapper.Get<List<ProductDto>>(_apiUrl, "product", "list");
        return received.MapToList();
    }

    public async Task<List<Coupon>> GetCouponList()
    {
        var received = await _httpClientWrapper.Get<List<CouponDto>>(_apiUrl, "coupon", "list");
        return received.MapToCouponList();
    }

    public async void ProductDelete(string id)
    {
        _httpClientWrapper.Delete(_apiUrl, "product", id);
    }

    public async void CouponDelete(string id)
    {
        _httpClientWrapper.Delete(_apiUrl, "coupon", id);
    }

    public async Task<Product> GetProductById(string id)
    {
        var received = await _httpClientWrapper.Get<ProductDto>(_apiUrl, "product", id);
        return received.MapToProduct();
    }

    public async Task<Coupon> GetCouponById(string id)
    {
        var received = await _httpClientWrapper.Get<CouponDto>(_apiUrl, "coupon", id);
        return received.MapToDomain();
    }

    public async Task<Coupon> GetCouponByCode(string couponCode)
    {
        var received = await _httpClientWrapper.Get<CouponDto>(_apiUrl, "coupon", "code", couponCode);
        return received.MapToDomain();
    }

    public async Task<Product> ProductUpdate(Product product)
    {
        var received = await _httpClientWrapper.Put<ProductDto, ProductDto>(_apiUrl, "product", product.MapToDto());
        return received.MapToProduct();
    }

    public async Task<Coupon> CouponUpdate(Coupon coupon)
    {
        var received = await _httpClientWrapper.Put<CouponDto, CouponDto>(_apiUrl, "coupon", coupon.MapToDto());
        return received.MapToDomain();
    }

    public async Task<Product> ProductAdd(Product product)
    {
        var received = await _httpClientWrapper.Post<ProductDto, ProductDto>(_apiUrl, "product", product.MapToDto());
        return received.MapToProduct();
    }

    public async Task<Coupon> CouponAdd(Coupon coupon)
    {
        var received = await _httpClientWrapper.Post<CouponDto, CouponDto>(_apiUrl, "coupon", coupon.MapToDto());
        return received.MapToDomain();
    }
}
