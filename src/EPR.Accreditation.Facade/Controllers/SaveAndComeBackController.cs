using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/[controller]/{id}")]
    public class SaveAndComeBackController : ControllerBase
    {
        protected readonly ISaveAndComeBackService _saveAndComeBackService;

        public SaveAndComeBackController(ISaveAndComeBackService saveAndComeBackService)
        {
            _saveAndComeBackService = saveAndComeBackService ?? throw new ArgumentNullException(nameof(saveAndComeBackService));
        }

        [HttpGet]
        public async Task<IActionResult> GetSaveAndComeBack(Guid id)
        {
            var saveAndComeBack = await _saveAndComeBackService.GetSaveAndComeBack(id);

            return Ok(saveAndComeBack);
        }

        [HttpGet("/api/[controller]/HasApplicationSaved/{id}")]
        public async Task<IActionResult> GetHasApplicationSaved(Guid id)
        {
            var hasApplicationSaved = await _saveAndComeBackService.GetHasApplicationSaved(id);

            return Ok(hasApplicationSaved);
        }

        [HttpPost]
        public async Task<IActionResult> AddSaveAndComeBack(
            Guid id,
            [FromBody] SaveAndComeBack saveAndComeBack)
        {
            await _saveAndComeBackService.AddSaveAndComeBack(
                id,
                saveAndComeBack);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSaveAndComeBack(Guid id)
        {
            await _saveAndComeBackService.DeleteSaveAndComeBack(id);

            return Ok();
        }
    }
}
