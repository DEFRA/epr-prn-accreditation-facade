using EPR.Accreditation.Facade.Common.Dtos.Portal;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface ISiteService
    {
        Task<int> CreateExemptionReference(
            int siteId,
            ExemptionReference exemptionReference);

        public Task<ExemptionReference> GetExemptionReference(
            int exemptionReferenceId,
            int siteId);

        public Task UpdateExemptionReference(
            int exemptionReferenceId,
            int siteId,
            ExemptionReference exemptionReference);
    }
}
