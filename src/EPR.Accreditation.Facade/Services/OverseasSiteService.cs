using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Services.Interfaces;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Common.Dtos.Portal;
using AutoMapper;

namespace EPR.Accreditation.Facade.Services
{
    public class OverseasSiteService : IOverseasSiteService
    {
        protected readonly IMapper _mapper;
        protected readonly IHttpOverseasSiteService _httpOverseasSiteService;
        protected readonly IHttpCountryService _httpCountryService;

        public OverseasSiteService(
            IMapper mapper,
            IHttpOverseasSiteService httpOverseasSiteService,
            IHttpCountryService httpCountryService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

            if (!countries.Any())
            {
                throw new ArgumentException("Empty countries list. Cannot continue");
            }

            var reprocessorDetails = new ReprocessorDetailsDto
            {
                Name = overseasSite.OverseasAddress?.Name,
                CountryId = overseasSite.OverseasAddress?.CountryId,
                CountryList = countries,
                Address = overseasSite.OverseasAddress?.Address
            };

            return reprocessorDetails;
        }

        public async Task UpdateReprocessorDetails(
            Guid id,
            Guid overseasSiteId,
            ReprocessorDetailsDto reprocessorDetails)
        {
            var overseasSite = await _httpOverseasSiteService.GetOverseasReprocessingSite(
                id,
                overseasSiteId);
            overseasSite.OverseasAddress = _mapper.Map<OverseasAddress>(reprocessorDetails);

            await _httpOverseasSiteService.UpdateOverseasReprocessingSite(
                id,
                overseasSiteId,
                overseasSite);
        }
    }
}
