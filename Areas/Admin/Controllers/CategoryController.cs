
using BinaAz.Models;
using Microsoft.AspNetCore.Mvc;
using BinaAz.Models;


namespace BinaAz.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult CategoryList()
        {
            return Json(_context.Categories.ToList());
        }
        [HttpPost]
        public JsonResult Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Json("Added.");
        }
        [HttpGet]
        public JsonResult Edit(int id)
        {
            var category = _context.Categories.FirstOrDefault(b => b.Id == id);
            return Json(category);
        }
        [HttpPost]
        public JsonResult Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return Json("Succesfully.");
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(b => b.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return Json("Succesfully.");
            }
            return Json("Not Found.");

        }
    }
}
