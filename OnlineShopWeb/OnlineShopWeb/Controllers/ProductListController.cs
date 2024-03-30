using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWeb.Controllers
{
    public class ProductListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
