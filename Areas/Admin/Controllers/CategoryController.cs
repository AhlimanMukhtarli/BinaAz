
using BinaAz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using BinaAz.Models;


namespace Binaz.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult GetCategory()
        {
            return View(_context.Categories.ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("GetCategory");
        }
        public IActionResult Delete(int id)
        {
            var c = _context.Categories.Find(id);
            if (c != null)
            {
                _context.Categories.Remove(c);
            }
            _context.SaveChanges();
            return RedirectToAction("GetCategory");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View(_context.Categories.Find(id));
        }
        [HttpPost]
        public IActionResult Edit(Category newC)
        {
            var c = _context.Categories.Find(newC.Id);
            if (c != null)
            {
                c.Name = newC.Name;
                c.ParentId = newC.ParentId;
            }
            _context.SaveChanges();
            return RedirectToAction("GetCategory");
        }
        public IActionResult Activation(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                if (category.IsActive)
                {
                    category.IsActive = false;
                }
                else
                {
                    category.IsActive = true;
                }
                _context.SaveChanges();
            }
            return RedirectToAction("GetCategory");
        }
    }
}
