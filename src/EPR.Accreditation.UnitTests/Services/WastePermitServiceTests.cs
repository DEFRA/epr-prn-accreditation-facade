using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services;
using Moq;

namespace EPR.Accreditation.UnitTests.Services
{
    [TestClass]
    public class WastePermitServiceTests
    {
        private WastePermitService _wastePermitService;
        private Mock<IHttpAccreditationService> _mockHttpAccreditationService;

        [TestInitialize]
        public void Init()
        {
            _mockHttpAccreditationService = new Mock<IHttpAccreditationService>();
            _wastePermitService = new WastePermitService(_mockHttpAccreditationService.Object);
        }

        [TestMethod]
        public async Task GetHasPermitExemption_ReturnsTrue_ForValidAccreditation_()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var accreditation = new Facade.Common.Dtos.Accreditation
            {
                WastePermit = new WastePermit
                {
                    WastePermitExemption = true
                }
            };

            _mockHttpAccreditationService.Setup(s => s.GetAccreditation(accreditationExternalId)).ReturnsAsync(accreditation);

            // Act
            var result = await _wastePermitService.GetHasPermitExemption(accreditationExternalId);

            // Assert
            Assert.IsTrue(result);
            _mockHttpAccreditationService.Verify(s => s.GetAccreditation(accreditationExternalId), Times.Once);
        }

        [TestMethod]
        public async Task GetHasPermitExemption_ReturnsFalse_ForValidAccreditation()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var accreditation = new Facade.Common.Dtos.Accreditation
            {
                WastePermit = new WastePermit
                {
                    WastePermitExemption = false
                }
            };

            _mockHttpAccreditationService.Setup(s => s.GetAccreditation(accreditationExternalId)).ReturnsAsync(accreditation);

            // Act
            var result = await _wastePermitService.GetHasPermitExemption(accreditationExternalId);

            // Assert
            Assert.IsFalse(result);
            _mockHttpAccreditationService.Verify(s => s.GetAccreditation(accreditationExternalId), Times.Once);
        }

        [TestMethod]
        public async Task GetHasPermitExemption_ReturnsNull_ForNullWastePermit()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var accreditation = new Facade.Common.Dtos.Accreditation { WastePermit = null };

            _mockHttpAccreditationService.Setup(s => s.GetAccreditation(accreditationExternalId)).ReturnsAsync(accreditation);

            // Act
            var result = await _wastePermitService.GetHasPermitExemption(accreditationExternalId);

            // Assert
            Assert.IsNull(result);
            _mockHttpAccreditationService.Verify(s => s.GetAccreditation(accreditationExternalId), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GetHasPermitExemption_ThrowsException()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            _mockHttpAccreditationService.Setup(s =>
                s.GetAccreditation(accreditationExternalId)).ThrowsAsync(new Exception("Test exception"));

            // Act
            await _wastePermitService.GetHasPermitExemption(accreditationExternalId);

            // Assert
            _mockHttpAccreditationService.Verify(s => s.GetAccreditation(accreditationExternalId), Times.Once);
        }

        [TestMethod]
        public async Task UpdatePermitExemption_CallsUpdateAccreditationWithCorrectArguments_ForValidData()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var permitExemption = new PermitExemption
            {
                HasPermitExemption = true
            };
            var expectedAccreditation = new Facade.Common.Dtos.Accreditation
            {
                WastePermit = new WastePermit
                {
                    WastePermitExemption = true
                }
            };

            // Act
            await _wastePermitService.UpdatePermitExemption(accreditationExternalId, permitExemption);

            // Assert
            _mockHttpAccreditationService.Verify(a => a.UpdateAccreditation(
                accreditationExternalId,
                It.Is<Facade.Common.Dtos.Accreditation>(a => AreEqual(a, expectedAccreditation))), Times.Once);
        }

        private bool AreEqual(Facade.Common.Dtos.Accreditation a1, Facade.Common.Dtos.Accreditation a2)
        {
            if (a1.WastePermit.WastePermitExemption == a2.WastePermit.WastePermitExemption)
                return true;
            return false;
        }
    }
}
