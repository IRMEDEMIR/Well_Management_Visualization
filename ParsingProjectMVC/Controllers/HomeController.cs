
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParsingProjectMVC.Models;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ParsingProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Services.ParsingDbContext _context;

        public HomeController(ILogger<HomeController> logger, Services.ParsingDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var kuyular = await _context.Kuyu.ToListAsync();
            return View(kuyular);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult KuyuDetail()
        {
            // CSV dosyasýnýn URL'sini belirleyin
            var csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "WellboreGeometrisi.csv");

            // CSV dosyasýnýn yolu ve adý view'a ViewBag ile gönderildi
            ViewBag.CsvFilePath = csvFilePath;

            return View();
        }


        public IActionResult SurfaceDetail()
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