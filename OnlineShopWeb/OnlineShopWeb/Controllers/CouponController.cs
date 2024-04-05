using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWeb.Controllers
{
    public class CouponController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
