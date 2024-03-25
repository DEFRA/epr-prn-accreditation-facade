using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    public interface IHttpSaveAndContinueService
    {
        Task<SaveAndComeBack> GetSaveAndContinue(Guid accreditationExternalId);

        Task AddSaveAndContinue(
            Guid accreditationExternalId,
            SaveAndComeBack saveAndContinue);

        Task DeleteSaveAndContinue(Guid accreditationExternalId);
    }
}
