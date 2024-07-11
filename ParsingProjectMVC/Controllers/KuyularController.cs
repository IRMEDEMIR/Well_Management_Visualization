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
    public class KuyularController : Controller
    {
        //private readonly string _filePath = "C:\\Users\\WİN10\\Desktop\\TPAO\\Parsing_Project\\TPAO_01\\output\\Kuyular.csv"; // CSV dosyasının yolunu buraya ekleyin
        //private readonly string _filePath = "C:\\Users\\demir\\OneDrive\\Desktop\\Parsing_Project\\TPAO_01\\output\\Kuyular.csv";
        private readonly string _filePath = "C:\\Users\\Pc\\OneDrive\\Masaüstü\\tpao_list\\Parsing_Project\\TPAO_01\\output\\Kuyular.csv";
        //private readonly string _filePath = "C:\\Users\\Asus\\Desktop\\TPAO\\Parsing_Project\\TPAO_01\\output\\Kuyular.csv";

        private List<KuyuModel> kuyular = new List<KuyuModel>();

        public KuyularController()
        {
            LoadKuyularFromCsv();
        }

        private void LoadKuyularFromCsv()
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
                        kuyular.Add(new KuyuModel
                        {
                            Id = idCounter++,
                            KuyuAdi = record.KuyuAdi,
                            Enlem = record.Enlem,
                            Boylam = record.Boylam,
                            KuyuGrubuAdi = null,
                            SahaAdi = null
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Error handling
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
            }
        }


        private void SaveKuyularToCsv()
        {
            try
            {
                using (var writer = new StreamWriter(_filePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(kuyular.Select(k => new
                    {
                        k.KuyuAdi,
                        k.Enlem,
                        k.Boylam
                    }));
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"CSV dosyasına yazılırken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Create(string kuyuAdi, string enlem, string boylam)
        {
            var regex = new Regex(@"^[A-Z]+-\d+(\/K\d*)?$");
            bool isExisting = kuyular.Any(k => k.KuyuAdi.Equals(kuyuAdi, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(kuyuAdi) && regex.IsMatch(kuyuAdi) && !isExisting && double.TryParse(enlem, out double parsedEnlem) && double.TryParse(boylam, out double parsedBoylam))
            {
                var newKuyu = new KuyuModel
                {
                    Id = kuyular.Count > 0 ? kuyular.Max(k => k.Id) + 1 : 1,
                    KuyuAdi = kuyuAdi,
                    Enlem = parsedEnlem.ToString(), 
                    Boylam = parsedBoylam.ToString() 
                };
                kuyular.Add(newKuyu);
                SaveKuyularToCsv();

            TempData["SuccessMessage"] = "Kuyu başarıyla eklendi!";
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }
        }



        [HttpPost]
        public IActionResult Delete(int id)
        {
            var kuyuToDelete = kuyular.FirstOrDefault(k => k.Id == id);
            if (kuyuToDelete != null)
            {
                kuyular.Remove(kuyuToDelete);
                SaveKuyularToCsv();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index(int pageNumber = 1, int pageSize = 50)
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


        [HttpGet]
        public IActionResult Update(int id)
        {
            var kuyu = kuyular.FirstOrDefault(k => k.Id == id);
            if (kuyu == null)
            {
                return NotFound();
            }
            return View(kuyu);
        }

        [HttpPost]
        public IActionResult Update(int id, KuyuModel updatedKuyu)
        {
            var regex = new Regex(@"^[A-Z]+-\d+(\/K\d*)?$");  
            var kuyu = kuyular.FirstOrDefault(k => k.Id == id);

            if (kuyu == null)
            {
                TempData["ErrorMessage"] = "Güncellenecek Kuyu bulunamadı.";
                return RedirectToAction("Index");
            }

            if (!regex.IsMatch(updatedKuyu.KuyuAdi))
            {
                TempData["ErrorMessage"] = "Kuyu adı formatınız doğru değil.";
                return RedirectToAction("Index");
            }

            bool isExisting = kuyular.Any(k => k.KuyuAdi.Equals(updatedKuyu.KuyuAdi, StringComparison.OrdinalIgnoreCase) && k.Id != id);

            if (isExisting)
            {
                TempData["ErrorMessage"] = "Bu Kuyu adı kullanılmakta.";
                return RedirectToAction("Index");
            }

            kuyu.KuyuAdi = updatedKuyu.KuyuAdi;
            kuyu.Enlem = updatedKuyu.Enlem;
            kuyu.Boylam = updatedKuyu.Boylam;
            SaveKuyularToCsv();
            TempData["SuccessMessage"] = "Kuyu başarıyla güncellendi!";
            return RedirectToAction("Index");
        }

    }
}
