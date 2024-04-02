using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;

namespace EPR.Accreditation.Facade.Services
{
    public class SiteService : ISiteService
    {
        protected readonly IHttpSiteService _httpExemptionReferenceService;

        public SiteService(IHttpSiteService httpExemptionReferenceService)
        {
            _httpExemptionReferenceService = httpExemptionReferenceService ?? throw new ArgumentNullException(nameof(httpExemptionReferenceService));
        }

        public async Task<int> CreateExemptionReference(
            int siteId,
            ExemptionReference exemptionReference)
        {
            return await _httpExemptionReferenceService.CreateExemptionReference(
                siteId,
                exemptionReference);
        }

        public async Task<ExemptionReference> GetExemptionReference(
            int exemptionReferenceId,
            int siteId)
        {
            return await _httpExemptionReferenceService.GetExemptionReference(
                exemptionReferenceId,
                siteId);
        }

        public async Task UpdateExemptionReference(
            int exemptionReferenceId,
            int siteId,
            ExemptionReference exemptionReference)
        {
            var updatedExemptionReference = new ExemptionReference
            {
                Reference = exemptionReference.Reference
            };

            await _httpExemptionReferenceService.UpdateExemptionReference(
                exemptionReferenceId,
                siteId,
                updatedExemptionReference);
        }
    }
}
