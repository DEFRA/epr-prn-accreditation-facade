using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface IOverseasSiteService
    {
        public Task<OverseasAddress> GetReprocessorDetails(
            Guid accreditationExternalId,
            Guid overseasSiteExternalId);

        public Task UpdateReprocessorDetails(
            Guid accreditationExternalId,
            Guid overseasSiteExternalId,
            OverseasAddress reprocessorDetails);
        public Task<OverseasReprocessingSite> GetOverseasSite(Guid externalId, Guid siteId);
        public Task<OverseasReprocessingSite> UpdateOverseasSite(Guid externalId, Guid siteId);
    }
}
