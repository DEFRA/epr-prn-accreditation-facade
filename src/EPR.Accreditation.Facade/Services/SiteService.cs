using EPR.Accreditation.Facade.Common.Dtos;
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

        }

        public async Task<DTO.Site> GetSite(
            Guid id)
        {
            var site = await _httpSiteService.GetSite(id);

            return site;
        }
    }
}
