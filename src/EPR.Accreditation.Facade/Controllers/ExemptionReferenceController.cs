using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/[controller]/{siteId}")]
    public class ExemptionReferenceController : ControllerBase
    {
        protected readonly IExemptionReferenceService _exemptionReferenceService;

        public ExemptionReferenceController(IExemptionReferenceService exemptionService)
        {
            _exemptionReferenceService = exemptionService ?? throw new ArgumentNullException(nameof(exemptionService));
        }

        [HttpGet]
        public async Task<IActionResult> GetExemptionReference(int siteId)
        {
            var exemptionReference = await _exemptionReferenceService.GetExemptionReference(siteId);

            return Ok(exemptionReference);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExemptionReference(
            int exemptionReferenceId,
            int siteId,
            [FromBody] ExemptionReference exemptionReference)
        {
            await _exemptionReferenceService.UpdateExemptionReference(
                exemptionReferenceId,
                siteId,
                exemptionReference);

            return Ok();
        }
    }
}
