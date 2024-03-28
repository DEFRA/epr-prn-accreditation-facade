using EPR.Accreditation.Facade.Common.Dtos.Portal;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface IAccreditationMaterialService
    {
        public Task<bool?> GetReprocessedWasteLastYear(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId);

        public Task UpdateReprocessedWasteLastYear(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            ReprocessedWasteLastYear reprocessedWasteLastYear);
    }
}
