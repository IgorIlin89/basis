using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWeb.Controllers
{
    public class CouponListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
