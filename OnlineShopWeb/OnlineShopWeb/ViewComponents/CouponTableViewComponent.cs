using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Models;

namespace OnlineShopWeb.ViewComponents;

public class CouponTableViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(ShoppingCartModel model)
    {
        return View(model);
    }
}
