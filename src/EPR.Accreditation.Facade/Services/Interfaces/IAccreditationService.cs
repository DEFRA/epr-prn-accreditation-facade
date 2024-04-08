using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.Enums;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface IAccreditationService
    {
        Task<OperatorType> GetOperatorType(Guid accreditationExternalId);
        Task<Guid> CreateAccreditation(Common.Dtos.Accreditation accreditation);

        Task<string> GetWasteSource(
            SiteType siteType,
            Guid accreditationExternalId,
            Guid? siteExternalId,
            Guid materialExternalId);

        Task UpdateWasteSource(
            SiteType siteType,
            Guid accreditationExternalId,
            Guid? siteExternalId,
            Guid materialExternalId,
            string wasteSource);

        Task<string> GetWasteMaterialName(
            SiteType siteType,
            Guid accreditationExternalId,
            Guid? siteExternalId,
            Guid materialExternalId,
            Language language);

        Task CreateWastePermit(
            Guid accreditationExternalId,
            WastePermit workPermit);

        Task<WastePermit> GetWastePermit(
            Guid accreditationExternalId);

        Task<MaterialOutputsDto> GetMaterialOutputs(
            Guid accreditationExternalId,
            Guid materialExternalId);

        Task UpdateMaterialOutputs(
            Guid accreditationExternalId,
            Guid materialExternalId,
            MaterialOutputsDto materialOutputsDto);
        Task<CheckYourAnswersDto> GetCheckYourAnswers(Guid accreditationExternalId);
    }
}
