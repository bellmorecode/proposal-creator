using Microsoft.AspNetCore.Mvc;
using propmaker.Models;
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

        [HttpPost]
        public async Task<IActionResult> SaveProposal(IFormCollection form)
        {
            var proposalId = Guid.Parse($"{form["proposalId"]}");


            return RedirectToAction("Index", "Home", new { p = proposalId });
        }

        [HttpPost]
        public async Task<IActionResult> NewProposal(int id = 0)
        {
            var success = false;
            // id is the templateId, not used for now but the default is zero, blank template.

            // set the defaults... 

            if (id == 0)
            {
                var prop = new ProposalDocument();
                prop.Header.Title = "New Proposal";
                prop.Header.Overview = @"Praesent interdum lectus eu pellentesque laoreet. Nulla sem lorem, pharetra eget orci sed, placerat accumsan magna. Nam rutrum, erat ac consectetur sollicitudin, massa leo auctor tortor, in scelerisque ex nisi scelerisque ex. Nullam nibh sem, mattis eget lacinia nec, sollicitudin sit amet odio. Quisque pulvinar maximus augue ac rutrum. ";
                prop.Header.Filename = "proposal.docx";

                prop.Sections.Add(new DocumentSection { SortOrder = 1, Title = "Business Objectives", Description = " <Coming Soon... > " });
                prop.Sections.Add(new DocumentSection { SortOrder = 2, Title = "Services", Description = " <Coming Soon... > " });
                prop.Sections.Add(new DocumentSection { SortOrder = 3, Title = "Project Output and Deliverables", Description = " <Coming Soon... > " });
                prop.Sections.Add(new DocumentSection { SortOrder = 4, Title = "Project Cost and Terms", Description = " <Coming Soon... > " });
                prop.Sections.Add(new DocumentSection { SortOrder = 5, Title = "Signatures", Description = " <Coming Soon... > " });

                success = await svc.SaveProposal(prop);
            }

            return Json(new { success });
        }
    }
}