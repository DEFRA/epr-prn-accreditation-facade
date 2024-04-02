using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;

namespace EPR.Accreditation.Facade.Services
{
    public class ExemptionReferenceService : IExemptionReferenceService
    {
        protected readonly IHttpExemptionReferenceService _httpExemptionReferenceService;

        public ExemptionReferenceService(IHttpExemptionReferenceService httpExemptionReferenceService)
        {
            _httpExemptionReferenceService = httpExemptionReferenceService ?? throw new ArgumentNullException(nameof(httpExemptionReferenceService));
        }

        public async Task<ExemptionReference> GetExemptionReference(int siteId)
        {
            return await _httpExemptionReferenceService.GetExemptionReference(siteId);
        }
    }
}
