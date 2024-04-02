using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/[controller]/{siteId}")]
    public class ExemptionReferenceController : ControllerBase
    {
        protected readonly ISaveAndComeBackService _exemptionService;

        public ExemptionReferenceController(IExemptionService exemeptionService)
        {
            _exemptionService = exemeptionService ?? throw new ArgumentNullException(nameof(exemeptionService));
        }

        [HttpGet]
        public async Task<IActionResult> GetExemptionReference(int siteId)
        {
            var exemptionReference = await _exemptionService.GetExemptionReference(siteId);

            return Ok(exemptionReference);
        }
    }
