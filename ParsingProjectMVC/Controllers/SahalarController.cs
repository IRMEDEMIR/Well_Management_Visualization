using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsingProjectMVC.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParsingProjectMVC.Controllers
{
    public class SahalarController : Controller
    {
        private readonly Services.ParsingDbContext _context;

        public SahalarController(Services.ParsingDbContext context)
        {
            _context = context;
        }

        // Listeleme işlemi
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 50)
        {
            var pagedSahalar = await _context.Saha
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalItems = await _context.Saha.CountAsync();

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            return View(pagedSahalar);
        }

        // Yeni saha ekleme işlemi
        [HttpPost]
        public async Task<IActionResult> Create(string sahaAdi, int pageNumber = 1, int pageSize = 50)
        {
            var regex = new Regex(@"^[A-Z\s]+$");

            // Client-side evaluation (ToList ile veriyi çekip öyle sorgulama)
            var sahalar = await _context.Saha.ToListAsync();
            bool isExisting = sahalar.Any(s => s.SahaAdi.Equals(sahaAdi, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(sahaAdi) && regex.IsMatch(sahaAdi) && !isExisting)
            {
                var newSaha = new SahaModel
                {
                    SahaAdi = sahaAdi
                };
                _context.Saha.Add(newSaha);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Saha başarıyla eklendi!";
            }
            else
            {
                if (isExisting)
                {
                    TempData["ErrorMessage"] = "Bu saha adı zaten mevcut.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Saha adı yalnızca büyük harf ve boşluk içermelidir.";
                }
            }
            return RedirectToAction("Index", new { pageNumber, pageSize });
        }

        // Saha silme işlemi
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int pageNumber = 1, int pageSize = 50)
        {
            var saha = await _context.Saha.FindAsync(id);
            if (saha != null)
            {
                _context.Saha.Remove(saha);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Saha başarıyla silindi!";
            }
            else
            {
                TempData["ErrorMessage"] = "Saha bulunamadı.";
            }

            return RedirectToAction("Index", new { pageNumber, pageSize });
        }

        // Saha güncelleme sayfasını getir
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var saha = await _context.Saha.FindAsync(id);
            if (saha == null)
            {
                return NotFound();
            }
            return View(saha);
        }

        // Saha güncelleme işlemi
        [HttpPost]
        public async Task<IActionResult> Update(int id, SahaModel updatedSaha, int pageNumber = 1, int pageSize = 50)
        {
            var regex = new Regex(@"^[A-Z\s]+$");
            var saha = await _context.Saha.FindAsync(id);

            if (saha == null)
            {
                TempData["ErrorMessage"] = "Güncellenecek saha bulunamadı.";
                return RedirectToAction("Index", new { pageNumber, pageSize });
            }

            // Client-side evaluation (ToList ile veriyi çekip öyle sorgulama)
            var sahalar = await _context.Saha.ToListAsync();
            bool isExisting = sahalar.Any(s => s.SahaAdi.Equals(updatedSaha.SahaAdi, StringComparison.OrdinalIgnoreCase) && s.Id != id);

            if (!string.IsNullOrEmpty(updatedSaha.SahaAdi) && regex.IsMatch(updatedSaha.SahaAdi) && !isExisting)
            {
                saha.SahaAdi = updatedSaha.SahaAdi;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Saha başarıyla güncellendi!";
            }
            else
            {
                if (isExisting)
                {
                    TempData["ErrorMessage"] = "Bu saha adı kullanılmakta.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Saha adı yalnızca büyük harf ve boşluk içermelidir.";
                }
            }

            return RedirectToAction("Index", new { pageNumber, pageSize });
        }
    }
}
