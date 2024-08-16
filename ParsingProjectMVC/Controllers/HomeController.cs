using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsingProjectMVC.Models;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;

namespace ParsingProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<KuyuModel> kuyular = new List<KuyuModel>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            LoadKuyularFromCsv();
        }

        private void LoadKuyularFromCsv()
        {
            try
            {
                // Proje dizininden dosya yolunu oluþturun
                var projectRootPath = Directory.GetCurrentDirectory();
                var filePath = Path.Combine(projectRootPath, "wwwroot", "data", "Kuyular.csv");

                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null,  // Baþlýk doðrulamasýný devre dýþý býrak
                        MissingFieldFound = null // Eksik alan bulunduðunda hata fýrlatmayý devre dýþý býrak
                    };
                    csv.Context.RegisterClassMap<KuyuModelMap>();
                    kuyular = csv.GetRecords<KuyuModel>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
            }
        }

        public IActionResult Index()
        {
            return View(kuyular);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult KuyuDetail()
        {
            return View();
        }

        public IActionResult FormationDetail()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public sealed class KuyuModelMap : ClassMap<KuyuModel>
    {
        public KuyuModelMap()
        {
            Map(m => m.KuyuAdi).Name("KuyuAdi");
            Map(m => m.Enlem).Name("Enlem").Optional();
            Map(m => m.Boylam).Name("Boylam").Optional();
        }
    }
}