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

        Task<ReprocessingSupportingInformationDto> GetReprocessorSupportingInformation(
            Guid accreditationExternalId,
            Guid materialExternalId,
            ReprocessorSupportingInformationType reprocessorSupportingInformationType);

        Task UpdateReprocessorSupportingInformation(
            Guid accreditationExternalId,
            Guid materialExternalId,
            ReprocessingSupportingInformationDto materialOutputsDto,
            ReprocessorSupportingInformationType reprocessorSupportingInformationType);

        Task<MaterialOutputsDto> GetMaterialOutputs(
            Guid accreditationExternalId,
            Guid materialExternalId);

        Task UpdateMaterialOutputs(
            Guid accreditationExternalId,
            Guid materialExternalId,
            MaterialOutputsDto materialOutputsDto);

        Task<MaterialWasteOutputsDto> GetMaterialWasteOutputs(
            Guid accreditationExternalId,
            Guid materialExternalId);

        Task UpdateMaterialWasteOutputs(
            Guid accreditationExternalId,
            Guid materialExternalId,
            MaterialWasteOutputsDto materialWasteOutputsDto);

        Task<CheckYourAnswersDto> GetCheckYourAnswers(Guid accreditationExternalId);

        Task<List<AccreditationTaskProgress>> GetTaskProgress(
            Guid accreditationExternalId);
    }
}
