using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/[controller]/{accreditationExternalId}")]
    public class SaveAndComeBackController : ControllerBase
    {
        protected readonly ISaveAndComeBackService _saveAndComeBackService;

        public SaveAndComeBackController(ISaveAndComeBackService saveAndComeBackService)
        {
            _saveAndComeBackService = saveAndComeBackService ?? throw new ArgumentNullException(nameof(saveAndComeBackService));
        }

        [HttpGet]
        public async Task<IActionResult> GetSaveAndComeBack(Guid accreditationExternalId)
        {
            var saveAndComeBack = await _saveAndComeBackService.GetSaveAndComeBack(accreditationExternalId);

            return Ok(saveAndComeBack);
        }

        [HttpGet("/api/[controller]/HasApplicationSaved/{accreditationExternalId}")]
        public async Task<IActionResult> GetHasApplicationSaved(Guid accreditationExternalId)
        {
            var hasApplicationSaved = await _saveAndComeBackService.GetHasApplicationSaved(accreditationExternalId);

            return Ok(hasApplicationSaved);
        }

        [HttpPost]
        public async Task<IActionResult> AddSaveAndComeBack(
            Guid accreditationExternalId,
            [FromBody] SaveAndComeBack saveAndComeBack)
        {
            await _saveAndComeBackService.AddSaveAndComeBack(
                accreditationExternalId,
                saveAndComeBack);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSaveAndComeBack(Guid accreditationExternalId)
        {
            await _saveAndComeBackService.DeleteSaveAndComeBack(accreditationExternalId);

            return Ok();
        }
    }
}
