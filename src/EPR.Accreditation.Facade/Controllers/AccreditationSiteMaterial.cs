using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/Accreditation/{accreditationExternalId}/Site/{siteExternalId}/Material/{materialExternalId}")]
    public class AccreditationSiteMaterial : ControllerBase
    {
        protected readonly IAccreditationService _accreditationService;
        protected readonly IWastePermitService _wastePermitService;

        public AccreditationSiteMaterial(
            IAccreditationService accreditationService,
            IWastePermitService wastePermitService)
        {
            _accreditationService = accreditationService ?? throw new ArgumentNullException(nameof(accreditationService));
            _wastePermitService = wastePermitService ?? throw new ArgumentNullException(nameof(wastePermitService));
        }

        [HttpGet("WasteSource")]
        public async Task<IActionResult> GetWasteSource(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId)
        {
            var wasteSource = await _accreditationService.GetWasteSource(
                accreditationExternalId,
                siteExternalId,
                materialExternalId);

            return Ok(wasteSource);
        }

        [HttpPut("WasteSource")]
        public async Task<IActionResult> SaveWasteSource(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            [FromBody] string wasteSource)
        {
            await _accreditationService.UpdateWasteSource(
                accreditationExternalId,
                siteExternalId,
                materialExternalId,
                wasteSource);

            return Ok();
        }

        [HttpGet("Name")]
        public async Task<IActionResult> GetMaterialName(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            Language language)
        {
            if (language == Language.Undefined)
                return BadRequest("Invalid language selection. Must be either English(1) or Welsh(2)");

            var wasteSource = await _accreditationService.GetWasteMaterialName(
                accreditationExternalId,
                siteExternalId,
                materialExternalId,
                language);

            return Ok(wasteSource);
        }

        [HttpGet("MaterialOutputs")]
        public async Task<IActionResult> GetMaterialOutputs(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId)
        {
            var materialOoutputs = await _accreditationService.GetMaterialOutputs(
                accreditationExternalId,
                siteExternalId,
                materialExternalId);

            return Ok(materialOoutputs);
        }

        [HttpPut("MaterialOutputs")]
        public async Task<IActionResult> UpdateMaterialOutputs(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            [FromBody] MaterialOutputsDto materialOutputsDto)
        {
            await _accreditationService.UpdateMaterialOutputs(
                accreditationExternalId,
                siteExternalId,
                materialExternalId,
                materialOutputsDto);

            return Ok();
        }
    }
}
