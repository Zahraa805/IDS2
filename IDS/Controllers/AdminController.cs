using Microsoft.AspNetCore.Mvc;

namespace IDS.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string load)
        {
            ViewBag.LoadPartial = load;
            return View();
        }

    }
}
