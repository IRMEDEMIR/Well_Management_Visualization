using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using ParsingProjectMVC.Models;
using System.Globalization;

namespace ParsingProjectMVC.Controllers
{
    public class WellborelarController : Controller
    {
        private readonly string _filePath = "C:\\Users\\Erdil\\Desktop\\TPAO_01\\TPAO_01\\output\\Wellborelar.csv";
        //private readonly string _filePath = "C:\\Users\\demir\\OneDrive\\Desktop\\Parsing_Project\\TPAO_01\\output\\Wellborelar.csv";

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
                            KuyuGrubuAdi = null,
                            KuyuAdi = null,
                            SahaAdi = null,
                            Derinlik = null
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

        private void SaveWellborelarToCsv()
        {
            try
            {
                using (var writer = new StreamWriter(_filePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(wellborelar.Select(w => new
                    {
                        w.WellboreAdi
                    }));
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"CSV dosyasına yazılırken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
