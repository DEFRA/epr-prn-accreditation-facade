using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Exceptions;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;

namespace EPR.Accreditation.Facade.Services
{
    public class SaveAndComeBackService : ISaveAndComeBackService
    {
        protected readonly IHttpSaveAndComeBackService _httpSaveAndComeBackService;

        public SaveAndComeBackService(IHttpSaveAndComeBackService httpSaveAndComeBackService)
        {
            _httpSaveAndComeBackService = httpSaveAndComeBackService ?? throw new ArgumentNullException(nameof(httpSaveAndComeBackService));
        }

        public async Task AddSaveAndComeBack(
            Guid accreditationExternalId,
            SaveAndComeBack saveAndComeBack)
        {
            await _httpSaveAndComeBackService.DeleteSaveAndComeBack(accreditationExternalId);
            await _httpSaveAndComeBackService.AddSaveAndComeBack(
                accreditationExternalId,
                saveAndComeBack);
        }

        public async Task DeleteSaveAndComeBack(Guid accreditationExternalId)
        {
            await _httpSaveAndComeBackService.DeleteSaveAndComeBack(accreditationExternalId);
        }

        public async Task<bool> GetHasApplicationSaved(Guid accreditationExternalId)
        {
            try
            {
                await _httpSaveAndComeBackService.GetSaveAndComeBack(accreditationExternalId);

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
            return await _httpSaveAndComeBackService.GetSaveAndComeBack(accreditationExternalId);
        }
    }
}
