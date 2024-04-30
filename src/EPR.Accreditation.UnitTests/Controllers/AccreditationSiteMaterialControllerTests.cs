using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Controllers;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EPR.Accreditation.UnitTests.Controllers
{
    [TestClass]
    public class AccreditationSiteMaterialControllerTests
    {
        private AccreditationMaterialController _accreditationSiteMaterialController;
        private Mock<IAccreditationService> _mockAccreditationService;
        private Mock<IWastePermitService> _mockWastePermitService;
        private Mock<IAccreditationMaterialService> _mockAccreditationMaterialService;

        [TestInitialize]
        public void Init()
        {
            _mockAccreditationService = new Mock<IAccreditationService>();
            _mockWastePermitService = new Mock<IWastePermitService>();
            _mockAccreditationMaterialService = new Mock<IAccreditationMaterialService>();

            _accreditationSiteMaterialController = new AccreditationMaterialController(
                _mockAccreditationService.Object,
                _mockWastePermitService.Object,
                _mockAccreditationMaterialService.Object);
        }

        [TestMethod]
        public async Task GetReprocessedWasteLastYear_ReturnsOk()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var materialExternalId = Guid.NewGuid();
            var expectedResult = false;

            _mockAccreditationMaterialService.Setup(s =>
                s.GetReprocessedWasteLastYear(
                    accreditationExternalId,
                    materialExternalId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _accreditationSiteMaterialController
                .GetReprocessedWasteLastYear(
                accreditationExternalId,
                materialExternalId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedResult, result.Value);

            _mockAccreditationMaterialService.Verify(s =>
            s.GetReprocessedWasteLastYear(
                accreditationExternalId,
                materialExternalId), Times.Once());
        }

        [TestMethod]
        public async Task UpdateReprocessedWasteLastYear_ReturnsOk()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var materialExternalId = Guid.NewGuid();
            var reprocessedWasteLastYear = new ReprocessedWasteLastYear();

            _mockAccreditationMaterialService.Setup(s =>
                s.UpdateReprocessedWasteLastYear(
                    accreditationExternalId,
                    materialExternalId,
                    reprocessedWasteLastYear))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _accreditationSiteMaterialController
                .UpdateReprocessedWasteLastYear(
                accreditationExternalId,
                materialExternalId,
                reprocessedWasteLastYear) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            _mockAccreditationMaterialService.Verify(s =>
            s.UpdateReprocessedWasteLastYear(
                accreditationExternalId,
                materialExternalId,
                reprocessedWasteLastYear), Times.Once());
        }

        [TestMethod]
        public async Task GetHasNpwdAccreditationNumber_ReturnsOk_WithCorrectValueOfTrue()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var materialExternalId = Guid.NewGuid();
            var expectedValue = true;

            _mockAccreditationMaterialService.Setup(s =>
                s.GetHasNpwdAccreditationNumber(
                    accreditationExternalId,
                    materialExternalId))
                .ReturnsAsync(expectedValue);

            // Act
            var result = await _accreditationSiteMaterialController.GetHasNpwdAccreditationNumber(
                accreditationExternalId,
                materialExternalId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedValue, result.Value);

            _mockAccreditationMaterialService.Verify(s =>
            s.GetHasNpwdAccreditationNumber(
                accreditationExternalId,
                materialExternalId), Times.Once());
        }

        [TestMethod]
        public async Task GetHasNpwdAccreditationNumber_ReturnsOk_WithCorrectValueOfFalse()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var materialExternalId = Guid.NewGuid();
            var expectedValue = false;

            _mockAccreditationMaterialService.Setup(s =>
                s.GetHasNpwdAccreditationNumber(
                    accreditationExternalId,
                    materialExternalId))
                .ReturnsAsync(expectedValue);

            // Act
            var result = await _accreditationSiteMaterialController.GetHasNpwdAccreditationNumber(
                accreditationExternalId,
                materialExternalId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedValue, result.Value);

            _mockAccreditationMaterialService.Verify(s =>
            s.GetHasNpwdAccreditationNumber(
                accreditationExternalId,
                materialExternalId), Times.Once());
        }

        [TestMethod]
        public async Task UpdateHasNpwdAccreditationNumber_ReturnsOk()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var materialExternalId = Guid.NewGuid();
            var npwdAccreditationNumber = new HasNpwdAccreditationNumber();

            _mockAccreditationMaterialService.Setup(s =>
                s.UpdateHasNpwdAccreditationNumber(
                    accreditationExternalId,
                    materialExternalId,
                    npwdAccreditationNumber))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _accreditationSiteMaterialController
                .UpdateHasNpwdAccreditationNumber(
                accreditationExternalId,
                materialExternalId,
                npwdAccreditationNumber) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            _mockAccreditationMaterialService.Verify(s =>
            s.UpdateHasNpwdAccreditationNumber(
                accreditationExternalId,
                materialExternalId,
                npwdAccreditationNumber), Times.Once());
        }
    }
}
