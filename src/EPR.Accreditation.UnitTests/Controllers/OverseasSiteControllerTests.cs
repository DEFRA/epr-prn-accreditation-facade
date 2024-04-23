using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Controllers;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EPR.Accreditation.UnitTests.Controllers
{
    [TestClass]
    public class OverseasSiteControllerTests
    {
        private OverseasSiteController _overseasSiteController;
        private Mock<IOverseasSiteService> _mockOverseasSiteService;
        private Mock<IAccreditationService> _mockAccreditationService;

        [TestInitialize]
        public void Init()
        {
            _mockAccreditationService = new Mock<IAccreditationService>();
            _mockOverseasSiteService = new Mock<IOverseasSiteService>();
            _overseasSiteController = new OverseasSiteController(
                _mockOverseasSiteService.Object, 
                _mockAccreditationService.Object);
        }

        [TestMethod]
        public async Task GetReprocessorDetails_ReturnsOkResultWithDetails()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var overseasSiteExternalId = Guid.NewGuid();
            var expectedDetails = new ReprocessorDetailsDto();

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

        [TestMethod]
        public async Task GetOverseasSiteOutputs_ReturnsOk_ForValidIds()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            Guid siteExternalId = Guid.NewGuid();

            var overseasReprocessingSiteOutputs = new OverseasReprocessingSiteOutputs();

            _mockAccreditationService.Setup(s => s.GetOverseasReprocessingSiteOutputs(
                accreditationExternalId,
                siteExternalId))
                .ReturnsAsync(overseasReprocessingSiteOutputs);

            // Act
            var result = await _overseasSiteController.GetOverseasSiteOutputs(
                accreditationExternalId,
                siteExternalId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            _mockAccreditationService.Verify(s => s.GetOverseasReprocessingSiteOutputs(
                accreditationExternalId,
                siteExternalId), Times.Once);
        }
    }
}
