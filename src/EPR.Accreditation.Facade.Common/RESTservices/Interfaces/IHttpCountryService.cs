using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    public interface IHttpCountryService
    {
        Task<IEnumerable<Country>> GetCountryList();
    }
}
