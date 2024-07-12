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
    public class WellborelarController : Controller
    {
        //private readonly string _filePath = "C:\\Users\\WİN10\\Desktop\\TPAO\\Parsing_Project\\TPAO_01\\output\\Wellborelar.csv"; // CSV dosyasının yolunu buraya ekleyin
        //private readonly string _filePath = "C:\\Users\\demir\\OneDrive\\Desktop\\Parsing_Project\\TPAO_01\\output\\Wellborelar.csv";
        //private readonly string _filePath = "C:\\Users\\Pc\\OneDrive\\Masaüstü\\tpao_list\\Parsing_Project\\TPAO_01\\output\\Wellborelar.csv";
        //private readonly string _filePath = "C:\\Users\\Asus\\Desktop\\TPAO\\Parsing_Project\\TPAO_01\\output\\Wellborelar.csv";


        //private List<WellboreModel> wellborelar = new List<WellboreModel>();

        public WellborelarController()
        {
            //LoadWellborelarFromCsv();
        }
        /*
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
                            Derinlik = record.Derinlik,
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
        public IActionResult Create(string wellboreAdi, string derinlik, int pageNumber = 1, int pageSize = 50) 
        {
            var regex = new Regex(@"^([A-Z]+(\s[A-Z]+)*)-\d+(\/K\d*)?(\/[SMR]\d*)*$");

            if (!string.IsNullOrEmpty(wellboreAdi) && regex.IsMatch(wellboreAdi))
            {
                bool isExisting = wellborelar.Any(w => w.WellboreAdi.Equals(wellboreAdi, StringComparison.OrdinalIgnoreCase));
                if (isExisting)
                {
                    TempData["ErrorMessage"] = "Bu Wellbore adı zaten mevcut.";
                    return RedirectToAction("Index", new { pageNumber, pageSize });
                }

                var newWellbore = new WellboreModel
                {
                    Id = wellborelar.Count > 0 ? wellborelar.Max(w => w.Id) + 1 : 1,
                    WellboreAdi = wellboreAdi,
                    Derinlik = derinlik 
                };
                wellborelar.Add(newWellbore);
                SaveWellborelarToCsv();
                TempData["SuccessMessage"] = "Wellbore başarıyla eklendi!";
                return RedirectToAction("Index", new { pageNumber, pageSize });
            }
            else
            {
                TempData["ErrorMessage"] = "Wellbore adı formatı yanlış.";
                return RedirectToAction("Index", new { pageNumber, pageSize });
            }
        }





        [HttpPost]
        public IActionResult Delete(int id, int pageNumber = 1, int pageSize = 50)
        {
            var wellboreToDelete = wellborelar.FirstOrDefault(k => k.Id == id);
            if (wellboreToDelete != null)
            {
                wellborelar.Remove(wellboreToDelete);
                SaveWellborelarToCsv();
            }
            return RedirectToAction("Index", new { pageNumber, pageSize });
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


        [HttpGet]
        public IActionResult Update(int id)
        {
            var wellbore = wellborelar.FirstOrDefault(k => k.Id == id);
            if (wellbore == null)
            {
                return NotFound();
            }
            return View(wellbore);
        }


        [HttpPost]
        public IActionResult Update(int id, WellboreModel updatedWellbore, int pageNumber = 1, int pageSize = 50)
        {
            var regex = new Regex(@"^([A-Z]+(\s[A-Z]+)*)-\d+(\/K\d*)?(\/[SMR]\d*)*$");

            var wellbore = wellborelar.FirstOrDefault(w => w.Id == id);

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

            bool isExisting = wellborelar.Any(w => w.WellboreAdi.Equals(updatedWellbore.WellboreAdi, StringComparison.OrdinalIgnoreCase) && w.Id != id);
            if (isExisting)
            {
                TempData["ErrorMessage"] = "Bu Wellbore adı kullanılmakta.";
                return RedirectToAction("Index", new { pageNumber, pageSize });
            }

            wellbore.WellboreAdi = updatedWellbore.WellboreAdi;
            wellbore.Derinlik = updatedWellbore.Derinlik;
            SaveWellborelarToCsv();
            TempData["SuccessMessage"] = "Wellbore başarıyla güncellendi!";
            return RedirectToAction("Index", new { pageNumber, pageSize });
        }
        */
    }
}
