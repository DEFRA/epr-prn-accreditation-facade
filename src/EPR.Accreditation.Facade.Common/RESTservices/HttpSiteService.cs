using EPR.Accreditation.Facade.Common.Dtos;
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

        public async Task<Site> GetSite(
            Guid accreditationExternalId,
            Guid siteExternalId)
        {
            return await Get<Site>($"{accreditationExternalId}/Site/{siteExternalId}");
        }

        public async Task UpdateSite(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Site site)
        {
            await Put($"{accreditationExternalId}/Site/{siteExternalId}", site);
        }
    }
}
