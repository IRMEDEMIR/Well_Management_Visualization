using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsingProjectMVC.Models;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParsingProjectMVC.Controllers
{
    public class WellborelarController : Controller
    {
        private readonly Services.ParsingDbContext _context;

        public WellborelarController(Services.ParsingDbContext context)
        {
            _context = context;
        }

        // Listeleme işlemi
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 50)
        {
            var pagedWellborelar = await _context.Wellbore
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalItems = await _context.Wellbore.CountAsync();

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            return View(pagedWellborelar);
        }

        // Yeni wellbore ekleme işlemi
        [HttpPost]
        public async Task<IActionResult> Create(string wellboreAdi, string derinlik, int pageNumber = 1, int pageSize = 50)
        {
            var regex = new Regex(@"^([A-Z]+(\s[A-Z]+)*)-\d+(\/K\d*)?(\/[SMR]\d*)*$");

            if (!string.IsNullOrEmpty(wellboreAdi) && regex.IsMatch(wellboreAdi))
            {
                var allWellbores = await _context.Wellbore.ToListAsync();
                bool isExisting = allWellbores.Any(w => w.WellboreAdi.Equals(wellboreAdi, StringComparison.OrdinalIgnoreCase));
                if (isExisting)
                {
                    TempData["ErrorMessage"] = "Bu Wellbore adı zaten mevcut.";
                    return RedirectToAction("Index", new { pageNumber, pageSize });
                }

                var newWellbore = new WellboreModel
                {
                    WellboreAdi = wellboreAdi,
                    Derinlik = derinlik
                };
                _context.Wellbore.Add(newWellbore);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Wellbore başarıyla eklendi!";
                return RedirectToAction("Index", new { pageNumber, pageSize });
            }
            else
            {
                TempData["ErrorMessage"] = "Wellbore adı formatı yanlış.";
                return RedirectToAction("Index", new { pageNumber, pageSize });
            }
        }

        // Wellbore silme işlemi
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int pageNumber = 1, int pageSize = 50)
        {
            var wellboreToDelete = await _context.Wellbore.FindAsync(id);
            if (wellboreToDelete != null)
            {
                _context.Wellbore.Remove(wellboreToDelete);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Wellbore başarıyla silindi!";
            }
            else
            {
                TempData["ErrorMessage"] = "Wellbore bulunamadı.";
            }

            return RedirectToAction("Index", new { pageNumber, pageSize });
        }

        // Wellbore güncelleme sayfasını getir
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var wellbore = await _context.Wellbore.FindAsync(id);
            if (wellbore == null)
            {
                return NotFound();
            }
            return View(wellbore);
        }

        // Wellbore güncelleme işlemi
        [HttpPost]
        public async Task<IActionResult> Update(int id, WellboreModel updatedWellbore, int pageNumber = 1, int pageSize = 50)
        {
            var regex = new Regex(@"^([A-Z]+(\s[A-Z]+)*)-\d+(\/K\d*)?(\/[SMR]\d*)*$");

            var wellbore = await _context.Wellbore.FindAsync(id);

            if (wellbore == null)
            {
                TempData["ErrorMessage"] = "Güncellenecek Wellbore bulunamadı.";
                return RedirectToAction("Index", new { pageNumber, pageSize });
            }

            if (!regex.IsMatch(updatedWellbore.WellboreAdi))
            {
                TempData["ErrorMessage"] = "Wellbore adı formatı yanlış.";
                return RedirectToAction("Index", new { pageNumber, pageSize });
            }

            var allWellbores = await _context.Wellbore.ToListAsync();
            bool isExisting = allWellbores.Any(w => w.WellboreAdi.Equals(updatedWellbore.WellboreAdi, StringComparison.OrdinalIgnoreCase) && w.Id != id);
            if (isExisting)
            {
                TempData["ErrorMessage"] = "Bu Wellbore adı kullanılmakta.";
                return RedirectToAction("Index", new { pageNumber, pageSize });
            }

            wellbore.WellboreAdi = updatedWellbore.WellboreAdi;
            wellbore.Derinlik = updatedWellbore.Derinlik;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Wellbore başarıyla güncellendi!";
            return RedirectToAction("Index", new { pageNumber, pageSize });
        }
    }
}
