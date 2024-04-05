using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;

namespace EPR.Accreditation.Facade.Services
{
    public class OverseasSiteService : IOverseasSiteService
    {
        protected readonly IHttpAccreditationService _httpAccreditationService;

        public async Task<OverseasReprocessingSite> GetOverseasSite(
            Guid externalId, 
            Guid siteId)
        {
            //var overseasSite = _httpAccreditationService.

            throw new NotImplementedException();
        }
    }
}
