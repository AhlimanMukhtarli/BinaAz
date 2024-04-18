using BinaAz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BinaAz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SinglePost()
        {
            return View();
        }

        public IActionResult Contact() {
            return View();
        }

        public IActionResult Agents()
        {
            return View();
        }
        public IActionResult Listings()
        {
            return View();
        }
        public IActionResult PropertyDetalist()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}