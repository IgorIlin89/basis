using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.TransferObjects.Models.ListModels;
namespace OnlineShopWeb.ViewComponents;

public class CouponTableViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(ShoppingCartListModel model)
    {
        return View(model);
    }
}
