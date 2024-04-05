using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Enums;
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

        public async Task<Guid> CreateSite(Dtos.Site site)
        {
            var externalId = await Post<Guid>(site);
            return externalId;
        }

        public async Task<Dtos.Site> GetSite(
            Guid siteExternalId)
        {
            return await Get<Dtos.Site>($"{siteExternalId}/Site");
        }

        public async Task UpdateSite(
            Guid siteExternalId,
            Site site)
        {
            await Put($"Site/{siteExternalId}", site);
        }
    }
}