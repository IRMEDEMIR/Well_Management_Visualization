using Microsoft.AspNetCore.Mvc;
using ParsingProjectMVC.Models;

namespace ParsingProjectMVC.Controllers
{
    public class KuyuGruplariController : Controller
    {
        private List<KuyuGrubuModel> kuyugruplari = new List<KuyuGrubuModel>
        {
            // Örnek veriler
            //new KuyuGrubuModel { KuyuGrubuAdi = "Kuyugrup 1" },
            //new KuyuGrubuModel { KuyuGrubuAdi = "Kuyugrup 2" },
            //new KuyuGrubuModel { KuyuGrubuAdi = "Kuyugrup 3" },
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
