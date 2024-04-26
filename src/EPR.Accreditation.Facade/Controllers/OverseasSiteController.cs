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

        public OverseasSiteController(IOverseasSiteService overseasSiteService)
        {
            _overseasSiteService = overseasSiteService ?? throw new ArgumentNullException(nameof(overseasSiteService));
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
    }
}
