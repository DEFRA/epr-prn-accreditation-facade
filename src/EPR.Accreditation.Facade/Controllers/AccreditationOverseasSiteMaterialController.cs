using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/Accreditation/{accreditationExternalId}/OverseasSite/{siteExternalId}/Material/{materialExternalId}")]
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
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId)
        {
            var wasteSource = await _accreditationService.GetWasteSource(
                SiteType.OverseasSite,
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
                SiteType.OverseasSite,
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
                SiteType.OverseasSite,
                accreditationExternalId,
                siteExternalId,
                materialExternalId,
                language);

            return Ok(wasteSource);
        }
    }
}
