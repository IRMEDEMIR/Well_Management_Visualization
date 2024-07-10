using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using ParsingProjectMVC.Models;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace ParsingProjectMVC.Controllers
{
    public class WellborelarController : Controller
    {
        private readonly string _filePath = "C:\\Users\\WİN10\\Desktop\\TPAO\\Parsing_Project\\TPAO_01\\output\\Wellborelar.csv"; // CSV dosyasının yolunu buraya ekleyin
        private List<WellboreModel> wellborelar = new List<WellboreModel>();

        public WellborelarController()
        {
            LoadWellborelarFromCsv();
        }

        private void LoadWellborelarFromCsv()
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
                        wellborelar.Add(new WellboreModel
                        {
                            Id = idCounter++,
                            WellboreAdi = record.WellboreAdi,
                            Derinlik = GenerateRandomDepth(), // Generate random depth
                            KuyuGrubuAdi = null,
                            KuyuAdi = null,
                            SahaAdi = null
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Error handling
                Console.WriteLine($"CSV dosyası okunurken bir hata oluştu: {ex.Message}");
            }
        }

        private void SaveWellborelarToCsv()
        {
            try
            {
                using (var writer = new StreamWriter(_filePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(wellborelar.Select(w => new
                    {
                        w.WellboreAdi,
                        w.Derinlik // Save depth value
                    }));
                }
            }
            catch (Exception ex)
            {
                // Error handling
                Console.WriteLine($"CSV dosyasına yazılırken bir hata oluştu: {ex.Message}");
            }
        }

        private int GenerateRandomDepth()
        {
            Random random = new Random();
            // Uniform distribution between 100 and 10000
            return random.Next(100, 10001);
        }

        [HttpPost]
        public IActionResult Create(string wellboreAdi)
        {
            if (!string.IsNullOrEmpty(wellboreAdi))
            {
                var newWellbore = new WellboreModel
                {
                    Id = wellborelar.Count > 0 ? wellborelar.Max(w => w.Id) + 1 : 1,
                    WellboreAdi = wellboreAdi,
                    Derinlik = GenerateRandomDepth() // Generate random depth for new wellbore
                };
                wellborelar.Add(newWellbore);
                SaveWellborelarToCsv();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var wellboreToDelete = wellborelar.FirstOrDefault(k => k.Id == id);
            if (wellboreToDelete != null)
            {
                wellborelar.Remove(wellboreToDelete);
                SaveWellborelarToCsv();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index(int pageNumber = 1, int pageSize = 50)
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
