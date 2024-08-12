namespace ApiCouponProduct.Domain.Dtos;

public class AddProductDto
{
    public int? ProductId { get; set; }
    public string Name { get; set; }
    public string Producer { get; set; }
    public ProductCategory Category { get; set; }
    public string Picture { get; set; }
    public decimal Price { get; set; }
}