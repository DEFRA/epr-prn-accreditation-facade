using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/[controller]/{accreditationExternalId}/")]
    public class AccreditationController : ControllerBase
    {
        protected readonly IAccreditationService _accreditationService;
        protected readonly IWastePermitService _wastePermitService;

        public AccreditationController(
            IAccreditationService accreditationService,
            IWastePermitService wastePermitService)
        {
            _accreditationService = accreditationService ?? throw new ArgumentNullException(nameof(accreditationService));
            _wastePermitService = wastePermitService ?? throw new ArgumentNullException(nameof(wastePermitService));
        }

        [HttpGet("OperatorType")]
        public async Task<IActionResult> GetOperatorType(
            Guid accreditationExternalId)
        {
            var operatorTypeId = await _accreditationService.GetOperatorType(accreditationExternalId);

            return Ok(operatorTypeId);
        }

        [HttpPost]
        [Route("/api/[controller]")]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<IActionResult> CreateAccreditation([FromBody] Common.Dtos.Accreditation accreditation)
        {
            var externalId = await _accreditationService.CreateAccreditation(accreditation);

            return Ok(externalId);
        }

        [HttpPost("WastePermit")]
        public async Task<IActionResult> CreateWastePermit(
            Guid accreditationExternalId,
            Common.Dtos.WastePermit wastePermit)
        {
            await _accreditationService.CreateWastePermit(accreditationExternalId, wastePermit);

            return Ok();
        }

        [HttpGet("WastePermit")]
        public async Task<IActionResult> GetWastePermit(Guid accreditationExternalId)
        {
            var wastePermit = await _accreditationService.GetWastePermit(accreditationExternalId);

            return Ok(wastePermit);
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
