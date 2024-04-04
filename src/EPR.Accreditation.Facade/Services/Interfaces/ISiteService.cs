using EPR.Accreditation.Facade.Common.Dtos.Portal;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface ISiteService
    {
        public Task<IEnumerable<string>> GetExemptionReferences(
            Guid accreditationExternalId,
            Guid externalSiteId);

        public Task UpdateExemptionReferences(
            Guid accreditationExternalId,
            Guid externalSiteId,
            IEnumerable<ExemptionReference> references);
    }
}
