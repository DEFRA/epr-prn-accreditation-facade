using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/Site/{siteId}")]
    public class SiteController : ControllerBase
    {
        protected readonly ISiteService _siteServiceService;

        public SiteController(ISiteService siteService)
        {
            _siteServiceService = siteService ?? throw new ArgumentNullException(nameof(siteService));
        }

        [HttpGet("ExemptionReference/{exemptionReferenceId}")]
        public async Task<IActionResult> GetExemptionReference(
            int exemptionReferenceId,
            int siteId)
        {
            var exemptionReference = await _siteServiceService.GetExemptionReference(
                exemptionReferenceId,
                siteId);

            return Ok(exemptionReference);
        }

        [HttpPut("ExemptionReference/{exemptionReferenceId}")]
        public async Task<IActionResult> UpdateExemptionReference(
            int exemptionReferenceId,
            int siteId,
            [FromBody] ExemptionReference exemptionReference)
        {
            await _siteServiceService.UpdateExemptionReference(
                exemptionReferenceId,
                siteId,
                exemptionReference);

            return Ok();
        }
    }
}
