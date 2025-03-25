namespace OnlineShopWeb.Domain.Interfaces;

public interface IProductCouponAdapter
{
    Task<List<Coupon>> GetCouponList();
    Task<List<Product>> GetProductList();
    void CouponDelete(string id);
    void ProductDelete(string id);
    Task<Product> GetProductById(string id);
    Task<Coupon> GetCouponById(string id);
    Task<Coupon> GetCouponByCode(string couponCode);
    Task<Product> ProductUpdate(Product product);
    Task<Coupon> CouponUpdate(Coupon coupon);
    Task<Product> ProductAdd(Product product);
    Task<Coupon> CouponAdd(Coupon coupon);
}