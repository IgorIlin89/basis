using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;

namespace OnlineShopWeb.Controllers
{
    public class UserListController : Controller
    {
        private readonly IUserService _userService;
        public UserListController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new UserListModel
            {
                UserList = _userService.GetUserList()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return RedirectToAction("Index", "UserList");
        }
    }
}