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
            var result = _wastePermitService.GetHasPermitExemption(accreditationExternalId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result.HasValue);
            Assert.IsTrue(result.Result.Value);

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
            var result = _wastePermitService.GetHasPermitExemption(accreditationExternalId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result.HasValue);
            Assert.IsFalse(result.Result.Value);

            _mockHttpAccreditationService.Verify(service => service.GetAccreditation(accreditationExternalId), Times.Once);
        }

        //[TestMethod]
        //public async Task GetHasPermitExemption_ReturnsNull_WhenAccreditationWastePermitIsNull()
        //{
        //    // Arrange

        //    var accreditation = new Facade.Common.Dtos.Accreditation
        //    {
        //        WastePermit = null
        //    };

        //    _mockHttpAccreditationService.Setup(service => service.GetAccreditation(It.IsAny<Guid>())).ReturnsAsync(accreditation);

        //    // Act
        //    var result = _wastePermitService.GetHasPermitExemption(Guid.NewGuid());

        //    // Assert
        //    Assert.IsNull(result);

        //    _mockHttpAccreditationService.Verify(service => service.GetAccreditation(It.IsAny<Guid>()), Times.Once);
        //}
    }
}
