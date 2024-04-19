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
            Guid accreditationExternalId,
            Guid overseasSiteExternalId)
        {
            return await Get<OverseasReprocessingSite>($"{accreditationExternalId}/OverseasSite/{overseasSiteExternalId}");
        }

        public async Task UpdateOverseasReprocessingSite(
            Guid accreditationExternalId,
            OverseasReprocessingSite overseasReprocessingSite)
        {
            await Put($"{accreditationExternalId}/OverseasSite", overseasReprocessingSite);
        }
    }
}
