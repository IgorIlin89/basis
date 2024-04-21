using ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWeb.ExtensionsAndViewComponents;

public class UserNameViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userData =$"{HttpContext.GivenName()} {HttpContext.Surname()}";
        return View("Components/UserData.cshtml", userData);
    }
}
