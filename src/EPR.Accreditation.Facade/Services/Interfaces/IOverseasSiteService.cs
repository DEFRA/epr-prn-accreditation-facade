using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface IOverseasSiteService
    {
        Task<OverseasReprocessingSite> GetOverseasSite(Guid externalId, Guid siteId);
    }
}
