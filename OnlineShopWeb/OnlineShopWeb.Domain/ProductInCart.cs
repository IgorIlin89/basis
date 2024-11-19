namespace OnlineShopWeb.Domain;

public class ProductInCart
{
    public int Id { get; set; }
    public int Count { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int TransactionId { get; set; }
    public Transaction TransactionObject { get; set; }

}
