using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using ParsingProjectMVC.Models;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace ParsingProjectMVC.Controllers
{
    public class KuyularController : Controller
    {
        //private readonly string _filePath = "C:\\Users\\WİN10\\Desktop\\TPAO\\Parsing_Project\\TPAO_01\\output\\Kuyular.csv"; // CSV dosyasının yolunu buraya ekleyin
        //private readonly string _filePath = "C:\\Users\\demir\\OneDrive\\Desktop\\Parsing_Project\\TPAO_01\\output\\Kuyular.csv";
        //private readonly string _filePath = "C:\\Users\\Pc\\OneDrive\\Masaüstü\\tpao_list\\Parsing_Project\\TPAO_01\\output\\Kuyular.csv";
        private readonly string _filePath = "C:\\Users\\Asus\\Desktop\\TPAO\\Parsing_Project\\TPAO_01\\output\\Kuyular.csv";

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
                            Enlem = null,
                            Boylam = null,
                            KuyuGrubuAdi = null,
                            SahaAdi = null
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

        private void SaveKuyularToCsv()
        {
            try
            {
                using (var writer = new StreamWriter(_filePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(kuyular.Select(k => new
                    {
                        k.KuyuAdi
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
        public IActionResult Create(string kuyuAdi)
        {
            if (!string.IsNullOrEmpty(kuyuAdi))
            {
                var newKuyu = new KuyuModel
                {
                    Id = kuyular.Count > 0 ? kuyular.Max(k => k.Id) + 1 : 1,
                    KuyuAdi = kuyuAdi
                };
                kuyular.Add(newKuyu);
                SaveKuyularToCsv();
            }
            return RedirectToAction("Index");
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
    }
}
