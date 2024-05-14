namespace EPR.Accreditation.UnitTests.Services
{
    using EPR.Accreditation.Facade.Common.Dtos;
    using DTO = EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Facade.Common.Dtos.Portal;
    using EPR.Accreditation.Facade.Common.Enums;
    using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
    using EPR.Accreditation.Facade.Services;
    using Moq;
    using EPR.Accreditation.Facade.Common.RESTservices;

    [TestClass]
    public class AccreditationMaterialServiceTests
    {
        private AccreditationMaterialService _accreditationMaterialService;
        private Mock<IHttpAccreditationService> _mockHttpAccreditationService;
        private Mock<IHttpSiteService> _mockHttpSiteService;

        [TestInitialize]
        public void Init()
        {
            _mockHttpAccreditationService = new Mock<IHttpAccreditationService>();
            _mockHttpSiteService = new Mock<IHttpSiteService>();
            _accreditationMaterialService = new AccreditationMaterialService(
                _mockHttpAccreditationService.Object,
                _mockHttpSiteService.Object);
        }

        [TestMethod]
        public async Task GetReprocessedWasteLastYear_ReturnsWasteLastYear_WhenAccreditationMaterialExists()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var materialExternalId = Guid.NewGuid();
            var expectedWasteLastYear = true;
            var accreditationMaterial = new AccreditationMaterial { WasteLastYear = expectedWasteLastYear };

            _mockHttpAccreditationService.Setup(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    materialExternalId))
                .ReturnsAsync(accreditationMaterial);

            // Act
            var result = await _accreditationMaterialService.GetReprocessedWasteLastYear(
                accreditationExternalId,
                materialExternalId);

            // Assert
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expectedWasteLastYear, result.Value);

            _mockHttpAccreditationService.Verify(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    materialExternalId), Times.Once());
        }

        [TestMethod]
        public async Task GetReprocessedWasteLastYear_ReturnsNull_WhenAccreditationMaterialDoesNotExist()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var materialExternalId = Guid.NewGuid();

            _mockHttpAccreditationService.Setup(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    materialExternalId))
                .ReturnsAsync((AccreditationMaterial)null);

            // Act
            var result = await _accreditationMaterialService.GetReprocessedWasteLastYear(
                accreditationExternalId,
                materialExternalId);

            // Assert
            Assert.IsNull(result);

            _mockHttpAccreditationService.Verify(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    materialExternalId), Times.Once());
        }

        [TestMethod]
        public async Task UpdateReprocessedWasteLastYear_CallsUpdateAccreditationMaterialWithCorrectParameters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var reprocessedWasteLastYear = new ReprocessedWasteLastYear { HasReprocessedWasteLastYear = true };
            var expectedAccreditationMaterial = new AccreditationMaterial { WasteLastYear = true };

            _mockHttpAccreditationService
                .Setup(s =>
                    s.GetAccreditationMaterial(
                        SiteType.Site,
                        id,
                        materialId))
                .ReturnsAsync(new AccreditationMaterial());

            // Act
            await _accreditationMaterialService.UpdateReprocessedWasteLastYear(
                id,
                materialId,
                reprocessedWasteLastYear);

            // Assert
            _mockHttpAccreditationService.Verify(s =>
                s.UpdateAccreditationMaterial(
                    SiteType.Site,
                    id,
                    materialId,
                    It.IsAny<AccreditationMaterial>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateReprocessedWasteLastYear_WithFalseValue_CallsUpdateAccreditationMaterialWithCorrectParameters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var reprocessedWasteLastYear = new ReprocessedWasteLastYear { HasReprocessedWasteLastYear = false };
            var expectedAccreditationMaterial = new AccreditationMaterial { WasteLastYear = false };

            _mockHttpAccreditationService
                .Setup(s =>
                    s.GetAccreditationMaterial(
                        SiteType.Site,
                        id,
                        materialId))
                .ReturnsAsync(new AccreditationMaterial());
            // Act
            await _accreditationMaterialService.UpdateReprocessedWasteLastYear(
                id,
                materialId,
                reprocessedWasteLastYear);

            // Assert
            _mockHttpAccreditationService.Verify(s =>
                s.UpdateAccreditationMaterial(
                    SiteType.Site,
                    id,
                    materialId,
                    It.IsAny<AccreditationMaterial>()), Times.Once);
        }

        [TestMethod]
        public async Task GetWasteDescriptionCodes_AccreditationNull_ReturnsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            // Act
            var result = await _accreditationMaterialService.GetWasteDescriptionCodes(
                id,
                materialId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetWasteDescriptionCodes_AccreditationIsReproccesor_ReturnsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            _mockHttpAccreditationService
                .Setup(a =>
                    a.GetAccreditation(It.IsAny<Guid>()))
                .ReturnsAsync(new DTO.Accreditation
                {
                    OperatorTypeId = OperatorType.Reprocessor
                });

            // Act
            var result = await _accreditationMaterialService.GetWasteDescriptionCodes(
                id,
                materialId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetWasteDescriptionCodes_MaterialIsNull_ReturnsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            _mockHttpAccreditationService
                .Setup(a =>
                    a.GetAccreditation(It.IsAny<Guid>()))
                .ReturnsAsync(new DTO.Accreditation
                {
                    OperatorTypeId = OperatorType.Exporter
                });

            // Act
            var result = await _accreditationMaterialService.GetWasteDescriptionCodes(
                id,
                materialId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetWasteDescriptionCodes_MaterialWasteCodesNull_ReturnsEmptyList()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            _mockHttpAccreditationService
                .Setup(a =>
                    a.GetAccreditation(It.IsAny<Guid>()))
                .ReturnsAsync(new DTO.Accreditation
                {
                    OperatorTypeId = OperatorType.Exporter
                });

            _mockHttpAccreditationService
                .Setup(a =>
                    a.GetAccreditationMaterial(
                        SiteType.OverseasSite,
                        id,
                        materialId))
                .ReturnsAsync(new AccreditationMaterial());

            // Act
            var result = await _accreditationMaterialService.GetWasteDescriptionCodes(
                id,
                materialId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any() == false);
        }

        [TestMethod]
        public async Task GetWasteDescriptionCodes_MaterialWasteCodesNotNull_ReturnsWasteDescriptionCodeList()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();

            _mockHttpAccreditationService
                .Setup(a =>
                    a.GetAccreditation(It.IsAny<Guid>()))
                .ReturnsAsync(new DTO.Accreditation
                {
                    OperatorTypeId = OperatorType.Exporter
                });

            var code1 = "A";
            var code2 = "B";
            _mockHttpAccreditationService
                .Setup(a =>
                    a.GetAccreditationMaterial(
                        SiteType.OverseasSite,
                        id,
                        materialId))
                .ReturnsAsync(new AccreditationMaterial
                {
                    WasteCodes = new List<WasteCode>
                    {
                        new()
                        {
                            Code = code1,
                            WasteCodeTypeId = WasteCodeType.WasteDescriptionCode
                        },
                        new()
                        {
                            Code = "ZZ",
                            WasteCodeTypeId = WasteCodeType.MaterialCommodityCode
                        },
                        new()
                        {
                            Code = code2,
                            WasteCodeTypeId = WasteCodeType.WasteDescriptionCode
                        }
                    }
                });

            // Act
            var result = await _accreditationMaterialService.GetWasteDescriptionCodes(
                id,
                materialId);

            var listResult = result.ToList();
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(listResult.Count == 2);
            Assert.IsTrue(listResult[0] == code1);
            Assert.IsTrue(listResult[1] == code2);
        }

        [TestMethod]
        public async Task GetHasNpwdAccreditationNumber_ReturnsNull_WhenAccreditationMaterialIsNull()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var materialExternalId = Guid.NewGuid();

            _mockHttpAccreditationService.Setup(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    materialExternalId))
                .ReturnsAsync((AccreditationMaterial)null);

            // Act
            var result = await _accreditationMaterialService.GetHasNpwdAccreditationNumber(
                accreditationExternalId,
                materialExternalId);

            // Assert
            Assert.IsNull(result);

            _mockHttpAccreditationService.Verify(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    materialExternalId)
                , Times.Once);
        }

        [TestMethod]
        public async Task GetHasNpwdAccreditationNumber_ReturnsValueForTrue()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var materialExternalId = Guid.NewGuid();
            var hasNpwdAccreditationNumber = true;
            var accreditationMaterial = new AccreditationMaterial
            {
                HasNpwdAccreditationNumber = hasNpwdAccreditationNumber
            };

            _mockHttpAccreditationService.Setup(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    materialExternalId))
                .ReturnsAsync(accreditationMaterial);

            // Act
            var result = await _accreditationMaterialService.GetHasNpwdAccreditationNumber(
                accreditationExternalId,
                materialExternalId);

            // Assert
            Assert.AreEqual(hasNpwdAccreditationNumber, result);

            _mockHttpAccreditationService.Verify(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    materialExternalId)
                , Times.Once);
        }

        [TestMethod]
        public async Task GetHasNpwdAccreditationNumber_ReturnsValueForFalse()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var materialExternalId = Guid.NewGuid();
            var hasNpwdAccreditationNumber = false;
            var accreditationMaterial = new AccreditationMaterial
            {
                HasNpwdAccreditationNumber = hasNpwdAccreditationNumber
            };

            _mockHttpAccreditationService.Setup(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    materialExternalId))
                .ReturnsAsync(accreditationMaterial);

            // Act
            var result = await _accreditationMaterialService.GetHasNpwdAccreditationNumber(
                accreditationExternalId,
                materialExternalId);

            // Assert
            Assert.AreEqual(hasNpwdAccreditationNumber, result);

            _mockHttpAccreditationService.Verify(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    materialExternalId)
                , Times.Once);
        }

        [TestMethod]
        public async Task UpdateHasNpwdAccreditationNumber_CallsHttpServiceWithCorrectParameters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var hasNpwdAccreditationNumber = new HasNpwdAccreditationNumber
            {
                Has2024NPWDAccreditationNumber = true
            };

            _mockHttpAccreditationService
                .Setup(s =>
                    s.GetAccreditationMaterial(
                        SiteType.Site,
                        id,
                        materialId))
                .ReturnsAsync(new AccreditationMaterial());

            // Act
            await _accreditationMaterialService.UpdateHasNpwdAccreditationNumber(
                id,
                materialId,
                hasNpwdAccreditationNumber);

            // Assert
            _mockHttpAccreditationService.Verify(s =>
                s.UpdateAccreditationMaterial(
                    SiteType.Site,
                    id,
                    materialId,
                    It.IsAny<AccreditationMaterial>()),
                    Times.Once);
        }

        #region NpwdAccreditationNumber

        [TestMethod]
        public async Task GetNpwdAccreditationNumber_ReturnsNull_WhenAccreditationMaterialIsNull()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var materialExternalId = Guid.NewGuid();

            _mockHttpAccreditationService.Setup(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    materialExternalId))
                .ReturnsAsync((AccreditationMaterial)null);

            // Act
            var result = await _accreditationMaterialService.GetNpwdAccreditationNumber(
                accreditationExternalId,
                materialExternalId);

            // Assert
            Assert.IsNull(result);

            _mockHttpAccreditationService.Verify(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    materialExternalId)
                , Times.Once);
        }

        [TestMethod]
        public async Task GetNpwdAccreditationNumber_ReturnsNull_WhenWasteLastYearIsFalse()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var materialExternalId = Guid.NewGuid();
            var accreditationMaterial = new AccreditationMaterial
            {
                WasteLastYear = false
            };

            _mockHttpAccreditationService.Setup(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    materialExternalId))
                .ReturnsAsync(accreditationMaterial);

            // Act
            var result = await _accreditationMaterialService.GetNpwdAccreditationNumber(
                accreditationExternalId,
                materialExternalId);

            // Assert
            Assert.IsNull(result);

            _mockHttpAccreditationService.Verify(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    materialExternalId)
                , Times.Once);
        }

        [TestMethod]
        public async Task GetNpwdAccreditationNumber_ReturnsCorrectValue()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var materialExternalId = Guid.NewGuid();
            var expectedValue = "EX123456789";
            var accreditationMaterial = new AccreditationMaterial
            {
                WasteLastYear = true,
                NpwdAccreditationNumber = expectedValue
            };

            _mockHttpAccreditationService.Setup(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    materialExternalId))
                .ReturnsAsync(accreditationMaterial);

            // Act
            var result = await _accreditationMaterialService.GetNpwdAccreditationNumber(
                accreditationExternalId,
                materialExternalId);

            // Assert
            Assert.AreEqual(expectedValue, result);

            _mockHttpAccreditationService.Verify(s =>
                s.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    materialExternalId)
                , Times.Once);
        }

        [TestMethod]
        public async Task UpdateNpwdAccreditationNumber_CallsHttpServiceWithCorrectParameters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var materialId = Guid.NewGuid();
            var npwdAccreditationNumber = new NpwdAccreditationNumber
            {
                AccreditationNumber = "EX123456789"
            };

            _mockHttpAccreditationService
                .Setup(s =>
                    s.GetAccreditationMaterial(
                        SiteType.Site,
                        id,
                        materialId))
                .ReturnsAsync(new AccreditationMaterial());

            // Act
            await _accreditationMaterialService.UpdateNpwdAccreditationNumber(
                id,
                materialId,
                npwdAccreditationNumber);

            // Assert
            _mockHttpAccreditationService.Verify(s =>
                s.UpdateAccreditationMaterial(
                    SiteType.Site,
                    id,
                    materialId,
                    It.IsAny<AccreditationMaterial>()),
                    Times.Once);
        }

        #endregion
    }
}