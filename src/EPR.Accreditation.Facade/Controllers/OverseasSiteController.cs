using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/Accreditation/{accreditationExternalId}/[controller]")]
    public class OverseasSiteController : ControllerBase
    {
        protected readonly IAccreditationService _accreditationService;

        public OverseasSiteController(
            IAccreditationService accreditationService)
        {
            _accreditationService = accreditationService ?? throw new ArgumentNullException(nameof(accreditationService));
        }

        [HttpGet("{siteExternalId}/Outputs")]
        [ProducesResponseType(typeof(OverseasReprocessingSiteOutputs), 200)]
        public async Task<IActionResult> GetOverseasSiteOutputs(Guid accreditationExternalId, Guid siteExternalId)
        {
            var dto = await _accreditationService.GetOverseasReprocessingSiteOutputs(accreditationExternalId,siteExternalId);

            if (dto == null)
                return NotFound();

            return Ok(dto);
        }

        [HttpPut("Outputs")]
        public async Task<IActionResult> UpdateSite(
            Guid accreditationExternalId, 
            [FromBody] OverseasReprocessingSiteOutputs overseasReprocessingSiteOutputs)
        {
            if (!overseasReprocessingSiteOutputs.ExternalId.HasValue)
            {
                return BadRequest("Missing over seas site id");
            }

            await _accreditationService.UpdateOverseasReprocessingSiteOutputs(accreditationExternalId, overseasReprocessingSiteOutputs);

            return Ok();
        }
    }
}
