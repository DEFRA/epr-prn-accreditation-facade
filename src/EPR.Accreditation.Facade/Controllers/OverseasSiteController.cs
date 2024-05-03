using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/Accreditation/{id}/OverseasSite/{overseasSiteId}")]
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
            Guid id,
            Guid overseasSiteId)
        {
            var reprocessorDetails = await _overseasSiteService.GetReprocessorDetails(
                id,
                overseasSiteId);

            return Ok(reprocessorDetails);
        }

        [HttpPut("ReprocessorDetails")]
        public async Task<IActionResult> UpdateReprocessorDetails(
            Guid id,
            Guid overseasSiteId,
            [FromBody] ReprocessorDetailsDto reprocessorDetails)
        {
            await _overseasSiteService.UpdateReprocessorDetails(
                id,
                overseasSiteId,
                reprocessorDetails);

            return Ok();
        }

        [HttpGet("Outputs")]
        [ProducesResponseType(typeof(OverseasReprocessingSiteOutputs), 200)]
        public async Task<IActionResult> GetOverseasSiteOutputs(Guid id, Guid overseasSiteId)
        {
            var dto = await _accreditationService.GetOverseasReprocessingSiteOutputs(id, overseasSiteId);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        [HttpPut("Outputs")]
        public async Task<IActionResult> UpdateSite(
            Guid? id,
            Guid? overseasSiteId,
            [FromBody] OverseasReprocessingSiteOutputs overseasReprocessingSiteOutputs)
        {
            if(id == null || overseasSiteId == null)
            {
                return NotFound();
            }

            await _accreditationService.UpdateOverseasReprocessingSiteOutputs(
                id.Value,
                overseasSiteId.Value,
                overseasReprocessingSiteOutputs);

            return Ok();
        }
    }
}
