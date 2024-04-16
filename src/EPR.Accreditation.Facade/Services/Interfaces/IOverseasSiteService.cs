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
            OverseasAddress reprocessorDetails);
    }
}
