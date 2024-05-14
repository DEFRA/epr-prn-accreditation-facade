namespace EPR.Accreditation.UnitTests.Services
{
    using AutoMapper;
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Facade.Common.Dtos.Portal;
    using EPR.Accreditation.Facade.Common.RESTservices;
    using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
    using EPR.Accreditation.Facade.Services;
    using Moq;

    [TestClass]
    public class OverseasSiteServiceTests
    {
        private OverseasSiteService _overseasSiteService;
        private Mock<IHttpOverseasSiteService> _mockHttpOverseasSiteService;
        private Mock<IHttpCountryService> _mockHttpCountryService;
        private Mock<IMapper> _mockMapper;

        [TestInitialize]
        public void Init()
        {
            _mockMapper = new Mock<IMapper>();
            _mockHttpOverseasSiteService = new Mock<IHttpOverseasSiteService>();
            _mockHttpCountryService = new Mock<IHttpCountryService>();

            _overseasSiteService = new OverseasSiteService(
                _mockMapper.Object,
                _mockHttpOverseasSiteService.Object,
                _mockHttpCountryService.Object);
        }

        [TestMethod]
        public async Task GetReprocessorDetails_ReturnsDto_WhenDataIsValid()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var overseasSiteExternalId = Guid.NewGuid();
            var overseasAddress = new OverseasAddress
            {
                Name = "OverseasName",
                CountryId = 1,
                Address = "Address"
            };
            var overseasSite = new OverseasReprocessingSite
            {
                OverseasAddress = overseasAddress
            };
            var countries = new List<Country>
            {
                new Country
                {
                    CountryId = 1,
                    Name = "Country1"
                }
            };

            _mockHttpOverseasSiteService.Setup(s =>
                s.GetOverseasReprocessingSite(
                    accreditationExternalId,
                    overseasSiteExternalId))
                .ReturnsAsync(overseasSite);

            _mockHttpCountryService.Setup(s => s.GetCountryList()).ReturnsAsync(countries);

            // Act
            var result = await _overseasSiteService.GetReprocessorDetails(accreditationExternalId, overseasSiteExternalId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(overseasAddress.Name, result.Name);
            Assert.AreEqual(overseasAddress.CountryId, result.CountryId);
            Assert.AreEqual(countries, result.CountryList);
            Assert.AreEqual(overseasAddress.Address, result.Address);

            _mockHttpOverseasSiteService.Verify(s =>
                s.GetOverseasReprocessingSite(
                    accreditationExternalId,
                    overseasSiteExternalId),
                    Times.Once);

            _mockHttpCountryService.Verify(s => s.GetCountryList(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetReprocessorDetails_ReturnsNull_WhenCountryListIsEmpty()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var overseasSiteExternalId = Guid.NewGuid();
            var overseasAddress = new OverseasAddress
            {
                Name = "OverseasName",
                CountryId = 1,
                Address = "Address"
            };
            var overseasSite = new OverseasReprocessingSite
            {
                OverseasAddress = overseasAddress
            }
            ;
            var countries = new List<Country>(); // Empty country list

            _mockHttpOverseasSiteService.Setup(s =>
                s.GetOverseasReprocessingSite(
                    accreditationExternalId,
                    overseasSiteExternalId))
                .ReturnsAsync(overseasSite);

            _mockHttpCountryService.Setup(s => s.GetCountryList()).ReturnsAsync(countries);

            // Act
            var result = await _overseasSiteService.GetReprocessorDetails(accreditationExternalId, overseasSiteExternalId);

            // Assert
        }

        [TestMethod]
        public async Task UpdateReprocessorDetails_WithValidData_CallsUpdateOverseasReprocessingSite()
        {
            // Arrange
            var id = Guid.NewGuid();
            var overseasSiteId = Guid.NewGuid();
            var reprocessorDetails = new ReprocessorDetailsDto();

            _mockHttpOverseasSiteService
                .Setup(o =>
                    o.GetOverseasReprocessingSite(
                        id,
                        overseasSiteId))
                .ReturnsAsync(new OverseasReprocessingSite());

            // Act
            await _overseasSiteService.UpdateReprocessorDetails(
                id,
                overseasSiteId,
                reprocessorDetails);

            // Assert
            _mockHttpOverseasSiteService.Verify(s =>
                s.UpdateOverseasReprocessingSite(
                    id,
                    overseasSiteId,
                    It.IsAny<OverseasReprocessingSite>()),
                    Times.Once);
        }
    }
}
