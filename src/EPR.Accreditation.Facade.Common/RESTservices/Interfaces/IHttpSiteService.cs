using EPR.Accreditation.Facade.Common.Dtos.Portal;

namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    public interface IHttpSiteService
    {
        Task<int> CreateExemptionReference(
            int siteId,
            ExemptionReference exemptionReference);

        Task<ExemptionReference> GetExemptionReference(
            int exemptionReferenceId,
            int siteId);

        Task UpdateExemptionReference(
            int exemptionReferenceId,
            int siteId,
            ExemptionReference exemptionReference);
    }
}
