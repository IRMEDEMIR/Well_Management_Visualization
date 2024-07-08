using Microsoft.AspNetCore.Mvc;
using ParsingProjectMVC.Models;

namespace ParsingProjectMVC.Controllers
{
    public class KuyuGruplariController : Controller
    {
        private List<KuyuGruplariModel> kuyugruplari = new List<KuyuGruplariModel>
        {
            // Örnek veriler
            new KuyuGruplariModel { Id = 1, Name = "Kuyugrup 1" },
            new KuyuGruplariModel { Id = 2, Name = "Kuyugrup 2" },
            new KuyuGruplariModel { Id = 3, Name = "Kuyugrup 3" },
            // Daha fazla örnek veri ekleyin...
        };

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedKuyugruplari = kuyugruplari
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalItems = kuyugruplari.Count;

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            return View(pagedKuyugruplari);
        }
    }
}
