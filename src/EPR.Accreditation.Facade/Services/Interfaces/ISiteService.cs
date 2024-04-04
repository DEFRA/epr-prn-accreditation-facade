using EPR.Accreditation.Facade.Common.Dtos.Portal;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface ISiteService
    {
        public Task<IEnumerable<string>> GetExemptionReferences(Guid accreditationExternalId);

        public Task UpdateExemptionReferences(
            Guid accreditationExternalId,
            IEnumerable<ExemptionReference> references);
    }
}
