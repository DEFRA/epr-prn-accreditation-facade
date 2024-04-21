using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetCountryList();
    }
}
