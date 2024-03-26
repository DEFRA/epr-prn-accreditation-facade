using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/[controller]/{accreditationExternalId}")]
    public class WastePermitController : ControllerBase
    {
        protected readonly IWastePermitService _wastePermitService;

        public WastePermitController(IWastePermitService wastePermitService)
        {
            _wastePermitService = wastePermitService ?? throw new ArgumentNullException(nameof(wastePermitService));
        }

        [HttpGet("WastePermitExemption")]
        public async Task<IActionResult> GetHasPermitExemption(Guid accreditationExternalId)
        {
            var hasPermitExemption = await _wastePermitService.GetHasPermitExemption(accreditationExternalId);

            return Ok(hasPermitExemption);
        }

        [HttpPut("WastePermitExemption")]
        public async Task<IActionResult> UpdatePermitExemption(
            Guid accreditationExternalId,
            [FromBody] PermitExemption permitExemption)
        {
            await _wastePermitService.UpdatePermitExemption(
                accreditationExternalId,
                permitExemption);

            return Ok();
        }
    }
}
