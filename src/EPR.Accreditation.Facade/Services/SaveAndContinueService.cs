using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;

namespace EPR.Accreditation.Facade.Services
{
    public class SaveAndContinueService : ISaveAndContinueService
    {
        protected readonly IHttpSaveAndContinueService _httpSaveAndContinueService;

        public SaveAndContinueService(IHttpSaveAndContinueService httpSaveAndContinueService)
        {
            _httpSaveAndContinueService = httpSaveAndContinueService ?? throw new ArgumentNullException(nameof(httpSaveAndContinueService));
        }

        public async Task AddSaveAndContinue(
            Guid accreditationExternalId,
            SaveAndContinue saveAndContinue)
        {
            await _httpSaveAndContinueService.AddSaveAndContinue(
                accreditationExternalId,
                saveAndContinue);
        }

        public async Task DeleteSaveAndContinue(Guid accreditationExternalId)
        {
            await _httpSaveAndContinueService.DeleteSaveAndContinue(accreditationExternalId);
        }

        public async Task<bool> GetHasApplicationSaved(Guid accreditationExternalId)
        {
            return await _httpSaveAndContinueService.GetHasApplicationSaved(accreditationExternalId);
        }

        public async Task<SaveAndContinue> GetSaveAndContinue(Guid accreditationExternalId)
        {
            return await _httpSaveAndContinueService.GetSaveAndContinue(accreditationExternalId);
        }
    }
}
