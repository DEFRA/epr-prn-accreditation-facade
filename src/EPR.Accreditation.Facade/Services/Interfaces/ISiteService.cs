using DTO = EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface ISiteService
    {
        Task<DTO.Site> GetSite(
            Guid id);
    }
}
