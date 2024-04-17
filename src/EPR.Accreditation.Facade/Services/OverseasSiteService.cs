using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Services.Interfaces;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;

namespace EPR.Accreditation.Facade.Services
{
    public class OverseasSiteService : IOverseasSiteService
    {
        protected readonly IHttpOverseasSiteService _httpOverseasSiteService;
        protected readonly IHttpAccreditationService _httpAccreditationService;

        public OverseasSiteService(IHttpAccreditationService httpAccreditationService)
        {
            _httpAccreditationService = httpAccreditationService;
        }

        public async Task<OverseasAddress> GetReprocessorDetails(
            Guid accreditationExternalId,
            Guid overseasSiteExternalId)
        public async Task<OverseasReprocessingSite> GetOverseasSite(
            Guid externalId, 
            Guid siteId)
        {
            var overseasSite = await _httpOverseasSiteService.GetOverseasReprocessingSite(
                accreditationExternalId,
                overseasSiteExternalId);

            if (overseasSite.OverseasAddress == null)
            {
                return null;
            }

            return overseasSite.OverseasAddress;
            var overseasSite = await _httpAccreditationService.GetOverseasSite(externalId, siteId);
            return overseasSite;
        }

        public async Task UpdateReprocessorDetails(
            Guid accreditationExternalId,
            Guid overseasSiteExternalId,
            OverseasAddress reprocessorDetails)
        {
            var overseasSite = new OverseasReprocessingSite
        public async Task<OverseasReprocessingSite> UpdateOverseasSite(Guid externalId, Guid siteId)
            {
                ExternalId = overseasSiteExternalId,
                OverseasAddress = reprocessorDetails
            };

            await _httpOverseasSiteService.UpdateOverseasReprocessingSite(
                accreditationExternalId,
                overseasSite);
            throw new NotImplementedException();
        }
    }
}
