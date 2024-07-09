using Microsoft.AspNetCore.Mvc;
using ParsingProjectMVC.Models;

namespace ParsingProjectMVC.Controllers
{
    public class WellborelarController : Controller
    {
        private List<WellboreModel> wellborelar = new List<WellboreModel>
        {
            // Örnek veriler
            //new WellborelarModel { Id = 1, Name = "Wellbore 1" },
            //new WellborelarModel { Id = 2, Name = "Wellbore 2" },
            //new WellborelarModel { Id = 3, Name = "Wellbore 3" },
            // Daha fazla örnek veri ekleyin...
        };

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedWellborelar = wellborelar
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalItems = wellborelar.Count;

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            return View(pagedWellborelar);
        }
    }
}
