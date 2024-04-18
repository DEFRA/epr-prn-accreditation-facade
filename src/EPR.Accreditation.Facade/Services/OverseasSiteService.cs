using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Services.Interfaces;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Common.Dtos.Portal;

namespace EPR.Accreditation.Facade.Services
{
    public class OverseasSiteService : IOverseasSiteService
    {
        protected readonly IHttpOverseasSiteService _httpOverseasSiteService;
        protected readonly IHttpCountryService _httpCountryService;

        public OverseasSiteService(
            IHttpOverseasSiteService httpOverseasSiteService,
            IHttpCountryService httpCountryService)
        {
            _httpOverseasSiteService = httpOverseasSiteService ?? throw new ArgumentNullException(nameof(httpOverseasSiteService));
            _httpCountryService = httpCountryService ?? throw new ArgumentNullException(nameof(httpCountryService));
        }

        public async Task<ReprocessorDetailsDto> GetReprocessorDetails(
            Guid accreditationExternalId,
            Guid overseasSiteExternalId)
        {
            var overseasSiteTask = _httpOverseasSiteService.GetOverseasReprocessingSite(
                accreditationExternalId,
                overseasSiteExternalId);

            var countriesTask = _httpCountryService.GetCountryList();

            await Task.WhenAll(overseasSiteTask, countriesTask);

            var overseasSite = overseasSiteTask.Result;
            var countries = countriesTask.Result;

            if (overseasSite.OverseasAddress == null
                || !countries.Any())
            {
                return null;
            }

            var reprocessorDetails = new ReprocessorDetailsDto
            {
                Name = overseasSite.OverseasAddress.Name,
                CountryId = overseasSite.OverseasAddress.CountryId,
                CountryList = countries,
                Address = overseasSite.OverseasAddress.Address
            };

            return reprocessorDetails;
        }

        public async Task UpdateReprocessorDetails(
            Guid accreditationExternalId,
            Guid overseasSiteExternalId,
            OverseasAddress reprocessorDetails)
        {
            var overseasSite = new OverseasReprocessingSite
            {
                ExternalId = overseasSiteExternalId,
                OverseasAddress = reprocessorDetails
            };

            await _httpOverseasSiteService.UpdateOverseasReprocessingSite(
                accreditationExternalId,
                overseasSite);
        }
    }
}
