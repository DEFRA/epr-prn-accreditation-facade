using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.RESTservices;
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

        public async Task<DTO.Site> GetSite(
            Guid id)
        {
            var site = await _httpSiteService.GetSite(id);

            return site;
        }

        public async Task<Guid> CreateSite(
            Guid accreditationExternalId,
            Common.Dtos.Site site)
        {
            return await _httpSiteService.CreateSite(accreditationExternalId, site);
        }

        public async Task UpdateSite(
            Guid siteExternalId,
            Site site)
        {
            await _httpSiteService.UpdateSite(
                siteExternalId,
                site
                );
        }
    }
}
