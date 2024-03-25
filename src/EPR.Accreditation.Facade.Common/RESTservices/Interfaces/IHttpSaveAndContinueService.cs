using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    public interface IHttpSaveAndContinueService
    {
        Task<SaveAndContinue> GetSaveAndContinue(Guid accreditationExternalId);

        Task AddSaveAndContinue(
            Guid accreditationExternalId,
            SaveAndContinue saveAndContinue);

        Task DeleteSaveAndContinue(Guid accreditationExternalId);
    }
}
