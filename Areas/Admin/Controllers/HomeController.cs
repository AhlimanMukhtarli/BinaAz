using ClothingShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClothingShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
      
    }
}
