using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/[controller]/{accreditationExternalId}")]
    public class SaveAndContinueController : ControllerBase
    {
        protected readonly ISaveAndContinueService _saveAndContinueService;

        public SaveAndContinueController(ISaveAndContinueService saveAndContinueService)
        {
            _saveAndContinueService = saveAndContinueService ?? throw new ArgumentNullException(nameof(saveAndContinueService));
        }

        [HttpGet]
        public async Task<IActionResult> GetSaveAndContinue(Guid accreditationExternalId)
        {
            var saveAndContinue = await _saveAndContinueService.GetSaveAndContinue(accreditationExternalId);

            return Ok(saveAndContinue);
        }

        [HttpGet("/api/[controller]/HasApplicationSaved/{accreditationExternalId}")]
        public async Task<IActionResult> GetHasApplicationSaved(Guid accreditationExternalId)
        {
            var hasApplicationSaved = await _saveAndContinueService.GetHasApplicationSaved(accreditationExternalId);

            return Ok(hasApplicationSaved);
        }

        [HttpPost]
        public async Task<IActionResult> AddSaveAndContinue(
            Guid accreditationExternalId,
            [FromBody] SaveAndContinue saveAndContinue)
        {
            await _saveAndContinueService.AddSaveAndContinue(
                accreditationExternalId,
                saveAndContinue);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSaveAndContinue(Guid accreditationExternalId)
        {
            await _saveAndContinueService.DeleteSaveAndContinue(accreditationExternalId);

            return Ok();
        }
    }
}
