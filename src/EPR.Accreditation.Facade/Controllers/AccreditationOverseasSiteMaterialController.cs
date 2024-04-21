namespace EPR.Accreditation.Facade.Controllers
{
    using EPR.Accreditation.Facade.Common.Enums;
    using EPR.Accreditation.Facade.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/Accreditation/{id}/OverseasSite/{siteId}/Material/{materialId}")]
    public class AccreditationOverseasSiteMaterialController : ControllerBase
    {
        protected readonly IAccreditationService _accreditationService;
        protected readonly IWastePermitService _wastePermitService;
        protected readonly IAccreditationMaterialService _accreditationMaterialService;

        public AccreditationOverseasSiteMaterialController(
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
            Guid siteId,
            Guid materialId)
        {
            var wasteSource = await _accreditationService.GetWasteSource(
                SiteType.OverseasSite,
                id,
                siteId,
                materialId);

            return Ok(wasteSource);
        }

        [HttpPut("WasteSource")]
        public async Task<IActionResult> SaveWasteSource(
            Guid id,
            Guid siteId,
            Guid materialId,
            [FromBody] string wasteSource)
        {
            await _accreditationService.UpdateWasteSource(
                SiteType.OverseasSite,
                id,
                siteId,
                materialId,
                wasteSource);

            return Ok();
        }

        [HttpGet("Name")]
        public async Task<IActionResult> GetMaterialName(
            Guid id,
            Guid siteId,
            Guid materialId,
            Language language)
        {
            if (language == Language.Undefined)
                return BadRequest("Invalid language selection. Must be either English(1) or Welsh(2)");

            var wasteSource = await _accreditationService.GetWasteMaterialName(
                SiteType.OverseasSite,
                id,
                siteId,
                materialId,
                language);

            return Ok(wasteSource);
        }

        /// <summary>
        /// Entry point to GET waste description codes for an application and
        /// material
        /// </summary>
        /// <param name="id">Accreditation id</param>
        /// <param name="siteId">The overseas site id</param>
        /// <param name="materialId">Material id</param>
        /// <returns>DTO containing list of waste description codes</returns>
        [HttpGet("WasteDescriptionCodes")]
        public async Task<IActionResult> GetWasteDescriptionCodes(
            Guid id,
            Guid siteId,
            Guid materialId)
        {
            var wasteDescriptionCodes = await _accreditationMaterialService.GetWasteDescriptionCodes(
                id,
                siteId,
                materialId);

            if (wasteDescriptionCodes == null)
            {
                return NotFound();
            }

            return Ok(wasteDescriptionCodes);
        }

        /// <summary>
        /// Entry point to POST waste description codes for an application and
        /// material
        /// </summary>
        /// <param name="id">Accreditation id</param>
        /// <param name="siteId">The overseas site id</param>
        /// <param name="materialId">Material id</param>
        /// <returns>Ok</returns>
        [HttpPost("WasteDescriptionCodes")]
        public async Task<IActionResult> UpdateWasteDescriptionCodes(
            Guid id,
            Guid siteId,
            Guid materialId,
            IEnumerable<string> wasteDescriptionCodes)
        {
            await _accreditationMaterialService.UpdateWasteDescriptionCodes(
                id,
                siteId,
                materialId,
                wasteDescriptionCodes);

            return Ok();
        }
    }
}
