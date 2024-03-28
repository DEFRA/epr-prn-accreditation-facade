namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/[controller]/{accreditationExternalId}/")]
    public class AccreditationController : ControllerBase
    {
        protected readonly IAccreditationService _accreditationService;
        protected readonly IWastePermitService _wastePermitService;
        protected readonly IAccreditationMaterialService _accreditationMaterialService;

        public AccreditationController(
            IAccreditationService accreditationService,
            IWastePermitService wastePermitService,
            IAccreditationMaterialService accreditationMaterialService)
        {
            _accreditationService = accreditationService ?? throw new ArgumentNullException(nameof(accreditationService));
            _wastePermitService = wastePermitService ?? throw new ArgumentNullException(nameof(wastePermitService));
            _accreditationMaterialService = accreditationMaterialService ?? throw new ArgumentNullException(nameof(accreditationMaterialService));
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

        [HttpGet("Site/{siteExternalId}/Material/{materialExternalId}/WasteSource")]
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

        [HttpPut("Site/{siteExternalId}/Material/{materialExternalId}/WasteSource")]
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

        [HttpGet("Site/{siteExternalId}/Material/{materialExternalId}/Name")]
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

        [HttpGet("Site/{siteExternalId}/Material/{materialExternalId}/MaterialOutputs")]
        public async Task<IActionResult> GetMaterialOutputs(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId)
        {
            var materialOoutputs = await _accreditationService.GetMaterialOutputs(
                accreditationExternalId,
                siteExternalId,
                materialExternalId);

            return Ok(materialOoutputs);
        }

        [HttpPut("Site/{siteExternalId}/Material/{materialExternalId}/MaterialOutputs")]
        public async Task<IActionResult> UpdateMaterialOutputs(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            [FromBody] MaterialOutputsDto materialOutputsDto)
        {
            await _accreditationService.UpdateMaterialOutputs(
                accreditationExternalId,
                siteExternalId,
                materialExternalId,
                materialOutputsDto);

            return Ok();
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

        [HttpGet("Site/{siteExternalId}/Material/{materialExternalId}/WasteLastYear")]
        public async Task<IActionResult> GetReprocessedWasteLastYear(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId)
        {
            var reprocessedWasteLastYear = await _accreditationMaterialService.GetReprocessedWasteLastYear(
                accreditationExternalId,
                siteExternalId,
                materialExternalId);

            return Ok(reprocessedWasteLastYear);
        }

        [HttpPut("Site/{siteExternalId}/Material/{materialExternalId}/WasteLastYear")]
        public async Task<IActionResult> UpdateReprocessedWasteLastYear(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            [FromBody] ReprocessedWasteLastYear reprocessedWasteLastYear)
        {
            await _accreditationMaterialService.UpdateReprocessedWasteLastYear(
                accreditationExternalId,
                siteExternalId,
                materialExternalId,
                reprocessedWasteLastYear);

            return Ok();
        }
    }
}
