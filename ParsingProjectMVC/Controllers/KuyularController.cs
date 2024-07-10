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
        private readonly string _filePath = "C:\\Users\\WİN10\\Desktop\\TPAO\\Parsing_Project\\TPAO_01\\output\\Kuyular.csv"; // CSV dosyasının yolunu buraya ekleyin
        //private readonly string _filePath = "C:\\Users\\demir\\OneDrive\\Desktop\\Parsing_Project\\TPAO_01\\output\\Kuyular.csv";
        //private readonly string _filePath = "C:\\Users\\Pc\\OneDrive\\Masaüstü\\tpao_list\\Parsing_Project\\TPAO_01\\output\\Kuyular.csv";
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
                        var (lat, lon) = GenerateRandomCoordinates(); // Generate random coordinates
                        kuyular.Add(new KuyuModel
                        {
                            Id = idCounter++,
                            KuyuAdi = record.KuyuAdi,
                            Enlem = lat,
                            Boylam = lon,
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

        private (double, double) GenerateRandomCoordinates()
        {
            // Define boundaries of Turkey in terms of latitude and longitude
            double minLat = 36.0;
            double maxLat = 42.0;
            double minLon = 26.0;
            double maxLon = 45.0;

            // Generate random coordinates within the defined boundaries
            Random rand = new Random();
            double lat = rand.NextDouble() * (maxLat - minLat) + minLat;
            double lon = rand.NextDouble() * (maxLon - minLon) + minLon;

            return (lat, lon);
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

        // POST: KuyuGruplari/Update
        [HttpPost]
        public IActionResult Update(int id, KuyuModel updatedKuyu)
        {
            var kuyu = kuyular.FirstOrDefault(k => k.Id == id);
            if (kuyu == null)
            {
                return NotFound();
            }

            kuyu.KuyuAdi = updatedKuyu.KuyuAdi;
            kuyu.Enlem = updatedKuyu.Enlem;
            kuyu.Boylam = updatedKuyu.Boylam;
            SaveKuyularToCsv();
            return RedirectToAction("Index");
        }
    }
}
