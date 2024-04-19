using BinaAz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BinaAz.Controllers
{
    public class AnnouncmentController : Controller
    {
        private readonly AppDbContext _context;

        public AnnouncmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Product/Create
        [HttpGet]

        public IActionResult Add()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Product model)
        {
            ViewBag.Categories = _context.Categories.ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _context.Products.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Listings");
        }
    }
}
