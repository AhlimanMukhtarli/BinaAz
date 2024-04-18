using BinaAz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BinaAz.Controllers
{
	public class ListingsController : Controller
	{
		private readonly AppDbContext _context;
		public ListingsController(AppDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			ViewBag.Categories = _context.Categories.ToList();
			return View(_context.Products.Include(x => x.Category).ToList());
		}
	}
}