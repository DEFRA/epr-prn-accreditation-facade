using EPR.Accreditation.Facade.Common.Dtos;
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

        [HttpGet("{accreditationExternalId}/Site/{siteExternalId}")]
        public async Task<IActionResult> GetSite(
            Guid accreditationExternalId,
            Guid siteExternalId)
        {
            var site = await _siteService.GetSite(accreditationExternalId, siteExternalId);

            return Ok(site);
        }
    }
}
