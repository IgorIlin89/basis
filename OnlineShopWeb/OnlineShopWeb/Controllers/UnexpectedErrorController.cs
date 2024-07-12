using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Models;

namespace OnlineShopWeb.Controllers
{
    public class UnexpectedErrorController : Controller
    {
        //private readonly HttpContext _httpContext;
        //public UnexpectedErrorController(HttpContext context)
        //{
        //    _httpContext = context;
        //}

        [Route("unexpectederror")]
        public IActionResult Index()
        {
            //HttpContext.
            return View(new ExceptionModel
            {
                Exception = TempData["ExceptionFromMiddleware"] as Exception
            });
            //return View(new ExceptionModel
            //{
            //    Exception = TempData["ExceptionFromMiddleware"] as Exception
            //});
        }
    }
}
