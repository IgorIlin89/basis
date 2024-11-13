namespace TransactionAdapter.DTOs;

public class AddProductInCartDto
{
    public int Count { get; set; }
    public int ProductId { get; set; }
    public decimal PricePerProduct { get; set; }
    public int TransactionId { get; set; }
}
