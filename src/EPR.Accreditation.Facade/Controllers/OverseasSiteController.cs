using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/Accreditation/{accreditationExternalId}/OverseasSite/{overseasSiteExternalId}")]
    public class OverseasSiteController : ControllerBase
    {
        protected readonly IOverseasSiteService _overseasSiteService;
        private readonly IAccreditationService _accreditationService;

        public OverseasSiteController(
            IOverseasSiteService overseasSiteService,
            IAccreditationService accreditationService)
        {
            _overseasSiteService = overseasSiteService ?? throw new ArgumentNullException(nameof(overseasSiteService));
            _accreditationService = accreditationService ?? throw new ArgumentNullException(nameof(accreditationService));
        }

        [HttpGet("ReprocessorDetails")]
        public async Task<IActionResult> GetReprocessorDetails(
            Guid accreditationExternalId,
            Guid overseasSiteExternalId)
        {
            var reprocessorDetails = await _overseasSiteService.GetReprocessorDetails(
                accreditationExternalId,
                overseasSiteExternalId);

            return Ok(reprocessorDetails);
        }

        [HttpPut("ReprocessorDetails")]
        public async Task<IActionResult> UpdateReprocessorDetails(
            Guid accreditationExternalId,
            Guid overseasSiteExternalId,
            [FromBody] ReprocessorDetailsDto reprocessorDetails)
        {
            await _overseasSiteService.UpdateReprocessorDetails(
                accreditationExternalId,
                overseasSiteExternalId,
                reprocessorDetails);

            return Ok();
        }

        [HttpGet("Outputs")]
        [ProducesResponseType(typeof(OverseasReprocessingSiteOutputs), 200)]
        public async Task<IActionResult> GetOverseasSiteOutputs(Guid accreditationExternalId, Guid overseasSiteExternalId)
        {
            var dto = await _accreditationService.GetOverseasReprocessingSiteOutputs(accreditationExternalId, overseasSiteExternalId);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        [HttpPut("Outputs")]
        public async Task<IActionResult> UpdateSite(
            Guid? accreditationExternalId,
            Guid? overseasSiteExternalId,
            [FromBody] OverseasReprocessingSiteOutputs overseasReprocessingSiteOutputs)
        {
            if(accreditationExternalId == null || overseasSiteExternalId == null)
            {
                return NotFound();
            }

            await _accreditationService.UpdateOverseasReprocessingSiteOutputs(
                accreditationExternalId.Value,
                overseasSiteExternalId.Value,
                overseasReprocessingSiteOutputs);

            return Ok();
        }
    }
}
