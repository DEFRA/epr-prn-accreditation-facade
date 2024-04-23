using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface IOverseasSiteService
    {
        public Task<ReprocessorDetailsDto> GetReprocessorDetails(
            Guid accreditationExternalId,
            Guid overseasSiteExternalId);

        public Task UpdateReprocessorDetails(
            Guid accreditationExternalId,
            Guid overseasSiteExternalId,
            ReprocessorDetailsDto reprocessorDetails);
    }
}
