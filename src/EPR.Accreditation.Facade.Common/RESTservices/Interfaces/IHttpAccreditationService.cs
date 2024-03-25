using Dto = EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    public interface IHttpAccreditationService
    {
        Task<Dto.Accreditation> GetAccreditation(Guid accreditationExternalId);

        Task<Dto.AccreditationMaterial> GetAccreditationMaterial(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId);

        Task UpdateAccreditationMaterial(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            Dto.AccreditationMaterial accreditationMaterial);
    }
}
