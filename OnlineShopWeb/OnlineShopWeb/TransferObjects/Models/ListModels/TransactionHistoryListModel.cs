using OnlineShopWeb.Domain;

namespace OnlineShopWeb.TransferObjects.Models.ListModels;

public class TransactionHistoryListModel
{
    public List<TransactionHistoryModel> TransactionHistoryModelList { get; set; } = new List<TransactionHistoryModel>();

}
