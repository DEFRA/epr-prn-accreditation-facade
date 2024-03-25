using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface ISaveAndContinueService
    {
        public Task<SaveAndContinue> GetSaveAndContinue(Guid accreditationExternalId);

        public Task<bool> GetHasApplicationSaved(Guid accreditationExternalId);

        public Task AddSaveAndContinue(
            Guid accreditationExternalId,
            SaveAndContinue saveAndContinue);

        public Task DeleteSaveAndContinue(Guid accreditationExternalId);
    }
}
