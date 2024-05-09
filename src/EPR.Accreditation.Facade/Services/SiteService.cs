namespace EPR.Accreditation.Facade.Services
{
    using AutoMapper;
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Facade.Common.Dtos.Portal;
    using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
    using EPR.Accreditation.Facade.Services.Interfaces;

    public class SiteService : ISiteService
    {
        protected readonly IMapper _mapper;
        protected readonly IHttpSiteService _httpSiteService;

        public SiteService(
            IMapper mapper,
            IHttpSiteService httpSiteService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpSiteService = httpSiteService ?? throw new ArgumentNullException(nameof(httpSiteService));
        }

        public async Task<IEnumerable<string>> GetExemptionReferences(Guid id)
        {
            var site = await _httpSiteService.GetSite(id);

            if (site != null)
            {
                return site.ExemptionReferences;
            }
            else
            {
                throw new Exception("Site not found for the given accreditation ID.");
            }
        }

        public async Task UpdateExemptionReferences(
            Guid id,
            IEnumerable<string> references)
        {
            var site = await _httpSiteService.GetSite(id);

            _mapper.Map(references, site.ExemptionReferences);

            await _httpSiteService.UpdateSite(
                id,
                site);
        }

        public async Task<AddressDto> GetSite(
            Guid id)
        {
            var site = await _httpSiteService.GetSite(id);

            return _mapper.Map<AddressDto>(site);
        }

        public async Task<Guid> CreateSite(
            Guid accreditationExternalId,
            AddressDto siteAddress)
        {
            var site = _mapper.Map<Site>(siteAddress);

            return await _httpSiteService.CreateSite(
                accreditationExternalId,
                site);
        }

        public async Task UpdateSite(
            Guid siteExternalId,
            AddressDto siteAddress)
        {
            var site = _mapper.Map<Site>(siteAddress);

            await _httpSiteService.UpdateSite(
                siteExternalId,
                site);
        }
    }
}
