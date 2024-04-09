using EPR.Accreditation.Facade.Common.Dtos.Portal;
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

        [TestInitialize]

        public void Init()
        {
            _mockAccreditationService = new Mock<IAccreditationService>();
            _mockWastePermitService = new Mock<IWastePermitService>();
            _mockSiteService = new Mock<ISiteService>();

            _accreditationController = new AccreditationController(
                _mockAccreditationService.Object,
                _mockWastePermitService.Object,
                _mockSiteService.Object
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
        public async Task GetHasPermitExemption_ThrowsException_WhenServiceThrowsException()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            _mockWastePermitService.Setup(s => s.GetHasPermitExemption(accreditationExternalId)).ThrowsAsync(new Exception("Test exception"));

            // Act & Assert
            try
            {
                await _accreditationController.GetHasPermitExemption(accreditationExternalId);
                Assert.Fail("Exception expected but not thrown");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(Exception));
                Assert.AreEqual("Test exception", ex.Message);
            }
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
        public async Task UpdatePermitExemption_ReturnsBadRequest_WhenServiceThrowsException()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var permitExemption = new PermitExemption();
            _mockWastePermitService.Setup(s =>
                s.UpdatePermitExemption(accreditationExternalId, permitExemption)).ThrowsAsync(new Exception("Test exception"));

            // Act & Assert
            try
            {
                var result = await _accreditationController.UpdatePermitExemption(accreditationExternalId, permitExemption);
                Assert.Fail("Exception expected but not thrown");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(Exception));
                Assert.AreEqual("Test exception", ex.Message);
            }

            _mockWastePermitService.Verify(service => service.UpdatePermitExemption(accreditationExternalId, permitExemption), Times.Once());
        }
    }
}