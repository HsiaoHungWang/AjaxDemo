using AjaxDemo.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using AjaxDemo.Models.DTO;
//using AspNetCore;


namespace AjaxDemo.Controllers
{
    public class ApiController : Controller
    {
        private readonly mydbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ApiController(mydbContext context, IWebHostEnvironment webHostEnvironment)
        {            
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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


            //實際路徑 C:\Shared\012_Ajax\workspace\AjaxDemo\wwwroot\uploads\abc.jpg
            //string strPath = @"C:\Shared\012_Ajax\workspace\AjaxDemo\wwwroot\uploads\abc.jpg";
            //string strPath = _webHostEnvironment.WebRootPath;
            //C:\Shared\012_Ajax\workspace\AjaxDemo\wwwroot
            //string strPath = _webHostEnvironment.ContentRootPath;
            //C:\Shared\012_Ajax\workspace\AjaxDemo


            //檔案上傳
            string strPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", _user.userPhoto.FileName);

            using (var fileStream = new FileStream(strPath, FileMode.Create))
            {
                _user.userPhoto.CopyTo(fileStream);
            }

            //寫進資料庫
            Member member = new Member();
            member.Name = _user.userName;
            member.Email = _user.userEmail;
            member.Age = _user.userAge;
            member.FileName = _user.userPhoto.FileName;

            //將上傳的檔案轉成二進位
            byte[] imgByte = null;
            using(var memorySteam = new MemoryStream())
            {
                _user.userPhoto.CopyTo(memorySteam);
                imgByte = memorySteam.ToArray();

            }
            member.FileData = imgByte;

            _context.Members.Add(member);
            _context.SaveChanges();






            //return Content($"{_user.userName} - {_user.userEmail} - {_user.userAge}", "text/plain");
            //return Content(info,"text/plain",System.Text.Encoding.UTF8);
            return Content(strPath);

        }

       [HttpPost]
        public IActionResult Spots([FromBody]SearchDTO _searchDTO)
        {
            //根據分類編號讀取相關景點
            var spots = _searchDTO.categoryId == 0 ? _context.SpotImagesSpots : _context.SpotImagesSpots.Where(s=>s.CategoryId == _searchDTO.categoryId);

            //關鍵字搜尋
            if (!string.IsNullOrEmpty(_searchDTO.keyword))
            {
                spots = spots.Where(s => s.SpotTitle.Contains(_searchDTO.keyword) || s.SpotDescription.Contains(_searchDTO.keyword));
            }

            //根據價錢搜尋
            //根據日期區間搜尋

            //排序
            switch (_searchDTO.sortBy)
            {
                case "spotTitle":
                    spots = _searchDTO.sortType == "asc" ? spots.OrderBy(s => s.SpotTitle) : spots.OrderByDescending(s => s.SpotTitle);
                    break;
                case "categoryId":
                    spots = _searchDTO.sortType == "asc" ? spots.OrderBy(s => s.CategoryId) : spots.OrderByDescending(s => s.CategoryId);
                    break;
                default:
                    spots = _searchDTO.sortType == "asc" ? spots.OrderBy(s => s.SpotId) : spots.OrderByDescending(s => s.SpotId);
                    break;
            }



            //分頁


            return Json(spots);
        }
    }
}
