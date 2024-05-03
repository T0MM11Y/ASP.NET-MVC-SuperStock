using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ManageMart.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageMart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }



        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> Seller()
        {
            var sellers = await _context.Sellers.ToListAsync();
            if (sellers == null)
            {
                sellers = new List<Seller>();
            }
            return View(sellers);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}