using EPR.Accreditation.Facade.Services;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/Accreditation/{accreditationExternalId}/OverseasSite/{siteExternalId}")]
    public class AccreditationOverseasSiteController : ControllerBase
    {
        protected readonly IOverseasSiteService _overseasSiteService;

        public AccreditationOverseasSiteController(IOverseasSiteService overseasSiteService)
        {
            _overseasSiteService = overseasSiteService ?? throw new ArgumentNullException(nameof(overseasSiteService));
        }

        [HttpGet("site")]
        public async Task<IActionResult> GetSite(
            Guid accreditationExternalId,
            Guid siteExternalId)
        {
            var site = await _overseasSiteService.GetOverseasSite(
                accreditationExternalId,
                siteExternalId);
            return Ok(site);
        }


    }
}
