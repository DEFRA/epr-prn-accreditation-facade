using DTO = EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    public interface IHttpSiteService
    {
        Task<DTO.Site> GetSite(
           Guid siteExternalId);
    }

    Task UpdateSite(
            Guid accreditationExternalId,
            Site site);
    }
}
