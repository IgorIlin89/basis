namespace ProductCouponAdapter;

public interface IProductCouponAdapter
{
    Task<List<CouponDto>> GetCouponList();
    Task<List<ProductDto>> GetProductList();
    void CouponDelete(string id);
    void ProductDelete(string id);
    Task<ProductDto> GetProductById(string id);
    Task<CouponDto> GetCouponById(string id);
    Task<CouponDto> GetCouponByCode(string couponCode);
    Task<ProductDto> ProductUpdate(ProductDto productDto);
    Task<CouponDto> CouponUpdate(CouponDto couponDto);
    Task<ProductDto> ProductAdd(ProductDto productDto);
    Task<CouponDto> CouponAdd(CouponDto couponDto);
}