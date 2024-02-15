using ClothingShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClothingShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly MyDbContext _context;
        public CategoryController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Category()
        {
            return View(_context.Categories.ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Category");
        }
        public IActionResult Delete(int id)
        {
            var c = _context.Categories.Find(id);
            if (c != null)
            {
                _context.Categories.Remove(c);
            }
            _context.SaveChanges();
            return RedirectToAction("Category");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_context.Blogs.Find(id));
        }
        [HttpPost]
        public IActionResult Edit(Category newC)
        {
            var c = _context.Categories.Find(newC.Id);
            if (c != null)
            {
                c.Name = newC.Name;
            }
            _context.SaveChanges();
            return RedirectToAction("Category");
        }
    }
}
