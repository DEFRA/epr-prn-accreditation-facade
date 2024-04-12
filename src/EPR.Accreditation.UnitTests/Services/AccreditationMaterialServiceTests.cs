using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services;
using Moq;

namespace EPR.Accreditation.UnitTests.Services
{
    [TestClass]
    public class AccreditationMaterialServiceTests
    {
        private AccreditationMaterialService _accreditationMaterialService;
        private Mock<IHttpAccreditationService> _mockAttpAccreditationService;

        [TestInitialize]
        public void Init()
        {
            _mockAttpAccreditationService = new Mock<IHttpAccreditationService>();
            _accreditationMaterialService = new AccreditationMaterialService(_mockAttpAccreditationService.Object);
        }

        [TestMethod]
        public async Task GetReprocessedWasteLastYear_ReturnsWasteLastYear_WhenAccreditationMaterialExists()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var materialExternalId = Guid.NewGuid();
            var expectedWasteLastYear = true;
            var accreditationMaterial = new AccreditationMaterial { WasteLastYear = expectedWasteLastYear };

            _mockAttpAccreditationService.Setup(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    null,
                    materialExternalId))
                .ReturnsAsync(accreditationMaterial);

            // Act
            var result = await _accreditationMaterialService.GetReprocessedWasteLastYear(
                accreditationExternalId,
                materialExternalId);

            // Assert
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expectedWasteLastYear, result.Value);

            _mockAttpAccreditationService.Verify(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    null,
                    materialExternalId), Times.Once());
        }

        [TestMethod]
        public async Task GetReprocessedWasteLastYear_ReturnsNull_WhenAccreditationMaterialDoesNotExist()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var materialExternalId = Guid.NewGuid();

            _mockAttpAccreditationService.Setup(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    null,
                    materialExternalId))
                .ReturnsAsync((AccreditationMaterial)null);

            // Act
            var result = await _accreditationMaterialService.GetReprocessedWasteLastYear(
                accreditationExternalId,
                materialExternalId);

            // Assert
            Assert.IsNull(result);

            _mockAttpAccreditationService.Verify(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    null,
                    materialExternalId), Times.Once());
        }

        [TestMethod]
        public async Task UpdateReprocessedWasteLastYear_CallsUpdateAccreditationMaterialWithCorrectParameters()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var materialExternalId = Guid.NewGuid();
            var reprocessedWasteLastYear = new ReprocessedWasteLastYear { HasReprocessedWasteLastYear = true };
            var expectedAccreditationMaterial = new AccreditationMaterial { WasteLastYear = true };

            // Act
            await _accreditationMaterialService.UpdateReprocessedWasteLastYear(
                accreditationExternalId,
                materialExternalId,
                reprocessedWasteLastYear);

            // Assert
            _mockAttpAccreditationService.Verify(s =>
                s.UpdateAccreditationMaterial(
                    SiteType.Site,
                    It.IsAny<Guid>(),
                    null,
                    It.IsAny<Guid>(),
                    It.IsAny<AccreditationMaterial>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateReprocessedWasteLastYear_WithFalseValue_CallsUpdateAccreditationMaterialWithCorrectParameters()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var materialExternalId = Guid.NewGuid();
            var reprocessedWasteLastYear = new ReprocessedWasteLastYear { HasReprocessedWasteLastYear = false };
            var expectedAccreditationMaterial = new AccreditationMaterial { WasteLastYear = false };

            // Act
            await _accreditationMaterialService.UpdateReprocessedWasteLastYear(
                accreditationExternalId,
                materialExternalId,
                reprocessedWasteLastYear);

            // Assert
            _mockAttpAccreditationService.Verify(s =>
                s.UpdateAccreditationMaterial(
                    SiteType.Site,
                    It.IsAny<Guid>(),
                    null,
                    It.IsAny<Guid>(),
                    It.IsAny<AccreditationMaterial>()), Times.Once);
        }
    }
}
