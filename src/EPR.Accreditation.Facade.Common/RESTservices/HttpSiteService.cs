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
            Guid id,
            Guid? overseasSiteId = null)
        {
            var siteParam = overseasSiteId.HasValue ? $"OverseasSite/{overseasSiteId}" : "Site";
            return await Get<Dtos.Site>($"{id}/{siteParam}");
        }

        public async Task UpdateSite(
            Guid id,
            Site site)
        {
            await Put($"{id}/Site", site);
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
