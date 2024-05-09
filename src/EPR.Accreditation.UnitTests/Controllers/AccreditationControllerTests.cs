using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Facade.Controllers;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EPR.Accreditation.UnitTests.Controllers
{
    [TestClass]
    public class AccreditationControllerTests
    {
        private AccreditationController _accreditationController;
        private Mock<IAccreditationService> _mockAccreditationService;
        private Mock<IWastePermitService> _mockWastePermitService;
        private Mock<ISiteService> _mockSiteService;
        private Mock<IAccreditationMaterialService> _mockMaterialService;

        [TestInitialize]

        public void Init()
        {
            _mockAccreditationService = new Mock<IAccreditationService>();
            _mockWastePermitService = new Mock<IWastePermitService>();
            _mockSiteService = new Mock<ISiteService>();
            _mockMaterialService = new Mock<IAccreditationMaterialService>();

            _accreditationController = new AccreditationController(
                _mockAccreditationService.Object,
                _mockWastePermitService.Object,
                _mockSiteService.Object,
                _mockMaterialService.Object
                );
        }

        [TestMethod]
        public async Task GetHasPermitExemption_ReturnsOk_WithWastePermit()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var expectedResult = true;

            _mockWastePermitService.Setup(s => s.GetHasPermitExemption(accreditationExternalId)).ReturnsAsync(expectedResult);

            // Act
            var result = await _accreditationController.GetHasPermitExemption(accreditationExternalId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(expectedResult, okResult.Value);

            _mockWastePermitService.Verify(service => service.GetHasPermitExemption(accreditationExternalId), Times.Once());
        }

        [TestMethod]
        public async Task GetHasPermitExemption_ReturnsOk_WhenServiceReturnsTrue()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();

            _mockWastePermitService.Setup(s => s.GetHasPermitExemption(accreditationExternalId)).ReturnsAsync(true);

            // Act
            var result = await _accreditationController.GetHasPermitExemption(accreditationExternalId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(true, okResult.Value);

            _mockWastePermitService.Verify(service => service.GetHasPermitExemption(accreditationExternalId), Times.Once());
        }

        [TestMethod]
        public async Task GetHasPermitExemption_ReturnsOk_WhenServiceReturnsFalse()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();

            _mockWastePermitService.Setup(s => s.GetHasPermitExemption(accreditationExternalId)).ReturnsAsync(false);

            // Act
            var result = await _accreditationController.GetHasPermitExemption(accreditationExternalId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(false, okResult.Value);

            _mockWastePermitService.Verify(service => service.GetHasPermitExemption(accreditationExternalId), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GetHasPermitExemption_ThrowsException_WhenServiceThrowsException()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            _mockWastePermitService.Setup(s => s.GetHasPermitExemption(accreditationExternalId)).ThrowsAsync(new Exception("Test exception"));

            // Act
            await _accreditationController.GetHasPermitExemption(accreditationExternalId);

            // Assert
            _mockWastePermitService.Verify(service => service.GetHasPermitExemption(accreditationExternalId), Times.Once());
        }

        [TestMethod]
        public async Task UpdatePermitExemption_ReturnsOk_WhenUpdateSuccessful()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var permitExemption = new PermitExemption();

            // Act
            var result = await _accreditationController.UpdatePermitExemption(accreditationExternalId, permitExemption);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));

            _mockWastePermitService.Verify(service => service.UpdatePermitExemption(accreditationExternalId, permitExemption), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task UpdatePermitExemption_ReturnsBadRequest_WhenServiceThrowsException()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var permitExemption = new PermitExemption();
            _mockWastePermitService.Setup(s =>
                s.UpdatePermitExemption(accreditationExternalId, permitExemption)).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _accreditationController.UpdatePermitExemption(accreditationExternalId, permitExemption);

            // Assert
            _mockWastePermitService.Verify(service => service.UpdatePermitExemption(accreditationExternalId, permitExemption), Times.Once());
        }

        [TestMethod]
        public async Task GetCheckAnswers_ReturnsOkObjectResultWithCorrectData()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var section = CheckAnswersSection.AboutMaterialReprocessorActuals;
            var expectedDto = new CheckAnswersDto();

            _mockAccreditationService.Setup(s => s.GetCheckAnswers(id, materialId, section)).ReturnsAsync(expectedDto);

            // Act
            var result = await _accreditationController.GetCheckAnswers(id, materialId, section) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedDto, result.Value);

            _mockAccreditationService.Verify(s => s.GetCheckAnswers(id, materialId, section), Times.Once());
        }
    }
}