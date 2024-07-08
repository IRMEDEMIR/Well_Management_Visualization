using Microsoft.AspNetCore.Mvc;

namespace ParsingProjectMVC.Controllers
{
    public class KuyularController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
