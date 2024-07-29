using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsingProjectMVC.Models;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParsingProjectMVC.Controllers
{
    public class KuyularController : Controller
    {
        private readonly Services.ParsingDbContext _context;

        public KuyularController(Services.ParsingDbContext context)
        {
            _context = context;
        }

        // Listeleme işlemi
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 50)
        {
            var pagedKuyular = await _context.Kuyu
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalItems = await _context.Kuyu.CountAsync();

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            return View(pagedKuyular);
        }

        // Yeni kuyu ekleme işlemi
        [HttpPost]
        public async Task<IActionResult> Create(string kuyuAdi, string enlem, string boylam, int pageNumber = 1, int pageSize = 50)
        {
            var regex = new Regex(@"^([A-Z]+(\s[A-Z]+)*)-\d+(\/K\d*)?$");

            var kuyular = await _context.Kuyu.ToListAsync();
            bool isExisting = kuyular.Any(k => k.KuyuAdi.Equals(kuyuAdi, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(kuyuAdi) && regex.IsMatch(kuyuAdi) && !isExisting && double.TryParse(enlem, out double parsedEnlem) && double.TryParse(boylam, out double parsedBoylam))
            {
                var newKuyu = new KuyuModel
                {
                    KuyuAdi = kuyuAdi,
                    Enlem = parsedEnlem.ToString(),
                    Boylam = parsedBoylam.ToString()
                };
                _context.Kuyu.Add(newKuyu);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Kuyu başarıyla eklendi!";
                return RedirectToAction("Index", new { pageNumber, pageSize });
            }
            else
            {
                if (isExisting)
                {
                    TempData["ErrorMessage"] = "Bu kuyu adı zaten mevcut.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Kuyu adı veya koordinat formatınız doğru değil.";
                }
                return RedirectToAction("Index", new { pageNumber, pageSize });
            }
        }

        // Kuyu silme işlemi
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int pageNumber = 1, int pageSize = 50)
        {
            var kuyu = await _context.Kuyu.FindAsync(id);
            if (kuyu != null)
            {
                _context.Kuyu.Remove(kuyu);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Kuyu başarıyla silindi!";
            }
            else
            {
                TempData["ErrorMessage"] = "Kuyu bulunamadı.";
            }

            return RedirectToAction("Index", new { pageNumber, pageSize });
        }

        // Kuyu güncelleme sayfasını getir
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var kuyu = await _context.Kuyu.FindAsync(id);
            if (kuyu == null)
            {
                return NotFound();
            }
            return View(kuyu);
        }

        // Kuyu güncelleme işlemi
        [HttpPost]
        public async Task<IActionResult> Update(int id, KuyuModel updatedKuyu, int pageNumber = 1, int pageSize = 50)
        {
            var regex = new Regex(@"^([A-Z]+(\s[A-Z]+)*)-\d+(\/K\d*)?$");

            var kuyu = await _context.Kuyu.FindAsync(id);

            if (kuyu == null)
            {
                TempData["ErrorMessage"] = "Güncellenecek Kuyu bulunamadı.";
                return RedirectToAction("Index", new { pageNumber, pageSize });
            }

            if (!regex.IsMatch(updatedKuyu.KuyuAdi))
            {
                TempData["ErrorMessage"] = "Kuyu adı formatınız doğru değil.";
                return RedirectToAction("Index", new { pageNumber, pageSize });
            }

            var kuyular = await _context.Kuyu.ToListAsync();
            bool isExisting = kuyular.Any(k => k.KuyuAdi.Equals(updatedKuyu.KuyuAdi, StringComparison.OrdinalIgnoreCase) && k.Id != id);

            if (isExisting)
            {
                TempData["ErrorMessage"] = "Bu Kuyu adı kullanılmakta.";
                return RedirectToAction("Index", new { pageNumber, pageSize });
            }

            kuyu.KuyuAdi = updatedKuyu.KuyuAdi;
            kuyu.Enlem = updatedKuyu.Enlem;
            kuyu.Boylam = updatedKuyu.Boylam;
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Kuyu başarıyla güncellendi!";
            return RedirectToAction("Index", new { pageNumber, pageSize });
        }
    }
}
