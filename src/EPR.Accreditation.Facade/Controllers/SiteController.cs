using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/Accreditation/{accreditationExternalId}/Site/{siteExternalId}")]
    public class SiteController : ControllerBase
    {
        protected readonly ISiteService _siteService;

        public SiteController(ISiteService siteService)
        {
            _siteService = siteService ?? throw new ArgumentNullException(nameof(siteService));
        }

        [HttpGet("ExemptionReferences")]
        public async Task<IActionResult> GetExemptionReferences(
            Guid accreditationExternalId,
            Guid siteExternalId)
        {
            var exemptionReferences = await _siteService.GetExemptionReferences(
                accreditationExternalId,
                siteExternalId);

            return Ok(exemptionReferences);
        }
    }
}
