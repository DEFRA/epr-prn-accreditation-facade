using DTO = EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    public interface IHttpSiteService
    {
        Task<Guid> CreateSite(DTO.Site site);

        Task<DTO.Site> GetSite(
            Guid accreditationExternalId,
            Guid siteExternalId);

        Task UpdateSite(
            Guid siteExternalId,
            DTO.Site site);
    }
}
