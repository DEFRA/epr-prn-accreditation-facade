using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    public interface IHttpSiteService
    {
        Task<Site> GetSite(
            Guid accreditationExternalId);

        Task UpdateSite(
            Guid accreditationExternalId,
            Site site);
    }
}
