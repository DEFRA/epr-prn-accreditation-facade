using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services;
using Moq;

namespace EPR.Accreditation.UnitTests.Services
{
    [TestClass]
    public class WastePermitServiceTests
    {
        private Mock<IHttpAccreditationService> _mockHttpAccreditationService;
        private WastePermitService _wastePermitService;

        [TestInitialize]
        public void Init()
        {
            _mockHttpAccreditationService = new Mock<IHttpAccreditationService>();
            _wastePermitService = new WastePermitService(_mockHttpAccreditationService.Object);
        }

        [TestMethod]
        public async Task GetHasPermitExemption_ReturnsTrue()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var accreditation = new Facade.Common.Dtos.Accreditation
            {
                WastePermit = new Facade.Common.Dtos.WastePermit { WastePermitExemption = true }
            };

            _mockHttpAccreditationService.Setup(service => service.GetAccreditation(accreditationExternalId)).ReturnsAsync(accreditation);

            // Act
            var result = await _wastePermitService.GetHasPermitExemption(accreditationExternalId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.HasValue);
            Assert.IsTrue(result.Value);

            _mockHttpAccreditationService.Verify(service => service.GetAccreditation(accreditationExternalId), Times.Once);
        }

        [TestMethod]
        public async Task GetHasPermitExemption_ReturnsFalse()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var accreditation = new Facade.Common.Dtos.Accreditation
            {
                WastePermit = new Facade.Common.Dtos.WastePermit { WastePermitExemption = false }
            };

            _mockHttpAccreditationService.Setup(service => service.GetAccreditation(accreditationExternalId)).ReturnsAsync(accreditation);

            // Act
            var result = await _wastePermitService.GetHasPermitExemption(accreditationExternalId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.HasValue);
            Assert.IsFalse(result.Value);

            _mockHttpAccreditationService.Verify(service => service.GetAccreditation(accreditationExternalId), Times.Once);
        }

        [TestMethod]
        public async Task GetHasPermitExemption_ReturnsNull_WhenAccreditationWastePermitIsNull()
        {
            // Arrange

            var accreditation = new Facade.Common.Dtos.Accreditation
            {
                WastePermit = null
            };

            _mockHttpAccreditationService.Setup(service => service.GetAccreditation(It.IsAny<Guid>())).ReturnsAsync(accreditation);

            // Act
            var result = await _wastePermitService.GetHasPermitExemption(Guid.NewGuid());

            // Assert
            Assert.IsNull(result);

            _mockHttpAccreditationService.Verify(service => service.GetAccreditation(It.IsAny<Guid>()), Times.Once);
        }


        [TestMethod]
        public async Task UpdatePermitExemption_WithPermitExemption_ShouldSetHasPermitExemptionToTrue()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();

            var permitExemption = new PermitExemption
            {
                HasPermitExemption = true
            };

            var accreditation = new Facade.Common.Dtos.Accreditation
            {
                WastePermit = new Facade.Common.Dtos.WastePermit { WastePermitExemption = permitExemption.HasPermitExemption }
            };

            _mockHttpAccreditationService.Setup(service => service.UpdateAccreditation(accreditationExternalId, accreditation));


            // Act
            await _wastePermitService.UpdatePermitExemption(accreditationExternalId, permitExemption);

            // Assert
            Assert.IsTrue(permitExemption.HasPermitExemption);

            _mockHttpAccreditationService.Verify(service => service.UpdateAccreditation(accreditationExternalId, accreditation), Times.Once);
        }

        [TestMethod]
        public async Task UpdatePermitExemption_WithPermitExemption_ShouldSetHasPermitExemptionToFalse()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();

            var permitExemption = new PermitExemption
            {
                HasPermitExemption = false
            };

            var accreditation = new Facade.Common.Dtos.Accreditation
            {
                WastePermit = new Facade.Common.Dtos.WastePermit { WastePermitExemption = permitExemption.HasPermitExemption }
            };


            // Act
            await _wastePermitService.UpdatePermitExemption(accreditationExternalId, permitExemption);

            // Assert
            Assert.IsFalse(permitExemption.HasPermitExemption);

            _mockHttpAccreditationService.Verify(service => service.UpdateAccreditation(accreditationExternalId, accreditation), Times.Once);
        }
    }
}
