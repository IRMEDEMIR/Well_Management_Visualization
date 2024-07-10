using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using ParsingProjectMVC.Models;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ParsingProjectMVC.Controllers
{
    public class KuyuGruplariController : Controller
    {
        //private readonly string _filePath = "C:\\Users\\WİN10\\Desktop\\TPAO\\Parsing_Project\\TPAO_01\\output\\Kuyu Grupları.csv"; // CSV dosyasının yolunu buraya ekleyin
        //private readonly string _filePath = "C:\\Users\\demir\\OneDrive\\Desktop\\Parsing_Project\\TPAO_01\\output\\Kuyu Grupları.csv";
        //private readonly string _filePath = "C:\\Users\\Pc\\OneDrive\\Masaüstü\\tpao_list\\Parsing_Project\\TPAO_01\\output\\Kuyu Grupları.csv";
        private readonly string _filePath = "C:\\Users\\Asus\\Desktop\\TPAO\\Parsing_Project\\TPAO_01\\output\\Kuyu Grupları.csv";
        private List<KuyuGrubuModel> kuyuGruplari = new List<KuyuGrubuModel>();

        public KuyuGruplariController()
        {
            LoadKuyuGruplariFromCsv();
        }

        private void LoadKuyuGruplariFromCsv()
        {
            try
            {
                using (var reader = new StreamReader(_filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<dynamic>().ToList();
                    int idCounter = 1;
                    foreach (var record in records)
                    {
                        kuyuGruplari.Add(new KuyuGrubuModel
                        {
                            Id = idCounter++,
                            KuyuGrubuAdi = record.KuyuGrubuAdi,
                            SahaAdi = null //burayı kontrol et
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"CSV dosyası okunurken bir hata oluştu: {ex.Message}");
            }
        }

        private void SaveKuyuGruplariToCsv()
        {
            try
            {
                using (var writer = new StreamWriter(_filePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(kuyuGruplari.Select(k => new { KuyuGrubuAdi = k.KuyuGrubuAdi }));
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"CSV dosyasına yazılırken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Create(string kuyuGrubuAdi)
        {
            var regex = new Regex(@"^[A-Z]+-\d+$");  
            bool isExisting = kuyuGruplari.Any(k => k.KuyuGrubuAdi.Equals(kuyuGrubuAdi, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(kuyuGrubuAdi) && regex.IsMatch(kuyuGrubuAdi) && !isExisting)
            {
                var newKuyuGrubu = new KuyuGrubuModel
                {
                    Id = kuyuGruplari.Count > 0 ? kuyuGruplari.Max(k => k.Id) + 1 : 1,
                    KuyuGrubuAdi = kuyuGrubuAdi
                };
                kuyuGruplari.Add(newKuyuGrubu);
                SaveKuyuGruplariToCsv();
                TempData["SuccessMessage"] = "Kuyu Grubu başarıyla eklendi!";
            }
            else
            {
                if (isExisting)
                {
                    TempData["ErrorMessage"] = "Kuyu Grubu adı zaten mevcut.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Kuyu Grubu adı formatınız doğru değil.";
                }
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var kuyuGrubu = kuyuGruplari.FirstOrDefault(k => k.Id == id);
            if (kuyuGrubu != null)
            {
                kuyuGruplari.Remove(kuyuGrubu);
                SaveKuyuGruplariToCsv();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index(int pageNumber = 1, int pageSize = 50)
        {
            var pagedKuyuGruplari = kuyuGruplari
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalItems = kuyuGruplari.Count;

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            return View(pagedKuyuGruplari);
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            var kuyuGrubu = kuyuGruplari.FirstOrDefault(k => k.Id == id);
            if (kuyuGrubu == null)
            {
                return NotFound();
            }
            return View(kuyuGrubu);
        }
        [HttpPost]
        public IActionResult Update(int id, KuyuGrubuModel updatedKuyuGrubu)
        {
            var regex = new Regex(@"^[A-Z]+-\d+$");  
            var kuyuGrubu = kuyuGruplari.FirstOrDefault(k => k.Id == id);

            if (kuyuGrubu == null)
            {
                TempData["ErrorMessage"] = "Güncellenecek Kuyu Grubu bulunamadı.";
                return RedirectToAction("Index");
            }

            if (!regex.IsMatch(updatedKuyuGrubu.KuyuGrubuAdi))
            {
                TempData["ErrorMessage"] = "Kuyu Grubu adı formatınız doğru değil.";
                return RedirectToAction("Index");
            }

            kuyuGrubu.KuyuGrubuAdi = updatedKuyuGrubu.KuyuGrubuAdi;
            SaveKuyuGruplariToCsv();
            TempData["SuccessMessage"] = "Kuyu Grubu başarıyla güncellendi!";
            return RedirectToAction("Index");
        }

    }
}
