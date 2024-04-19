namespace EPR.Accreditation.UnitTests.Services
{
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
    using EPR.Accreditation.Facade.Services;
    using Moq;

    [TestClass]
    public class CountryServiceTests
    {
        private CountryService _countryService;
        private Mock<IHttpCountryService> _mockHttpCountryService;

        [TestInitialize]
        public void Init()
        {
            _mockHttpCountryService = new Mock<IHttpCountryService>();
            _countryService = new CountryService(_mockHttpCountryService.Object);
        }

        [TestMethod]
        public async Task GetCountryList_ReturnsListOfCountries()
        {
            // Arrange
            var expectedCountries = new List<Country>
            {
                new Country { CountryId = 1, Name = "Country 1" },
                new Country { CountryId = 2, Name = "Country 2" }
            };

            _mockHttpCountryService.Setup(s => s.GetCountryList()).ReturnsAsync(expectedCountries);

            // Act
            var result = await _countryService.GetCountryList();

            // Assert
            CollectionAssert.AreEqual(expectedCountries, (System.Collections.ICollection)result);

            _mockHttpCountryService.Verify(s => s.GetCountryList(), Times.Once());
        }
    }
}
