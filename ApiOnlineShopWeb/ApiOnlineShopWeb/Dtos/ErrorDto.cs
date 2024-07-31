namespace ApiOnlineShopWeb.Dtos;

public class ErrorDto : Exception
{
    public int? StatusCode { get; set; }
    public string? Message { get; set; }
}
