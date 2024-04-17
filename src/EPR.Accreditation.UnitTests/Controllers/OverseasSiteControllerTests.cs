namespace EPR.Accreditation.UnitTests.Controllers
{
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Facade.Controllers;
    using EPR.Accreditation.Facade.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Moq;

    [TestClass]
    public class OverseasSiteControllerTests
    {
        private OverseasSiteController _overseasSiteController;
        private Mock<IOverseasSiteService> _mockOverseasSiteService;

        [TestInitialize]
        public void Init()
        {
            _mockOverseasSiteService = new Mock<IOverseasSiteService>();
            _overseasSiteController = new OverseasSiteController(_mockOverseasSiteService.Object);
        }

        [TestMethod]
        public async Task GetReprocessorDetails_ReturnsOkResultWithDetails()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var overseasSiteExternalId = Guid.NewGuid();
            var expectedDetails = new OverseasAddress();

            _mockOverseasSiteService.Setup(s =>
                s.GetReprocessorDetails(
                    accreditationExternalId,
                    overseasSiteExternalId))
                .ReturnsAsync(expectedDetails);

            // Act
            var result = await _overseasSiteController.GetReprocessorDetails(
                accreditationExternalId,
                overseasSiteExternalId)
                as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedDetails, result.Value);

            _mockOverseasSiteService.Verify(s =>
                s.GetReprocessorDetails(
                    accreditationExternalId,
                    overseasSiteExternalId),
                    Times.Once);
        }

        [TestMethod]
        public async Task UpdateReprocessorDetails_ValidData_ReturnsOkResult()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var overseasSiteExternalId = Guid.NewGuid();
            var reprocessorDetails = new OverseasAddress();

            // Act
            var result = await _overseasSiteController.UpdateReprocessorDetails(
                accreditationExternalId,
                overseasSiteExternalId,
                reprocessorDetails)
                as OkResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            _mockOverseasSiteService.Verify(s =>
                s.UpdateReprocessorDetails(
                    accreditationExternalId,
                    overseasSiteExternalId,
                    reprocessorDetails),
                    Times.Once);
        }
    }
}
