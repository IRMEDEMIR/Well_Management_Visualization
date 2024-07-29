using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsingProjectMVC.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

//kuyuya basýnca detayýný gör deyince grafiði göster