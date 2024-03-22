using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class MaterialController : ControllerBase
    {
        protected readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService ?? throw new ArgumentNullException(nameof(materialService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Language language)
        {
            if (language == Language.Undefined)
                return BadRequest("Language is not specified. Must be either English or Welsh");

            return Ok(await _materialService.GetAll(language));
        }
    }
}
