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
            return View(_context.Products.Include(x => x.Category).ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product model)
        {
            
            {
                await _context.Products.AddAsync(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Image", new { id = model.Id });
            }
            // Handle invalid model state here
        }




        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.Product.Id);
                product.Name = model.Product.Name;
                product.Description = model.Product.Description;
                product.CategoryId = model.Product.CategoryId;
                product.RoomCount = model.Product.RoomCount;
                product.Area = model.Product.Area;
                product.Price = model.Product.Price;
                product.IsActive = model.Product.IsActive;

                await _context.SaveChangesAsync();

                
                await _context.SaveChangesAsync();
                return RedirectToAction("GetProduct");
            }
            else
            {
                return View(model);
            }
        }


        public IActionResult Delete(int id)
        {
            var p = _context.Products.Find(id);
            if (p != null)
            {
                _context.Products.Remove(p);
            }
            _context.SaveChanges();
            return RedirectToAction("GetProduct");
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
            return RedirectToAction("Image", new { id = p.ProductId });

        }
        [HttpGet]
        public IActionResult Attributes(int id)
        {
            var model = new AttributeViewModel
            {
                ProductId = id,
                ProductAttributes = _context.ProductAttributes.Where(x => x.ProductId == id).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddAttribute(AttributeViewModel model)
        {

            var attribute = new ProductAttribute
            {
                Name = model.productAttribute.Name,
                Value = model.productAttribute.Value,
                ProductId = model.ProductId,
                IsActive = model.productAttribute.IsActive
            };
            _context.ProductAttributes.Add(attribute);
            _context.SaveChanges();

            return RedirectToAction("Attributes", new { id = model.ProductId });

        }
        public async Task<IActionResult> DeleteAttribute(int id)
        {
            var a = await _context.ProductAttributes.FirstOrDefaultAsync(p => p.Id == id);

            if (a != null)
            {
                _context.ProductAttributes.Remove(a);
            }
            _context.SaveChanges();
            return RedirectToAction("Attributes", new { id = id });

        }
        [HttpPost]
        public IActionResult Activation(int productId, bool isActive)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                product.IsActive = isActive;
                _context.SaveChanges();
            }
            return RedirectToAction("GetProduct");
        }
    }
}

