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

        public async Task<IEnumerable<string>> GetExemptionReferences(Guid accreditationExternalId)
        {
            var site = await _httpSiteService.GetSite(accreditationExternalId);

            if (site.ExemptionReferences == null)
                return null;

            return site.ExemptionReferences;
        }

        public async Task UpdateExemptionReferences(
            Guid accreditationExternalId,
            IEnumerable<string> references)
        {
            var site = new Site
            {
                ExemptionReferences = references
            };

            await _httpSiteService.UpdateSite(
                accreditationExternalId,
                site
                );
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
