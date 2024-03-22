using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.RESTservices;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccreditationController : ControllerBase
    {
        protected readonly IAccreditationService _accreditationService;

        public AccreditationController(IAccreditationService accreditationService)
        {
            _accreditationService = accreditationService ?? throw new ArgumentNullException(nameof(accreditationService));
        }

        [HttpGet("{accreditationExternalId}/Site/{siteExternalId}/Material/{materialExternalId}")]
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

        [HttpPut("{accreditationExternalId}/Site/{siteExternalId}/Material/{materialExternalId}")]
        public async Task<IActionResult> SaveWasteSource(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            string wasteSource)
        {
            await _accreditationService.UpdateWasteSource(
                accreditationExternalId,
                siteExternalId,
                materialExternalId,
                wasteSource);

            return Ok();
        }
    }
}
