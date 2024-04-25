using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/Accreditation/{accreditationExternalId}/Site/Material/{materialExternalId}")]
    public class AccreditationSiteMaterialController : ControllerBase
    {
        protected readonly IAccreditationService _accreditationService;
        protected readonly IWastePermitService _wastePermitService;
        protected readonly IAccreditationMaterialService _accreditationMaterialService;

        public AccreditationSiteMaterialController(
            IAccreditationService accreditationService,
            IWastePermitService wastePermitService,
            IAccreditationMaterialService accreditationMaterialService)
        {
            _accreditationService = accreditationService ?? throw new ArgumentNullException(nameof(accreditationService));
            _wastePermitService = wastePermitService ?? throw new ArgumentNullException(nameof(wastePermitService));
            _accreditationMaterialService = accreditationMaterialService ?? throw new ArgumentNullException(nameof(accreditationMaterialService));
        }

        [HttpGet("WasteSource")]
        public async Task<IActionResult> GetWasteSource(
            Guid accreditationExternalId,
            Guid materialExternalId)
        {
            var wasteSource = await _accreditationService.GetWasteSource(
                SiteType.Site,
                accreditationExternalId,
                null,
                materialExternalId);

            return Ok(wasteSource);
        }

        [HttpPut("WasteSource")]
        public async Task<IActionResult> SaveWasteSource(
            Guid accreditationExternalId,
            Guid materialExternalId,
            [FromBody] string wasteSource)
        {
            await _accreditationService.UpdateWasteSource(
                SiteType.Site,
                accreditationExternalId,
                null,
                materialExternalId,
                wasteSource);

            return Ok();
        }

        [HttpGet("Name")]
        public async Task<IActionResult> GetMaterialName(
            Guid accreditationExternalId,
            Guid? siteExternalId,
            Guid materialExternalId,
            Language language)
        {
            if (language == Language.Undefined)
                return BadRequest("Invalid language selection. Must be either English(1) or Welsh(2)");

            var wasteSource = await _accreditationService.GetWasteMaterialName(
                SiteType.Site,
                accreditationExternalId,
                null,
                materialExternalId,
                language);

            return Ok(wasteSource);
        }

        [HttpGet("NonWasteInputs")]
        public async Task<IActionResult> GetNonWasteInputs(
            Guid accreditationExternalId,
            Guid materialExternalId)
        {
            var nonWasteInputs = await _accreditationService.GetReprocessorSupportingInformation(
                accreditationExternalId,
                materialExternalId,
                ReprocessorSupportingInformationType.NonWasteInputs);

            return Ok(nonWasteInputs);
        }

        [HttpPut("NonWasteInputs")]
        public async Task<IActionResult> UpdateNonWasteInputs(
            Guid accreditationExternalId,
            Guid materialExternalId,
            [FromBody] ReprocessingSupportingInformationDto nonWasteInputsDto)
        {
            await _accreditationService.UpdateReprocessorSupportingInformation(
                accreditationExternalId,
                materialExternalId,
                nonWasteInputsDto,
                ReprocessorSupportingInformationType.NonWasteInputs);

            return Ok();
        }

        [HttpGet("ProductsProduced")]
        public async Task<IActionResult> GetProductsProduced(
            Guid accreditationExternalId,
            Guid materialExternalId)
        {
            var nonWasteInputs = await _accreditationService.GetReprocessorSupportingInformation(
                accreditationExternalId,
                materialExternalId,
                ReprocessorSupportingInformationType.ProductsProduced);

            return Ok(nonWasteInputs);
        }

        [HttpPut("ProductsProduced")]
        public async Task<IActionResult> UpdateProductsProduced(
            Guid accreditationExternalId,
            Guid materialExternalId,
            [FromBody] ReprocessingSupportingInformationDto nonWasteInputsDto)
        {
            await _accreditationService.UpdateReprocessorSupportingInformation(
                accreditationExternalId,
                materialExternalId,
                nonWasteInputsDto,
                ReprocessorSupportingInformationType.ProductsProduced);

            return Ok();
        }

        [HttpGet("MaterialOutputs")]
        public async Task<IActionResult> GetMaterialOutputs(
            Guid accreditationExternalId,
            Guid materialExternalId)
        {
            var materialOoutputs = await _accreditationService.GetMaterialOutputs(
                accreditationExternalId,
                materialExternalId);

            return Ok(materialOoutputs);
        }

        [HttpPut("MaterialOutputs")]
        public async Task<IActionResult> UpdateMaterialOutputs(
            Guid accreditationExternalId,
            Guid materialExternalId,
            [FromBody] MaterialOutputsDto materialOutputsDto)
        {
            await _accreditationService.UpdateMaterialOutputs(
                accreditationExternalId,
                materialExternalId,
                materialOutputsDto);

            return Ok();
        }

        [HttpGet("MaterialWasteOutputs")]
        public async Task<IActionResult> GetMaterialWasteOutputs(
            Guid accreditationExternalId,
            Guid materialExternalId)
        {
            var materialOoutputs = await _accreditationService.GetMaterialWasteOutputs(
                accreditationExternalId,
                materialExternalId);

            return Ok(materialOoutputs);
        }

        [HttpPut("MaterialWasteOutputs")]
        public async Task<IActionResult> UpdateMaterialWasteOutputs(
            Guid accreditationExternalId,
            Guid materialExternalId,
            [FromBody] MaterialWasteOutputsDto materialWasteOutputsDto)
        {
            await _accreditationService.UpdateMaterialWasteOutputs(
                accreditationExternalId,
                materialExternalId,
                materialWasteOutputsDto);

            return Ok();
        }

        [HttpGet("WasteLastYear")]
        public async Task<IActionResult> GetReprocessedWasteLastYear(
            Guid accreditationExternalId,
            Guid materialExternalId)
        {
            var reprocessedWasteLastYear = await _accreditationMaterialService.GetReprocessedWasteLastYear(
                accreditationExternalId,
                materialExternalId);

            return Ok(reprocessedWasteLastYear);
        }

        [HttpPut("WasteLastYear")]
        public async Task<IActionResult> UpdateReprocessedWasteLastYear(
            Guid accreditationExternalId,
            Guid materialExternalId,
            [FromBody] ReprocessedWasteLastYear reprocessedWasteLastYear)
        {
            await _accreditationMaterialService.UpdateReprocessedWasteLastYear(
                accreditationExternalId,
                materialExternalId,
                reprocessedWasteLastYear);

            return Ok();
        }

        [HttpGet("HasNpwdAccreditationNumber")]
        public async Task<IActionResult> GetHasNpwdAccreditationNumber(
            Guid accreditationExternalId,
            Guid materialExternalId)
        {
            var hasNpwdAccreditationNumber = await _accreditationMaterialService.GetHasNpwdAccreditationNumber(
                accreditationExternalId,
                materialExternalId);

            return Ok(hasNpwdAccreditationNumber);
        }

        [HttpPut("HasNpwdAccreditationNumber")]
        public async Task<IActionResult> UpdateHasNpwdAccreditationNumber(
            Guid accreditationExternalId,
            Guid materialExternalId,
            [FromBody] bool hasNpwdAccreditationNumber)
        {
            await _accreditationMaterialService.UpdateHasNpwdAccreditationNumber(
                accreditationExternalId,
                materialExternalId,
                hasNpwdAccreditationNumber);

            return Ok();
        }
    }
}
