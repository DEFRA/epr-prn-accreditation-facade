using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface ISaveAndComeBackService
    {
        public Task<SaveAndComeBack> GetSaveAndComeBack(Guid accreditationExternalId);

        public Task<bool> GetHasApplicationSaved(Guid accreditationExternalId);

        public Task AddSaveAndComeBack(
            Guid accreditationExternalId,
            SaveAndComeBack saveAndComeBack);

        public Task DeleteSaveAndComeBack(Guid accreditationExternalId);
    }
}
