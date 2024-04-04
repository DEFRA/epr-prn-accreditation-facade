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

        [HttpPost]
        public async Task<IActionResult> CreateSite(
            Common.Dtos.Site site)
        {
            await _siteService.CreateSite(site);

            return Ok();
        }

        [HttpGet("{accreditationExternalId}/Site/{siteExternalId}")]
        public async Task<IActionResult> GetSite(
            Guid accreditationExternalId,
            Guid siteExternalId)
        {
            var site = await _siteService.GetSite(accreditationExternalId, siteExternalId);

            return Ok(site);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSite(
            Guid SiteId,
            [FromBody] Site site)
        {
            await _siteService.UpdateSite(
                SiteId, site);

            return Ok();
        }
    }
}
