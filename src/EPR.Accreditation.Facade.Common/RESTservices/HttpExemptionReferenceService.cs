using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EPR.Accreditation.Facade.Common.RESTservices
{
    public class HttpExemptionReferenceService : BaseHttpService, IHttpExemptionReferenceService
    {
        public HttpExemptionReferenceService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName) : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task<ExemptionReference> GetExemptionReference(int siteId)
        {
            return await Get<ExemptionReference>($"{siteId}");
        }
    }
}
