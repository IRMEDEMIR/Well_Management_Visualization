using Microsoft.AspNetCore.Mvc;
using ParsingProjectMVC.Models;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace ParsingProjectMVC.Controllers
{
    public class KuyularController : Controller
    {
        private readonly string _filePath = "C:\\Users\\Erdil\\Desktop\\TPAO_01\\TPAO_01\\output\\Kuyular.csv"; // CSV dosyasının yolunu buraya ekleyin

        //private readonly string _filePath = "C:\\Users\\demir\\OneDrive\\Desktop\\Parsing_Project\\TPAO_01\\output\\Kuyular.csv"; // CSV dosyasının yolunu buraya ekleyin
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
