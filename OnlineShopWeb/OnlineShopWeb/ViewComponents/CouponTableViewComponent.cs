using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Models;
namespace OnlineShopWeb.ViewComponents;

public class CouponTableViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(ShoppingCartListModel model)
    {
        return View(model);
    }
}
