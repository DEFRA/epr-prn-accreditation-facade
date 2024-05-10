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

            if (site.ExemptionReferences == null)
                return null;

            return site.ExemptionReferences;
        }

        public async Task UpdateExemptionReferences(
            Guid id,
            IEnumerable<string> references)
        {
            var site = await _httpSiteService.GetSite(id);
            site.ExemptionReferences = references;

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
            Guid id,
            AddressDto siteAddress)
        {
            var site = _mapper.Map<Site>(siteAddress);

            return await _httpSiteService.CreateSite(
                id, 
                site);
        }

        public async Task UpdateSite(
            Guid siteId,
            AddressDto siteAddress)
        {
            var site = await _httpSiteService.GetSite(siteId);
            _mapper.Map(siteAddress, site);

            await _httpSiteService.UpdateSite(
                siteId,
                site);
        }
    }
}
