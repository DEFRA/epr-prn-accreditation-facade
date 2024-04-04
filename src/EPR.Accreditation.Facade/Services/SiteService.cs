using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;
using DTO = EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Services
{
    public class SiteService : ISiteService
    {
        protected readonly IHttpSiteService _httpSiteService;

        public SiteService(IHttpSiteService httpSiteService)
        {
            _httpSiteService = httpSiteService ?? throw new ArgumentNullException(nameof(httpSiteService));
        }

        public async Task<Guid> CreateSite(Common.Dtos.Site site)
        {
            return await _httpSiteService.CreateSite(site);
        }

        public async Task<DTO.Site> GetSite(
            Guid accreditationExternalId,
            Guid siteExternalId)
        {
            var site = await _httpSiteService.GetSite(accreditationExternalId, siteExternalId);

            return site;
        }

        public async Task UpdateSite(
            Guid siteExternalId,
            DTO.Site site)
        {

            await _httpSiteService.UpdateSite(
                siteExternalId,
                site);
        }
    }
}
