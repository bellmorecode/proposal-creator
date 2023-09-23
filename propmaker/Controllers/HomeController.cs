using Microsoft.AspNetCore.Mvc;
using propmaker.Models;
using propmaker.Services;
using System.Diagnostics;

namespace propmaker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration cfg;

        private readonly DataService svc;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            cfg = config;
            svc = new DataService(cfg);
        }

        public async Task<IActionResult> Index()
        {
            var proposals = await svc.GetProposals();
            return View(proposals);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}