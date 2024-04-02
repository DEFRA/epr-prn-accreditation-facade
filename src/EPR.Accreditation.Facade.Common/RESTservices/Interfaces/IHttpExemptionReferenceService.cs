using EPR.Accreditation.Facade.Common.Dtos.Portal;

namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    public interface IHttpExemptionReferenceService
    {
        Task<ExemptionReference> GetExemptionReference(int siteId);
    }
}
