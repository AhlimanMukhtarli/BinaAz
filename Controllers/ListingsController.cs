using Microsoft.AspNetCore.Mvc;

namespace BinaAz.Controllers
{
    public class ListingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
