using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;

namespace EPR.Accreditation.Facade.Services
{
    public class OverseasSiteService : IOverseasSiteService
    {
        protected readonly IHttpAccreditationService _httpAccreditationService;

        public OverseasSiteService(IHttpAccreditationService httpAccreditationService)
        {
            _httpAccreditationService = httpAccreditationService;
        }

        public async Task<OverseasReprocessingSite> GetOverseasSite(
            Guid externalId, 
            Guid siteId)
        {
            var overseasSite = await _httpAccreditationService.GetOverseasSite(externalId, siteId);
            return overseasSite;
        }

        public async Task<OverseasReprocessingSite> UpdateOverseasSite(Guid externalId, Guid siteId)
        {
            throw new NotImplementedException();
        }
    }
}
