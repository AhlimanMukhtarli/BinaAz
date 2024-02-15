using ClothingShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClothingShop.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class SliderController : Controller
    {
        private readonly MyDbContext _context;

        public SliderController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Slider()
        {
            return View();
        }
        [NonAction]
        public JsonResult GetSLider()
        {
            var sliders = _context.Sliders.ToList();
            return Json(sliders);
        }
    }
}
