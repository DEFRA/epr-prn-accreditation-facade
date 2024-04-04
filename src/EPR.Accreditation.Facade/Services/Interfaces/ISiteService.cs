using DTO = EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface ISiteService
    {
        Task<Guid> CreateSite(DTO.Site site);

        Task<DTO.Site> GetSite(
            Guid accreditationExternalId,
            Guid siteExternalId);

        Task UpdateSite(Guid siteExternalId, DTO.Site site);
    }
}
