using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/Accreditation/{accreditationExternalId}/Site/{siteExternalId}")]
    public class AccreditationSiteController : ControllerBase
    {
        protected readonly ISiteService _siteService;

        public AccreditationSiteController(ISiteService siteService)
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

        [HttpPut("ExemptionReferences")]
        public async Task<IActionResult> UpdatePermitExemption(
            Guid accreditationExternalId,
            Guid siteExternalId,
            [FromBody] IEnumerable<ExemptionReference> exemptionReferences)
        {
            await _siteService.UpdateExemptionReferences(
                accreditationExternalId,
                siteExternalId,
                exemptionReferences);

            return Ok();
        }
    }
}
