using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsingProjectMVC.Models;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Collections.Generic;

namespace ParsingProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly string _filePath = "C:\\Users\\WÝN10\\Desktop\\TPAO\\Parsing_Project\\TPAO_01\\output\\Kuyular.csv"; // CSV dosyasýnýn yolunu buraya ekleyin
        //private readonly string _filePath = "C:\\Users\\demir\\OneDrive\\Desktop\\Parsing_Project\\TPAO_01\\output\\Kuyular.csv";
        //private readonly string _filePath = "C:\\Users\\Pc\\OneDrive\\Masaüstü\\tpao_list\\Parsing_Project\\TPAO_01\\output\\Kuyular.csv";
        private readonly string _filePath = "C:\\Users\\Asus\\Desktop\\TPAO\\Parsing_Project\\TPAO_01\\output\\Kuyular.csv";
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
                using (var reader = new StreamReader(_filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<KuyuModel>().ToList();
                    kuyular = records;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
