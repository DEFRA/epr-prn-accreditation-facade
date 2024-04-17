namespace EPR.Accreditation.UnitTests.Services
{
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
    using EPR.Accreditation.Facade.Services;
    using Moq;

    [TestClass]
    public class OverseasSiteServiceTests
    {
        private OverseasSiteService _overseasSiteService;
        private Mock<IHttpOverseasSiteService> _mockHttpOverseasSiteService;

        [TestInitialize]
        public void Init()
        {
            _mockHttpOverseasSiteService = new Mock<IHttpOverseasSiteService>();
            _overseasSiteService = new OverseasSiteService(_mockHttpOverseasSiteService.Object);
        }

        [TestMethod]
        public async Task GetReprocessorDetails_WithValidData_ReturnsOverseasAddress()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var overseasSiteExternalId = Guid.NewGuid();
            var overseasSite = new OverseasReprocessingSite
            {
                OverseasAddress = new OverseasAddress()
            };

            _mockHttpOverseasSiteService.Setup(s =>
                s.GetOverseasReprocessingSite(
                    accreditationExternalId,
                    overseasSiteExternalId))
                .ReturnsAsync(overseasSite);

            // Act
            var result = await _overseasSiteService.GetReprocessorDetails(accreditationExternalId, overseasSiteExternalId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OverseasAddress));

            _mockHttpOverseasSiteService.Verify(s =>
                s.GetOverseasReprocessingSite(
                    accreditationExternalId,
                    overseasSiteExternalId),
                    Times.Once);
        }

        [TestMethod]
        public async Task GetReprocessorDetails_WithNullOverseasAddress_ReturnsNull()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var overseasSiteExternalId = Guid.NewGuid();
            var overseasSite = new OverseasReprocessingSite();

            _mockHttpOverseasSiteService.Setup(s =>
                s.GetOverseasReprocessingSite(
                    accreditationExternalId,
                    overseasSiteExternalId))
                .ReturnsAsync(overseasSite);

            // Act
            var result = await _overseasSiteService.GetReprocessorDetails(accreditationExternalId, overseasSiteExternalId);

            // Assert
            Assert.IsNull(result);

            _mockHttpOverseasSiteService.Verify(s =>
                s.GetOverseasReprocessingSite(
                    accreditationExternalId,
                    overseasSiteExternalId),
                    Times.Once);
        }

        [TestMethod]
        public async Task UpdateReprocessorDetails_WithValidData_CallsUpdateOverseasReprocessingSite()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var overseasSiteExternalId = Guid.NewGuid();
            var reprocessorDetails = new OverseasAddress();

            // Act
            await _overseasSiteService.UpdateReprocessorDetails(
                accreditationExternalId,
                overseasSiteExternalId,
                reprocessorDetails);

            // Assert
            _mockHttpOverseasSiteService.Verify(s =>
                s.UpdateOverseasReprocessingSite(
                    accreditationExternalId,
                    It.IsAny<OverseasReprocessingSite>()),
                    Times.Once);
        }
    }
}
