using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EPR.Accreditation.Facade.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CountryController : ControllerBase
    {
        protected readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
        }

        [HttpGet]
        public async Task<IActionResult> GetCountryList()
        {
            var countries = await _countryService.GetCountryList();

            return Ok(countries);
        }
    }
}
