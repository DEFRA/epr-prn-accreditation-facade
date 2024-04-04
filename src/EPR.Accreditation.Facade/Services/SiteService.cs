using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;

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
            IEnumerable<ExemptionReference> references)
        {
            var site = new Site
            {
                //ExemptionReferences = references
            };

            await _httpSiteService.UpdateSite(
                accreditationExternalId,
                site
                );
        }
    }
}
