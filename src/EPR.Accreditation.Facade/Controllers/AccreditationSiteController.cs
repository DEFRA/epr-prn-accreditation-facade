using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/Accreditation/{id}/Site")]
    public class AccreditationSiteController : ControllerBase
    {
        protected readonly ISiteService _siteService;

        public AccreditationSiteController(ISiteService siteService)
        {
            _siteService = siteService ?? throw new ArgumentNullException(nameof(siteService));
        }

        [HttpGet("ExemptionReferences")]
        public async Task<IActionResult> GetExemptionReferences(
            Guid id)
        {
            var exemptionReferences = await _siteService.GetExemptionReferences(id);

            return Ok(exemptionReferences);
        }

        [HttpPut("ExemptionReferences")]
        public async Task<IActionResult> UpdatePermitExemption(
            Guid id,
            [FromBody] IEnumerable<string> exemptionReferences)
        {
            await _siteService.UpdateExemptionReferences(
                id,
                exemptionReferences);

            return Ok();
        }
    }
}
