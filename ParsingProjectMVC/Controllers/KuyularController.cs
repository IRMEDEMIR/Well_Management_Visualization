using Microsoft.AspNetCore.Mvc;
using ParsingProjectMVC.Models;

namespace ParsingProjectMVC.Controllers
{
    public class KuyularController : Controller
    {
        private List<KuyuModel> kuyular = new List<KuyuModel>
        {
            // Örnek veriler
            //new KuyularModel { Id = 1, Name = "Kuyu 1" },
            //new KuyularModel { Id = 2, Name = "Kuyu 2" },
            //new KuyularModel { Id = 3, Name = "Kuyu 3" },
            // Daha fazla örnek veri ekleyin...
        };

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedKuyular = kuyular
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalItems = kuyular.Count;

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            return View(pagedKuyular);
        }
    }
}
