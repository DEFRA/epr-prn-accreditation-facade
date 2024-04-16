using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Services.Interfaces;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;

namespace EPR.Accreditation.Facade.Services
{
    public class OverseasSiteService : IOverseasSiteService
    {
        protected readonly IHttpOverseasSiteService _httpOverseasSiteService;

        public OverseasSiteService(IHttpOverseasSiteService httpOverseasSiteService)
        {
            _httpOverseasSiteService = httpOverseasSiteService ?? throw new ArgumentNullException(nameof(httpOverseasSiteService));
        }

        public async Task<OverseasAddress> GetReprocessorDetails(
            Guid accreditationExternalId,
            Guid overseasSiteExternalId)
        {
            var overseasSite = await _httpOverseasSiteService.GetOverseasReprocessingSite(
                accreditationExternalId,
                overseasSiteExternalId);

            if (overseasSite.OverseasAddress == null)
                return null;

            return overseasSite.OverseasAddress;
        }

        public async Task UpdateReprocessorDetails(
            Guid accreditationExternalId,
            OverseasAddress reprocessorDetails)
        {
            var overseasSite = new OverseasReprocessingSite
            {
                OverseasAddress = reprocessorDetails
            };

            await _httpOverseasSiteService.UpdateOverseasReprocessingSite(
                accreditationExternalId,
                overseasSite
                );
        }
    }
}
