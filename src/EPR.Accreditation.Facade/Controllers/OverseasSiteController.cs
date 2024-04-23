using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/Accreditation/{accreditationExternalId}/OverseasSite/{overseasSiteExternalId}")]
    public class OverseasSiteController : ControllerBase
    {
        protected readonly IOverseasSiteService _overseasSiteService;

        public OverseasSiteController(IOverseasSiteService overseasSiteService)
        {
            _overseasSiteService = overseasSiteService ?? throw new ArgumentNullException(nameof(overseasSiteService));
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
            [FromBody] OverseasAddress reprocessorDetails)
        {
            await _overseasSiteService.UpdateReprocessorDetails(
                accreditationExternalId,
                overseasSiteExternalId,
                reprocessorDetails);

            return Ok();
        }
    }
}
