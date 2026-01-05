using System.Diagnostics;
using IM2B.Models;
using Microsoft.AspNetCore.Mvc;
using shared.Interfaces;
using shared.Models;

namespace IM2B.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Filme> _filmeRepo;

        public HomeController(ILogger<HomeController> logger, IGenericRepository<Filme> filmeRepo)
        {
            _logger = logger;
            _filmeRepo = filmeRepo;
        }

        public async Task<IActionResult> Index()
        {
            var filmes = await _filmeRepo.GetAllAsync();
            return View(filmes);
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
