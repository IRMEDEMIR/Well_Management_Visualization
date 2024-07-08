using Microsoft.AspNetCore.Mvc;
using ParsingProjectMVC.Models;

namespace ParsingProjectMVC.Controllers
{
    public class SahalarController : Controller
    {
        private List<SahalarModel> sahalar = new List<SahalarModel>
        {
            // Örnek veriler
            new SahalarModel { Id = 1, Name = "Saha 1" },
            new SahalarModel { Id = 2, Name = "Saha 2" },
            new SahalarModel { Id = 3, Name = "Saha 3" },
            // Daha fazla örnek veri ekleyin...
        };

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedSahalar = sahalar
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalItems = sahalar.Count;

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            return View(pagedSahalar);
        }
    }
}

