namespace ApiCouponProduct.Domain.Dtos;

public class ErrorDto
{
    public ErrorStatusCode StatusCode { get; set; }
    public string? Message { get; set; }
}
