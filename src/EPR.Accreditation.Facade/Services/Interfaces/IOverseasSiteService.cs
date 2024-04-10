using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface IOverseasSiteService
    {
        public Task<OverseasReprocessingSite> GetOverseasSite(Guid externalId, Guid siteId);
        public Task<OverseasReprocessingSite> UpdateOverseasSite(Guid externalId, Guid siteId);
    }
}
