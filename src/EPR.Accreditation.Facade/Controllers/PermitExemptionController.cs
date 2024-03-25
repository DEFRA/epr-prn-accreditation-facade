using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/[controller]/{accreditationExternalId}")]
    public class PermitExemptionController : ControllerBase
    {
        protected readonly IPermitExemptionService _permitExemptionService;

        public PermitExemptionController(IPermitExemptionService permitExemptionService)
        {
            _permitExemptionService = permitExemptionService ?? throw new ArgumentNullException(nameof(permitExemptionService));
        }

        [HttpGet]
        public async Task<IActionResult> GetHasPermitExemption(Guid accreditationExternalId)
        {
            var hasPermitExemption = await _permitExemptionService.GetHasPermitExemption(accreditationExternalId);

            return Ok(hasPermitExemption);
        }
    }
}
