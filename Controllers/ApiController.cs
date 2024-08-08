using Microsoft.AspNetCore.Mvc;

namespace AjaxDemo.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index()
        {
            string content = "<h2>Ajax, 您好</h2>";
            return Content(content,"text/plain",System.Text.Encoding.UTF8);
        }
    }
}
