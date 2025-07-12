using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Newtonsoft.Json.Linq;

namespace MVC4Fun.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string number)
        {
            int i = int.Parse(number);
            ViewData["Result"] = $"Result: {i * i}";
            return View();
        }
    }
}
