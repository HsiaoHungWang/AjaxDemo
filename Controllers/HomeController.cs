using AjaxDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AjaxDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly mydbContext _context;
     
        public HomeController(ILogger<HomeController> logger, mydbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult JsonTest()
        {
            return View();
        }

        public IActionResult First()
        {
            var categories = _context.Categories;
            return View(categories);
        }

        public IActionResult CallAPI() {

            return View();
        }

        public IActionResult Address()
        {
            return View();
        }
        public IActionResult Show()
        {
            return View();
        }
        public IActionResult register()
        {
            return View();
        }

        public IActionResult travel() {
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
