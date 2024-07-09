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
        [HttpGet]
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

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var saha = sahalar.FirstOrDefault(s => s.Id == id);
            if (saha != null)
            {
                sahalar.Remove(saha);
            }
            return View("Index");
        }

        // GET: Sahalar/Edit/5
        [HttpGet]
        public IActionResult Update(int id)
        {
            var saha = sahalar.FirstOrDefault(s => s.Id == id);
            if (saha == null)
            {
                return NotFound();
            }
            return View(saha);
        }

        // POST: Sahalar/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, [Bind("Id,Name")] SahalarModel saha)
        {
            if (id != saha.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var target = sahalar.FirstOrDefault(s => s.Id == saha.Id);
                    if (target != null)
                    {
                        target.Name = saha.Name;
                        // Burada veritabanı güncelleme işlemi gerçekleşecek
                    }
                }
                catch (Exception ex)
                {
                    // Hata yönetimi
                    ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu: " + ex.Message);
                    return View(saha);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(saha);
        }


    }
}

