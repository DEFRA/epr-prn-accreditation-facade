using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

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


        [HttpGet("{accreditationExternalId}/OperatorType")]
        public async Task<IActionResult> GetOperatorType(
            Guid accreditationExternalId)
        {
            var operatorTypeId = await _accreditationService.GetOperatorType(accreditationExternalId);

            return Ok(operatorTypeId);
        }

        [HttpPut("{accreditationExternalId}/OperatorType/{operatorTypeId}")]
        public async Task<IActionResult> SaveOperatorType(
            Guid accreditationExternalId,
            Common.Enums.OperatorType operatorTypeId)
        {
            await _accreditationService.UpdateOperatorType(
                accreditationExternalId,
                operatorTypeId);

            return Ok();
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
            [FromBody] string wasteSource)
        {
            await _accreditationService.UpdateWasteSource(
                accreditationExternalId,
                siteExternalId,
                materialExternalId,
                wasteSource);

            return Ok();
        }

        [HttpGet("{accreditationExternalId}/Site/{siteExternalId}/Material/{materialExternalId}/Name")]
        public async Task<IActionResult> GetMaterialName(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            Language language)
        {
            if (language == Language.Undefined)
                return BadRequest("Invalid language selection. Must be either English(1) or Welsh(2)");

            var wasteSource = await _accreditationService.GetWasteMaterialName(
                accreditationExternalId,
                siteExternalId,
                materialExternalId,
                language);

            return Ok(wasteSource);
        }
    }
}
