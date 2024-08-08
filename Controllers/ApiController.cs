using AjaxDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace AjaxDemo.Controllers
{
    public class ApiController : Controller
    {
        private readonly mydbContext _context;

        public ApiController(mydbContext context)
        {            
            _context = context;
        }

        public IActionResult Index()
        {
            string content = "<h2>Ajax, 您好</h2>";
            return Content(content,"text/plain",System.Text.Encoding.UTF8);
        }

        public IActionResult Cities() {
           var cities = _context.Addresses.Select(a=>a.City).Distinct();
           return Json(cities);
        }

        public IActionResult Avatar(int id=1) {
            var member = _context.Members.Find(id);
            byte[] img = member.FileData;

            return File(img, "image/jpeg");
        }

    }
}
