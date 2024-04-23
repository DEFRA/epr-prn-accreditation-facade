using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EPR.Accreditation.Facade.Common.RESTservices
{
    public class HttpOverseasSiteService : BaseHttpService, IHttpOverseasSiteService
    {
        public HttpOverseasSiteService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName) : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task<OverseasReprocessingSite> GetOverseasReprocessingSite(
            Guid id,
            Guid overseasSiteId)
        {
            return await Get<OverseasReprocessingSite>($"{id}/OverseasSite/{overseasSiteId}");
        }

        public async Task UpdateOverseasReprocessingSite(
            Guid id,
            Guid overseasSiteId,
            OverseasReprocessingSite overseasReprocessingSite)
        {
            await Put($"{id}/OverseasSite/{overseasSiteId}", overseasReprocessingSite);
        }
    }
}
