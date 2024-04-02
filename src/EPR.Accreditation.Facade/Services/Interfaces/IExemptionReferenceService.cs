using EPR.Accreditation.Facade.Common.Dtos.Portal;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface IExemptionReferenceService
    {
        public Task<ExemptionReference> GetExemptionReference(int siteId);
    }
}
