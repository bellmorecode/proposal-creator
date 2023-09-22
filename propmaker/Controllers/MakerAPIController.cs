using Microsoft.AspNetCore.Mvc;
using propmaker.Services;

namespace propmaker.Controllers
{
    public class MakerAPIController : Controller
    {
        private readonly ILogger<MakerAPIController> _logger;
        private readonly IConfiguration cfg;

        private readonly DataService svc;
        private readonly ReportGenerator gen;
        public MakerAPIController(ILogger<MakerAPIController> logger, IConfiguration config) 
        {
            _logger = logger;
            cfg = config;
            svc = new DataService(cfg);
            gen = new ReportGenerator(cfg);
        }
    }
}