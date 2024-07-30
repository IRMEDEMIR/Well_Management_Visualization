using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParsingProjectMVC.Models;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
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

        public async Task<IActionResult> KuyuDetail(int id)
        {
            // JSON dosyasýnýn yolunu belirleyin
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "TestWellDirectionSurvey.json");

            // JSON dosyasýný okuyun
            var jsonString = System.IO.File.ReadAllText(jsonFilePath);
            var kuyuData = JsonSerializer.Deserialize<KuyuJsonModel>(jsonString);

            // Veriyi kontrol edin
            if (kuyuData == null)
            {
                return NotFound();
            }

            // JSON verisini ViewBag ile gönder
            ViewBag.KuyuData = jsonString;

            return View(kuyuData);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
