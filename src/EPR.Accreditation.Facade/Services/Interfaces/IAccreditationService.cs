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
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId);

        Task UpdateWasteSource(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            string wasteSource);

        Task<string> GetWasteMaterialName(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            Language language);

        Task CreateWastePermit(
            Guid accreditationExternalId,
            WastePermit workPermit);

        Task<WastePermit> GetWastePermit(
            Guid accreditationExternalId);

        Task<MaterialOutputsDto> GetMaterialOutputs(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId);

        Task UpdateMaterialOutputs(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            MaterialOutputsDto materialOutputsDto);
    }
}
