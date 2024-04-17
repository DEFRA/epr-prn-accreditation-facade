using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;

namespace EPR.Accreditation.Facade.Services
{
    public class CountryService : ICountryService
    {
        protected readonly IHttpCountryService _httpCountryService;

        public CountryService(IHttpCountryService httpCountryService)
        {
            _httpCountryService = httpCountryService ?? throw new ArgumentNullException(nameof(httpCountryService));
        }

        public async Task<IEnumerable<Country>> GetCountryList()
        {
            return await _httpCountryService.GetCountryList();
        }
    }
}
