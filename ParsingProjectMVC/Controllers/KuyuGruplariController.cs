using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParsingProjectMVC.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParsingProjectMVC.Controllers
{
    public class KuyuGruplariController : Controller
    {
        private readonly Services.ParsingDbContext _context;
        public KuyuGruplariController(Services.ParsingDbContext context)
        {
            _context = context;
        }

        // listeleme
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1,  int pageSize = 50)
        {
            var pagedKuyuGruplari =  await _context.KuyuGrubu
                .Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalItems = await _context.KuyuGrubu.CountAsync();
            
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;    
            ViewBag.TotalItems = totalItems;

            return View(pagedKuyuGruplari);
        }

        //yeni kuyu grubu ekleme
        [HttpPost]
        public async Task<IActionResult> Create(string kuyuGrubuAdi, int pageNumber = 1, int  pageSize = 50)
        {
            var regex = new Regex(@"^([A-Z]+(\s[A-Z]+)*)-\d+$");

            //client-side evaluation (toList ile veriyi çekip öyle sorgulama)
            var kuyuGruplari = await _context.KuyuGrubu.ToListAsync();
            bool isExisting = kuyuGruplari.Any(k => k.KuyuGrubuAdi.Equals(kuyuGrubuAdi, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(kuyuGrubuAdi) && regex.IsMatch(kuyuGrubuAdi) && !isExisting)
            {
                var newKuyuGrubu = new KuyuGrubuModel
                {
                    KuyuGrubuAdi = kuyuGrubuAdi
                };
                _context.KuyuGrubu.Add(newKuyuGrubu);   
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Kuyu Grubu başarıyla eklendi!";
            }
            else
            {
                if (isExisting)
                {
                    TempData["ErrorMessage"] = "Bu Kuyu grubu adı zaten mevcut.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Kuyu grubu adı yalnızca büyük harf ve boşluk içermelidir.";
                }
            }
            return RedirectToAction("Index", new { pageNumber, pageSize });
        }

        //kuyu grubu silme
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int pageNumber = 1, int pageSize = 50)
        {
            var kuyuGrubu = await _context.KuyuGrubu.FindAsync(id);
            if (kuyuGrubu != null) 
            { 
                _context.KuyuGrubu.Remove(kuyuGrubu);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Kuyu grubu başarıyla silindi!";
            }
            else
            {
                TempData["ErrorMessage"] = "Kuyu grubu bulunamadı.";
            }

            return RedirectToAction("Index", new { pageNumber, pageSize });
        }

        //kuyu grubu güncelleme
        [HttpPost]
        public async Task<IActionResult> Update(int id, KuyuGrubuModel updatedKuyuGrubu, int pageNumber = 1, int pageSize = 50)
        {
            var regex = new Regex(@"^([A-Z]+(\s[A-Z]+)*)-\d+$");
            var kuyuGrubu = await _context.KuyuGrubu.FindAsync(id);

            if (kuyuGrubu == null)
            {
                TempData["ErrorMessage"] = "Güncellenecek saha bulunamadı.";
                return RedirectToAction("Index", new { pageNumber, pageSize });
            }

            // Client-side evaluation (ToList ile veriyi çekip öyle sorgulama)
            var kuyuGruplari = await _context.KuyuGrubu.ToListAsync();
            bool isExisting = kuyuGruplari.Any(k => k.KuyuGrubuAdi.Equals(updatedKuyuGrubu.KuyuGrubuAdi, StringComparison.OrdinalIgnoreCase) && k.Id != id);

            if (!string.IsNullOrEmpty(updatedKuyuGrubu.KuyuGrubuAdi) && regex.IsMatch(updatedKuyuGrubu.KuyuGrubuAdi) && !isExisting)
            {
                kuyuGrubu.KuyuGrubuAdi = updatedKuyuGrubu.KuyuGrubuAdi;
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
