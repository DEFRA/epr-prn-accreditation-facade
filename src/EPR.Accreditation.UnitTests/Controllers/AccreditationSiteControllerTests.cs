using EPR.Accreditation.Facade.Controllers;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EPR.Accreditation.UnitTests.Controllers
{
    [TestClass]
    public class AccreditationSiteControllerTests
    {
        private AccreditationSiteController _accreditationSiteController;
        private Mock<ISiteService> _mockSiteService;

        [TestInitialize]
        public void Init()
        {
            _mockSiteService = new Mock<ISiteService>();
            _accreditationSiteController = new AccreditationSiteController(_mockSiteService.Object);
        }

        [TestMethod]
        public async Task GetExemptionReferences_ReturnsOk_ForValidAccreditationId()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var exemptionReferences = new List<string>();

            _mockSiteService.Setup(s => s.GetExemptionReferences(accreditationExternalId)).ReturnsAsync(exemptionReferences);

            // Act
            var result = await _accreditationSiteController.GetExemptionReferences(accreditationExternalId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(exemptionReferences, okResult.Value);
            _mockSiteService.Verify(s => s.GetExemptionReferences(accreditationExternalId), Times.Once);
        }

        [TestMethod]
        public async Task UpdatePermitExemption_ReturnsOk_ForValidData()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var exemptionReferences = new List<string> { "Reference1", "Reference2" };

            // Act
            var result = await _accreditationSiteController.UpdatePermitExemption(accreditationExternalId, exemptionReferences);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
            _mockSiteService.Verify(s => s.UpdateExemptionReferences(accreditationExternalId, exemptionReferences), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task UpdatePermitExemption_ServiceThrowsException_ThrowsException()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var exemptionReferences = new List<string> { "Reference1", "Reference2" };

            _mockSiteService.Setup(x =>
                x.UpdateExemptionReferences(
                    accreditationExternalId,
                    exemptionReferences))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            await _accreditationSiteController.UpdatePermitExemption(accreditationExternalId, exemptionReferences);

            // Assert
            _mockSiteService.Verify(s => s.UpdateExemptionReferences(accreditationExternalId, exemptionReferences), Times.Never);
        }
    }
}
