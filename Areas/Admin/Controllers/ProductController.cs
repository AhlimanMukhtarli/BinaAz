using ClothingShop.Extensions;
using ClothingShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly MyDbContext _context;
        public ProductController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Product()
        {
            return View(_context.Products.Include(x=>x.Category).ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if (FileExtensions.IsImage(product.ImgFile))
            {
                string nameImg = await FileExtensions.SaveAsync(product.ImgFile, "products");
                product.Image = nameImg;
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            else
            {
                return RedirectToAction("Add");
            }
            return RedirectToAction("Product");
        }
        public IActionResult Delete(int id)
        {
            var p = _context.Products.Find(id);
            if (p != null)
            {
                _context.Products.Remove(p);
            }
            _context.SaveChanges();
            return RedirectToAction("Product");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View(_context.Products.Find(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product newP)
        {
            var p = _context.Products.Find(newP.Id);
            if (p != null)
            {
                if (FileExtensions.IsImage(newP.ImgFile))
                {
                    string nameImg = await FileExtensions.SaveAsync(newP.ImgFile, "products");
                    p.ImgFile = newP.ImgFile;
                    p.Image = nameImg;
                    p.Name=newP.Name; 
                    p.Price= newP.Price;
                    _context.SaveChanges();
                    return RedirectToAction("Product");
                }
                else
                {
                    return RedirectToAction("Edit");
                }
            }
            return RedirectToAction("Edit");
        }
    }
}
