using EPR.Accreditation.Facade.Common.Dtos;
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
            Guid materialExternalId);

        Task UpdateAccreditationMaterial(
            SiteType siteType,
            Guid accreditationExternalId,
            Guid materialExternalId,
            DTO.AccreditationMaterial accreditationMaterial);

        Task<DTO.Accreditation> GetAccreditation(
            Guid accreditationExternalId);

        Task UpdateAccreditation(
            Guid accreditationExternalId,
            DTO.Accreditation accreditation);

        Task<CheckYourAnswersDto> GetCheckYourAnswers(Guid accreditationExternalId);

        Task<List<Dtos.AccreditationTaskProgress>> GetTaskProgress(
            Guid accreditationExternalId);

        Task<AccreditationMaterial> GetLastCalendarYearWaste(Guid accreditationExternalId, Guid accreditationMaterialExternalId);

        Task SetHasOverseasAgent(Guid accreditationExternalId, bool? hasOverseasAgent);
    }
}
