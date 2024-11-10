namespace ProductCouponAdapter.DTOs;

public class ProductDto
{
    public int? ProductId { get; set; }
    public string Name { get; set; }
    public string Producer { get; set; }
    public ProductCategoryDto Category { get; set; }
    public string Picture { get; set; }
    public decimal Price { get; set; }
}
