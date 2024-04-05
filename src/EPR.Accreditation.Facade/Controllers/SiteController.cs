using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SiteController : ControllerBase
    {
        protected readonly ISiteService _siteService;
        
        public SiteController(
            ISiteService siteService)
        {
            _siteService = siteService ?? throw new ArgumentNullException(nameof(siteService));
        }

        [HttpGet("Site/{id}")]
        public async Task<IActionResult> GetSite(
            Guid id)
        {
            var site = await _siteService.GetSite(id);

            return Ok(site);
        }
    }
}
