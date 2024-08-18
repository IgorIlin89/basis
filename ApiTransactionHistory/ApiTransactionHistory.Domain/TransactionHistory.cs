namespace ApiTransactionHistory.Domain;

public class TransactionHistory
{
    public int Id { get; set; }
    //public int Id { get; private init; }
    //public int Id { get; private set; }
    public int? TransactionHistoryToCouponsId { get; set; }
    public int UserId { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
    public decimal? FinalPrice { get; private set; }
    public TransactionHistoryToCoupons? Coupons { get; set; }

    //When using ICollection, EF Core give an Error
    public List<ProductInCart> ProductsInCart { get; set; }

    //Make Enum of TypeOfDiscount´, make sure it cant get values that are not allowed

    //TODO One To Many with Coupons look into
    //public IReadOnlyCollection<ProductInCart> ProductsInCart { get; set; }
    //public IReadOnlyCollection<ProductInCart> ProductsInCart { get; set; }

    //private TransactionHistory()
    //{

    //}

    //public TransactionHistory CreateObject(int UserId)
    //{
    //    var result = new TransactionHistory
    //    {
    //        Id = 1,
    //        UserId = 3,
    //        ProductsInCart = new List<ProductInCart>()
    //    };
    //    //TransactionHistory without Products in cart no sense, without price 
    //    result.ProductsInCart.Add(new ProductInCart
    //    {

    //    });
    //}
    internal void CalculateFinalPrice()
    {
        decimal result = new();

        //MappingTransactionHistory.AccessAuthorization(this);

        foreach (var element in ProductsInCart)
        {
            result += element.PricePerProduct * element.Count;
        }

        //TODO Coupons implementation with data from OnlineshopWeb

        FinalPrice = result;
    }

    //public void CalculateFinalPrice(AddTransactionHistoryCommandHandler handler)
    //{
    //    //typeof (AddTransactionHistoryCommandHandler)
    //}
}
