using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BinaAz.Models;
using BinaAz.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using BinaAz.Extensions;

namespace BinaAz.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public IActionResult GetProduct()
        {
            var products = _context.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Product/Create
        [HttpGet]

        public IActionResult Add()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Product model)
        {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            return View(model);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CategoryId,Price,Area,Description,IsActive")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }
        [HttpGet]
        public IActionResult Image(int id)
        {
            var model = new ImageViewModel
            {
                Id = id,
                Images = _context.Images.Where(x => x.ProductId == id).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddImage(ImageViewModel model)
        {
            if (FileExtensions.IsImage(model.ImgFile))
            {
                string nameImg = await FileExtensions.SaveAsync(model.ImgFile, "products");
                var productImage = new image
                {
                    ImageUrl = nameImg,
                    ProductId = model.Id,
                    IsActive = true
                };
                _context.Images.Add(productImage);
                _context.SaveChanges();
            }
            return RedirectToAction("Image", new { id = model.Id });

        }
        public async Task<IActionResult> DeleteImage(int id)
        {
            var p = await _context.Images.FirstOrDefaultAsync(p => p.Id == id);

            if (p != null)
            {
                _context.Images.Remove(p);
            }
            _context.SaveChanges();
            return RedirectToAction("Image", new { id = id });

        }
        [HttpGet]
        //public IActionResult Attributes(int id)
        //{
        //    var model = new AttributeViewModel
        //    {
        //        ProductId = id,
        //        ProductAttributes = _context.ProductAttributes.Where(x => x.ProductId == id).ToList()
        //    };
        //    return View(model);
        //}

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

    }

}
