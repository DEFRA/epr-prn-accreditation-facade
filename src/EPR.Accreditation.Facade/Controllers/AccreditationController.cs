namespace EPR.Accreditation.Facade.Controllers
{
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Facade.Common.Dtos.Portal;
    using EPR.Accreditation.Facade.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/[controller]/{id}/")]
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
            Guid id)
        {
            var checkYourAnswersDto = await _accreditationService.GetCheckYourAnswers(id);

            return Ok(checkYourAnswersDto);
        }

        [HttpGet("OperatorType")]
        public async Task<IActionResult> GetOperatorType(
            Guid id)
        {
            var operatorTypeId = await _accreditationService.GetOperatorType(id);

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
            Guid id,
            Common.Dtos.WastePermit wastePermit)
        {
            await _accreditationService.CreateWastePermit(id, wastePermit);

            return Ok();
        }

        [HttpGet("WastePermit")]
        public async Task<IActionResult> GetWastePermit(Guid id)
        {
            var wastePermit = await _accreditationService.GetWastePermit(id);

            return Ok(wastePermit);
        }

        [HttpGet("WastePermitExemption")]
        public async Task<IActionResult> GetHasPermitExemption(Guid id)
        {
            var hasPermitExemption = await _wastePermitService.GetHasPermitExemption(id);

            return Ok(hasPermitExemption);
        }

        [HttpPut("WastePermitExemption")]
        public async Task<IActionResult> UpdatePermitExemption(
            Guid id,
            [FromBody] PermitExemption permitExemption)
        {
            await _wastePermitService.UpdatePermitExemption(
                id,
                permitExemption);

            return Ok();
        }

        [HttpGet("TaskProgress")]
        public async Task<IActionResult> GetTaskProgress(
            Guid id)
        {
            var taskProgress = await _accreditationService.GetTaskProgress(id);

            return Ok(taskProgress);
        }

        [HttpGet("LastCalendarYearWaste")]
        public async Task<IActionResult> LastCalendarYearWaste(
            Guid id,
            Guid accreditationMaterialExternalId)
        {
            MaterialReprocessorDetails materialReprocessorDetails = await _accreditationMaterialService.GetReprocessedWasteLastYearData(
                id,
                accreditationMaterialExternalId);

            return Ok(materialReprocessorDetails);
        }

        [HttpGet("HasOverseasAgent")]
        public async Task<IActionResult> GetHasOverseasAgent(
            Guid id)
        {
            var hasOverseasAgent = await _accreditationService.GetHasOverseasAgent(id);

            return Ok(hasOverseasAgent);
        }

        [HttpPut("HasOverseasAgent")]
        public async Task<IActionResult> SetHasOverseasAgent(
            Guid id,
            [FromBody] bool? hasOverseasAgent)
        {
            await _accreditationService.SetHasOverseasAgent(id, hasOverseasAgent);

            return Ok();
        }

        [HttpGet("Site")]
        public async Task<IActionResult> GetSite(
            Guid id)
        {
            var site = await _siteService.GetSite(id);

            return Ok(site);
        }

        [HttpPost("Site")]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<IActionResult> CreateSite(
            Guid id,
            [FromBody]AddressDto siteAddress)
        {
            var externalId = await _siteService.CreateSite(id, siteAddress);

            return Ok(externalId);
        }

        [HttpPut("Site")]
        public async Task<IActionResult> UpdateSite(
            Guid id,
            [FromBody]AddressDto siteAddress)
        {
            await _siteService.UpdateSite(id, siteAddress);

            return Ok();
        }

        [HttpGet("LegalDocumentAddress")]
        public async Task<IActionResult> GetLegalDocumentsAddress(Guid id)
        {
            var addressDto = await _accreditationService.GetLegalDocumentsAddress(id);

            return Ok(addressDto);
        }

        [HttpPut("LegalDocumentAddress")]
        public async Task<IActionResult> SaveLegalDocumentsAddress(
            Guid id,
            AddressDto addressDto)
        {
            await _accreditationService.UpdateLegalDocumentsAddress(id, addressDto);

            return Ok();
        }

        /// <summary>
        /// Returns PRN tonnage data for given accreditation.
        /// </summary>
        /// <param name="id">Accreditation id.</param>
        /// <returns>PRN data dto.</returns>
        [HttpGet("PrnTonnesPlanned")]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<IActionResult> GetPrnTonnesPlanned(
            Guid id)
        {
            var externalId = await _accreditationService.GetPrnTonnesPlanned(id);

            return Ok(externalId);
        }

        /// <summary>
        /// Updates PRN tonnage data for given accreditation.
        /// </summary>
        /// <param name="id">Accreditation id.</param>
        /// <param name="prnTonnesPlannedDto">PRN data dto.</param>
        /// <returns>Task.</returns>
        [HttpPut("PrnTonnesPlanned")]
        public async Task<IActionResult> UpdatePrnTonnesPlanned(
            Guid id,
            [FromBody] PrnTonnesPlannedDto prnTonnesPlannedDto)
        {
            await _accreditationService.UpdatePrnTonnesPlanned(id, prnTonnesPlannedDto);

            return Ok();
        }
    }
}
