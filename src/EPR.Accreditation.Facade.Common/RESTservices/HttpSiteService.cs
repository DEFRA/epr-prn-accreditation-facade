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


        public async Task<Dtos.Site> GetSite(
            Guid siteExternalId)
        {
            return await Get<Dtos.Site>($"{siteExternalId}/Site");
        }

        public async Task UpdateSite(
            Guid accreditationExternalId,
            Site site)
        {
            await Put($"{accreditationExternalId}/Site", site);
        }

        public async Task<Guid> CreateSite(
            Guid accreditationExternalId,
            Site site)
        {
            var externalId = await Post<Guid>($"{accreditationExternalId}/Site", site);
            return externalId;
        }
    }
}
