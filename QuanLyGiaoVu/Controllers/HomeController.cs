using Microsoft.AspNetCore.Mvc;
using QuanLyGiaoVu.Data;
using QuanLyGiaoVu.Models;
using System.Diagnostics;

namespace QuanLyGiaoVu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QlgvContext _context;
        public HomeController(ILogger<HomeController> logger, QlgvContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.hv = _context.Hocviens.Count();
            ViewBag.lh = _context.Lophocs.Count();
            ViewBag.gv = _context.Giaoviens.Count();
            var data = _context.Thongtinchitietlophocs.GroupBy(t => t.MalophocNavigation.Tenlophoc).Select(
                g => new {
                    lophoc = g.Key,
                    soluong = g.Count()
                }
                ).ToList();
            ViewBag.ChartData = data;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("/404")]
        public IActionResult PageNotfound()
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
