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

        [TestInitialize]

        public void Init()
        {
            _mockAccreditationService = new Mock<IAccreditationService>();
            _mockWastePermitService = new Mock<IWastePermitService>();
            _accreditationController = new AccreditationController(
                _mockAccreditationService.Object,
                _mockWastePermitService.Object
                );
        }

        [TestMethod]
        public async Task GetHasPermitExemption_ReturnsOk_WithWastePermit()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var expectedResult = true;

            _mockWastePermitService.Setup(service => service.GetHasPermitExemption(accreditationExternalId)).ReturnsAsync(expectedResult);

            // Act
            var result = await _accreditationController.GetHasPermitExemption(accreditationExternalId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(expectedResult, okResult.Value);

            _mockWastePermitService.Verify(service => service.GetHasPermitExemption(accreditationExternalId), Times.Once());
        }
    }
}
