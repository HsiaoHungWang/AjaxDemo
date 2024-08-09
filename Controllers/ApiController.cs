using AjaxDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;

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
            //暫停五秒
            //System.Threading.Thread.Sleep(5000);

            string content = "Ajax, 您好";
            return Content(content,"text/plain",System.Text.Encoding.UTF8);
        }

        public IActionResult Cities() {
           var cities = _context.Addresses.Select(a=>a.City).Distinct();
           return Json(cities);
        }

        //todo 根據 city 讀出鄉鎮區(site_id)的資料

        //todo 根據 site_id 讀出路名(road) 

        //http://...../api/avatar/3
        public IActionResult Avatar(int id=1) {
            var member = _context.Members.Find(id);
            if (member != null)
            {
                byte[] img = member.FileData;
                return File(img, "image/jpeg");
            }
            return NotFound();

        }

        //public IActionResult Register(string userName, string userEmail, int userAge = 20) {
          public IActionResult Register(UserDTO _user)
            {
             if (string.IsNullOrEmpty(_user.userName))
            {
                _user.userName = "Guest";
            }
            string info = $"{_user.userPhoto.FileName}-{_user.userPhoto.Length}-{_user.userPhoto.ContentType}";

            //return Content($"{_user.userName} - {_user.userEmail} - {_user.userAge}", "text/plain");
            return Content(info,"text/plain",System.Text.Encoding.UTF8);
        }

    }
}
