namespace EPR.Accreditation.UnitTests.Controllers
{
    using EPR.Accreditation.Facade.Controllers;
    using EPR.Accreditation.Facade.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Moq;

    [TestClass]
    public class AccreditationOverseasSiteMaterialControllerTests
    {
        protected Mock<IAccreditationService> _mockAccreditationService;
        protected Mock<IWastePermitService> _mockWastePermitService;
        protected Mock<IAccreditationMaterialService> _mockAccreditationMaterialService;

        private AccreditationOverseasSiteMaterialController _controller;

        [TestInitialize]
        public void Init()
        {
            _mockAccreditationService = new Mock<IAccreditationService>();
            _mockWastePermitService = new Mock<IWastePermitService>();
            _mockAccreditationMaterialService = new Mock<IAccreditationMaterialService>();

        _controller = new AccreditationOverseasSiteMaterialController(
                _mockAccreditationService.Object,
                _mockWastePermitService.Object,
                _mockAccreditationMaterialService.Object);
        }

        [TestMethod]
        public async Task GetWasteDescriptionCodes_NoWasteCodes_ReturnsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            _mockAccreditationMaterialService.Setup(s =>
                s.GetWasteDescriptionCodes(
                    id,
                    siteId,
                    materialId))
                .ReturnsAsync((List<string>)null);

            // Act
            var result = await _controller.GetWasteDescriptionCodes(
                id,
                siteId,
                materialId) as NotFoundResult;

            // Assert
            _mockAccreditationMaterialService.Verify(s =>
                s.GetWasteDescriptionCodes(
                    id,
                    siteId,
                    materialId),
                Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetWasteDescriptionCodes_WasteCodesNotNull_ReturnsList()
        {
            // Arrange
            var wasteCodes = new List<string>
            {
                "One",
                "Two",
                "Three"
            };
            var id = Guid.NewGuid();
            var siteId = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            _mockAccreditationMaterialService.Setup(s =>
                s.GetWasteDescriptionCodes(
                    id,
                    siteId,
                    materialId))
                .ReturnsAsync(wasteCodes);

            // Act
            var result = await _controller.GetWasteDescriptionCodes(
                id,
                siteId,
                materialId) as OkObjectResult;

            // Assert
            _mockAccreditationMaterialService.Verify(s =>
                s.GetWasteDescriptionCodes(
                    id,
                    siteId,
                    materialId),
                Times.Once);
            Assert.IsNotNull(result);
            var list = result.Value as IEnumerable<string>;
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Any());
            Assert.IsTrue(list.ToList().Count == 3);
        }

        [TestMethod]
        public async Task UpdateWasteDescriptionCodes_CallsService()
        {
            // Arrange
            var wasteCodes = new List<string>();

            // Act
            var result = await _controller.UpdateWasteDescriptionCodes(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                wasteCodes) as OkResult;

            // Assert
            _mockAccreditationMaterialService
                .Verify(s =>
                    s.UpdateWasteDescriptionCodes(
                        It.IsAny<Guid>(),
                        It.IsAny<Guid>(),
                        It.IsAny<Guid>(),
                        wasteCodes),
                    Times.Once);
            Assert.IsNotNull(result);
        }
    }
}
