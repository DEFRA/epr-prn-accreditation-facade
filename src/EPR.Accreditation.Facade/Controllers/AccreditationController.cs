using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccreditationController : ControllerBase
    {
        protected readonly IHttpAccreditationService _httpAccreditationService;

        public AccreditationController(IHttpAccreditationService httpAccreditationService)
        {
            _httpAccreditationService = httpAccreditationService ?? throw new ArgumentNullException(nameof(httpAccreditationService));
        }

        [HttpGet("{accreditationExternalId}/Site/{siteExternalId}/Material/{materialExternalId}")]
        public async Task<IActionResult> GetWasteSource(
            Guid accreditationExternalId, 
            Guid siteExternalId,
            Guid materialExternalId)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                accreditationExternalId,
                siteExternalId,
                materialExternalId);

            if (siteMaterial == null)
                return NotFound();

            return Ok(siteMaterial.WasteSource);
        }

        [HttpPut("{accreditationExternalId}/Site/{siteExternalId}/Material/{materialExternalId}")]
        public async Task<IActionResult> SaveWasteSource(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            string wasteSource)
        {
            var siteMaterial = new AccreditationMaterial
            {
                WasteSource = wasteSource
            };

            await _httpAccreditationService.UpdateAccreditationMaterial(
                accreditationExternalId,
                siteExternalId,
                materialExternalId,
                siteMaterial);

            return Ok();
        }
    }
}
