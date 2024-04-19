using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/[controller]/{accreditationExternalId}/")]
    public class AccreditationController : ControllerBase
    {
        protected readonly IAccreditationService _accreditationService;
        protected readonly IWastePermitService _wastePermitService;
        protected readonly ISiteService _siteService;
        protected readonly IAccreditationMaterialService _accreditationMaterialService;

        public AccreditationController(
            IAccreditationService accreditationService,
            IWastePermitService wastePermitService,
            ISiteService siteService,
            IAccreditationMaterialService accreditationMaterialService)
        {
            _accreditationService = accreditationService ?? throw new ArgumentNullException(nameof(accreditationService));
            _wastePermitService = wastePermitService ?? throw new ArgumentNullException(nameof(wastePermitService));
            _siteService = siteService ?? throw new ArgumentNullException(nameof(siteService));
            _accreditationMaterialService = accreditationMaterialService ?? throw new ArgumentNullException(nameof(accreditationMaterialService));
        }

        [HttpGet("CheckYourAnswers")]
        public async Task<IActionResult> GetCheckYourAnswers(
            Guid accreditationExternalId)
        {
            var checkYourAnswersDto = await _accreditationService.GetCheckYourAnswers(accreditationExternalId);

            return Ok(checkYourAnswersDto);
        }
        
	    [HttpGet("CheckAnswers")]
        public async Task<IActionResult> GetCheckAnswers(
            Guid accreditationExternalId, CheckAnswersSection section)
        {
            var checkYourAnswersDto = null//await _accreditationService.GetCheckAnswers(accreditationExternalId);

            return Ok(checkYourAnswersDto);
        }      

        [HttpGet("OperatorType")]
        public async Task<IActionResult> GetOperatorType(
            Guid accreditationExternalId)
        {
            var operatorTypeId = await _accreditationService.GetOperatorType(accreditationExternalId);

            return Ok(operatorTypeId);
        }

        [HttpPost]
        [Route("/api/[controller]")]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<IActionResult> CreateAccreditation([FromBody] Common.Dtos.Accreditation accreditation)
        {
            var externalId = await _accreditationService.CreateAccreditation(accreditation);

            return Ok(externalId);
        }

        [HttpPost("WastePermit")]
        public async Task<IActionResult> CreateWastePermit(
            Guid accreditationExternalId,
            Common.Dtos.WastePermit wastePermit)
        {
            await _accreditationService.CreateWastePermit(accreditationExternalId, wastePermit);

            return Ok();
        }

        [HttpGet("WastePermit")]
        public async Task<IActionResult> GetWastePermit(Guid accreditationExternalId)
        {
            var wastePermit = await _accreditationService.GetWastePermit(accreditationExternalId);

            return Ok(wastePermit);
        }

        [HttpGet("WastePermitExemption")]
        public async Task<IActionResult> GetHasPermitExemption(Guid accreditationExternalId)
        {
            var hasPermitExemption = await _wastePermitService.GetHasPermitExemption(accreditationExternalId);

            return Ok(hasPermitExemption);
        }

        [HttpPut("WastePermitExemption")]
        public async Task<IActionResult> UpdatePermitExemption(
            Guid accreditationExternalId,
            [FromBody] PermitExemption permitExemption)
        {
            await _wastePermitService.UpdatePermitExemption(
                accreditationExternalId,
                permitExemption);

            return Ok();
        }

        [HttpGet("Site")]
        public async Task<IActionResult> GetSite(
            Guid accreditationExternalId)
        {
            var site = await _siteService.GetSite(accreditationExternalId);

            return Ok(site);
        }

        [HttpGet("TaskProgress")]
        public async Task<IActionResult> GetTaskProgress(
            Guid accreditationExternalId)
        {
            var taskProgress = await _accreditationService.GetTaskProgress(accreditationExternalId);

            return Ok(taskProgress);
        }

        [HttpGet("LastCalendarYearWaste")]
        public async Task<IActionResult> LastCalendarYearWaste(
            Guid accreditationExternalId,
            Guid accreditationMaterialExternalId)
        {
            MaterialReprocessorDetails materialReprocessorDetails = await _accreditationMaterialService.GetReprocessedWasteLastYearData(
                accreditationExternalId, 
                accreditationMaterialExternalId);

            return Ok(materialReprocessorDetails);
        }

        [HttpGet("HasOverseasAgent")]
        public async Task<IActionResult> GetHasOverseasAgent(
            Guid accreditationExternalId)
        {
            var hasOverseasAgent = await _accreditationService.GetHasOverseasAgent(accreditationExternalId);

            return Ok(hasOverseasAgent);
        }

        [HttpPut("HasOverseasAgent")]
        public async Task<IActionResult> SetHasOverseasAgent(
            Guid accreditationExternalId,
            [FromBody] bool? hasOverseasAgent)
        {
            await _accreditationService.SetHasOverseasAgent(accreditationExternalId, hasOverseasAgent);

            return Ok();
        }
    }
}
