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
    }
}
