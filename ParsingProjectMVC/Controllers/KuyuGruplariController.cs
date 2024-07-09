using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using ParsingProjectMVC.Models;
using System.Globalization;

namespace ParsingProjectMVC.Controllers
{
    public class KuyuGruplariController : Controller
    {
        private readonly string _filePath = "C:\\Users\\demir\\OneDrive\\Desktop\\Parsing_Project\\TPAO_01\\output\\Kuyu Grupları.csv"; // CSV dosyasının yolunu buraya ekleyin
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
            catch(Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"CSV dosyası okunurken bir hata oluştu: {ex.Message}");
            }
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
    }
}
