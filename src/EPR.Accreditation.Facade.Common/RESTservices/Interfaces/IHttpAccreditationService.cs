using EPR.Accreditation.Facade.Common.Enums;
using DTO = EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    public interface IHttpAccreditationService
    {
        Task<OperatorType> GetOperatorType(Guid accreditationExternalId);

        Task<Guid> CreateAccreditation(DTO.Accreditation accreditation);

        Task<DTO.AccreditationMaterial> GetAccreditationMaterial(
            SiteType siteType,
            Guid accreditationExternalId,
            Guid? siteExternalId,
            Guid materialExternalId);

        Task UpdateAccreditationMaterial(
            SiteType siteType,
            Guid accreditationExternalId,
            Guid? siteExternalId,
            Guid materialExternalId,
            DTO.AccreditationMaterial accreditationMaterial);

        Task<DTO.Accreditation> GetAccreditation(
            Guid accreditationExternalId);

        Task UpdateAccreditation(
            Guid accreditationExternalId,
            DTO.Accreditation accreditation);
    }
}
