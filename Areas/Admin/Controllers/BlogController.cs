using ClothingShop.Extensions;
using ClothingShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly MyDbContext _context;
        public BlogController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Blog()
        {
            return View(_context.Blogs.ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Blog blog)
        {
            if (FileExtensions.IsImage(blog.ImgFile))
            {
                string nameImg = await FileExtensions.SaveAsync(blog.ImgFile, "blog");
                blog.Image = nameImg;
                blog.DateTime = DateTime.Now;
                _context.Blogs.Add(blog);
                _context.SaveChanges();
            }
            else
            {
                return RedirectToAction("Add");
            }
            return RedirectToAction("Blog");
        }

        public IActionResult Delete(int id)
        {
            var blog = _context.Blogs.Find(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
            }
            _context.SaveChanges();
            return RedirectToAction("Blog");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_context.Blogs.Find(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Blog newBlog)
        {
            var blog = _context.Blogs.Find(newBlog.Id);
            if (blog != null)
            {
                if (FileExtensions.IsImage(newBlog.ImgFile))
                {
                    string nameImg = await FileExtensions.SaveAsync(newBlog.ImgFile, "blog");
                    blog.ImgFile = newBlog.ImgFile;
                    blog.Image = nameImg;
                    blog.DateTime = DateTime.Now;
                    blog.Username = newBlog.Username;
                    blog.Title = newBlog.Title;
                    blog.Context = newBlog.Context;
                    _context.SaveChanges();
                    return RedirectToAction("Blog");
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
