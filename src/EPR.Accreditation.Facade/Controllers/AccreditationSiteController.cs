using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/Accreditation/{accreditationExternalId}/Site")]
    public class AccreditationSiteController : ControllerBase
    {
        protected readonly ISiteService _siteService;

        public AccreditationSiteController(ISiteService siteService)
        {
            _siteService = siteService ?? throw new ArgumentNullException(nameof(siteService));
        }

        [HttpGet("ExemptionReferences")]
        public async Task<IActionResult> GetExemptionReferences(
            Guid accreditationExternalId)
        {
            var exemptionReferences = await _siteService.GetExemptionReferences(accreditationExternalId);

            return Ok(exemptionReferences);
        }

        [HttpPut("ExemptionReferences")]
        public async Task<IActionResult> UpdatePermitExemption(
            Guid accreditationExternalId,
            [FromBody] IEnumerable<string> exemptionReferences)
        {
            await _siteService.UpdateExemptionReferences(
                accreditationExternalId,
                exemptionReferences);

            return Ok();
        }
    }
}
