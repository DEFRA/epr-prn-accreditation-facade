using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Exceptions;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;

namespace EPR.Accreditation.Facade.Services
{
    public class SaveAndComeBackService : ISaveAndComeBackService
    {
        protected readonly IHttpSaveAndContinueService _httpSaveAndContinueService;

        public SaveAndComeBackService(IHttpSaveAndContinueService httpSaveAndContinueService)
        {
            _httpSaveAndContinueService = httpSaveAndContinueService ?? throw new ArgumentNullException(nameof(httpSaveAndContinueService));
        }

        public async Task AddSaveAndComeBack(
            Guid accreditationExternalId,
            SaveAndComeBack saveAndComeBack)
        {
            await _httpSaveAndContinueService.AddSaveAndContinue(
                accreditationExternalId,
                saveAndComeBack);
        }

        public async Task DeleteSaveAndComeBack(Guid accreditationExternalId)
        {
            await _httpSaveAndContinueService.DeleteSaveAndContinue(accreditationExternalId);
        }

        public async Task<bool> GetHasApplicationSaved(Guid accreditationExternalId)
        {
            try
            {
                await _httpSaveAndContinueService.GetSaveAndContinue(accreditationExternalId);

                return true;
            }
            catch (ResponseCodeException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return false;
                }

                throw;
            }
        }

        public async Task<SaveAndComeBack> GetSaveAndComeBack(Guid accreditationExternalId)
        {
            return await _httpSaveAndContinueService.GetSaveAndContinue(accreditationExternalId);
        }
    }
}
