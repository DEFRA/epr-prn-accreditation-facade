using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/Accreditation/{id}/Material/{materialId}")]
    public class AccreditationMaterialController : ControllerBase
    {
        protected readonly IAccreditationService _accreditationService;
        protected readonly IWastePermitService _wastePermitService;
        protected readonly IAccreditationMaterialService _accreditationMaterialService;

        public AccreditationMaterialController(
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
            Guid id,
            Guid materialId)
        {
            var wasteSource = await _accreditationService.GetWasteSource(
                SiteType.Site,
                id,
                materialId);

            return Ok(wasteSource);
        }

        [HttpPut("WasteSource")]
        public async Task<IActionResult> SaveWasteSource(
            Guid id,
            Guid materialId,
            [FromBody] string wasteSource)
        {
            await _accreditationService.UpdateWasteSource(
                SiteType.Site,
                id,
                materialId,
                wasteSource);

            return Ok();
        }

        [HttpGet("Name")]
        public async Task<IActionResult> GetMaterialName(
            Guid id,
            Guid materialId,
            Language language)
        {
            if (language == Language.Undefined)
                return BadRequest("Invalid language selection. Must be either English(1) or Welsh(2)");

            var wasteSource = await _accreditationService.GetWasteMaterialName(
                SiteType.Site,
                id,
                materialId,
                language);

            return Ok(wasteSource);
        }

        [HttpGet("NonWasteInputs")]
        public async Task<IActionResult> GetNonWasteInputs(
            Guid id,
            Guid materialId)
        {
            var nonWasteInputs = await _accreditationService.GetReprocessorSupportingInformation(
                id,
                materialId,
                ReprocessorSupportingInformationType.NonWasteInputs);

            return Ok(nonWasteInputs);
        }

        [HttpPut("NonWasteInputs")]
        public async Task<IActionResult> UpdateNonWasteInputs(
            Guid id,
            Guid materialId,
            [FromBody] ReprocessingSupportingInformationDto nonWasteInputsDto)
        {
            await _accreditationService.UpdateReprocessorSupportingInformation(
                id,
                materialId,
                nonWasteInputsDto,
                ReprocessorSupportingInformationType.NonWasteInputs);

            return Ok();
        }

        [HttpGet("ProductsProduced")]
        public async Task<IActionResult> GetProductsProduced(
            Guid id,
            Guid materialId)
        {
            var nonWasteInputs = await _accreditationService.GetReprocessorSupportingInformation(
                id,
                materialId,
                ReprocessorSupportingInformationType.ProductsProduced);

            return Ok(nonWasteInputs);
        }

        [HttpPut("ProductsProduced")]
        public async Task<IActionResult> UpdateProductsProduced(
            Guid id,
            Guid materialId,
            [FromBody] ReprocessingSupportingInformationDto nonWasteInputsDto)
        {
            await _accreditationService.UpdateReprocessorSupportingInformation(
                id,
                materialId,
                nonWasteInputsDto,
                ReprocessorSupportingInformationType.ProductsProduced);

            return Ok();
        }

        [HttpGet("MaterialOutputs")]
        public async Task<IActionResult> GetMaterialOutputs(
            Guid id,
            Guid materialId)
        {
            var materialOoutputs = await _accreditationService.GetMaterialOutputs(
                id,
                materialId);

            return Ok(materialOoutputs);
        }

        [HttpPut("MaterialOutputs")]
        public async Task<IActionResult> UpdateMaterialOutputs(
            Guid id,
            Guid materialId,
            [FromBody] MaterialOutputsDto materialOutputsDto)
        {
            await _accreditationService.UpdateMaterialOutputs(
                id,
                materialId,
                materialOutputsDto);

            return Ok();
        }

        [HttpGet("MaterialWasteInputs")]
        public async Task<IActionResult> GetMaterialWasteOutputs(
            Guid id,
            Guid materialId)
        {
            var materialInputs = await _accreditationService.GetMaterialWasteInputs(
                id,
                materialId);

            return Ok(materialInputs);
        }

        [HttpPut("MaterialWasteInputs")]
        public async Task<IActionResult> UpdateMaterialWasteOutputs(
            Guid id,
            Guid materialId,
            [FromBody] MaterialWasteInputsDto materialWasteInputsDto)
        {
            await _accreditationService.UpdateMaterialWasteInputs(
                id,
                materialId,
                materialWasteInputsDto);

            return Ok();
        }

        [HttpGet("WasteLastYear")]
        public async Task<IActionResult> GetReprocessedWasteLastYear(
            Guid id,
            Guid materialId)
        {
            var reprocessedWasteLastYear = await _accreditationMaterialService.GetReprocessedWasteLastYear(
                id,
                materialId);

            return Ok(reprocessedWasteLastYear);
        }

        [HttpPut("WasteLastYear")]
        public async Task<IActionResult> UpdateReprocessedWasteLastYear(
            Guid id,
            Guid materialId,
            [FromBody] ReprocessedWasteLastYear reprocessedWasteLastYear)
        {
            await _accreditationMaterialService.UpdateReprocessedWasteLastYear(
                id,
                materialId,
                reprocessedWasteLastYear);

            return Ok();
        }
    }
}
