using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EPR.Accreditation.Facade.Common.RESTservices
{
    public class HttpSiteService : BaseHttpService, IHttpSiteService
    {
        public HttpSiteService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName) : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task<ExemptionReference> GetExemptionReference(
            int exemptionReferenceId,
            int siteId)
        {
            return await Get<ExemptionReference>($"{siteId}/ExemptionReference/{exemptionReferenceId}");
        }

        public async Task UpdateExemptionReference(
            int exemptionReferenceId,
            int siteId,
            ExemptionReference exemptionReference)
        {
            await Put($"{siteId}/ExemptionReference/{exemptionReferenceId}", exemptionReference);
        }
    }
}
