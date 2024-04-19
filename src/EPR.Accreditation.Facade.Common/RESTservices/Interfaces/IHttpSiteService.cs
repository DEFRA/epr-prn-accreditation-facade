using DTO = EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    public interface IHttpSiteService
    {
        Task<DTO.Site> GetSite(
           Guid id,
           Guid? overseasSiteId = null);
   
        Task UpdateSite(
            Guid id,
            DTO.Site site);
    }
}
