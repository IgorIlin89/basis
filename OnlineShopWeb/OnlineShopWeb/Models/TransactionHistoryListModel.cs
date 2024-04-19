using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Models;

public class TransactionHistoryListModel
{
    public List<TransactionHistoryModel> TransactionHistoryModelList { get; set; } = new List<TransactionHistoryModel>();

}
