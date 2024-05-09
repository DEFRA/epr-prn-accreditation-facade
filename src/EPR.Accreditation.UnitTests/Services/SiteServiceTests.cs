using AutoMapper;
using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services;
using Moq;

namespace EPR.Accreditation.UnitTests.Services
{
    [TestClass]
    public class SiteServiceTests
    {
        private SiteService _siteService;
        private Mock<IHttpSiteService> _mockHttpSiteService;
        private Mock<IMapper> _mockMapper;

        [TestInitialize]
        public void Init()
        {
            _mockMapper = new Mock<IMapper>();
            _mockHttpSiteService = new Mock<IHttpSiteService>();
            _siteService = new SiteService(
                _mockMapper.Object,
                _mockHttpSiteService.Object);
        }

        [TestMethod]
        public async Task GetExemptionReferences_ReturnsExemptionReferences_ForValidAccreditationIdAndSite()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var site = new Site
            {
                ExemptionReferences = new List<string>
                {
                    "Reference1",
                    "Reference2"
                }
            };
            _mockHttpSiteService
                .Setup(s =>
                    s.GetSite(
                        accreditationExternalId,
                        null))
                .ReturnsAsync(site);

            // Act
            var result = await _siteService.GetExemptionReferences(accreditationExternalId);

            // Assert
            CollectionAssert.AreEqual((System.Collections.ICollection)site.ExemptionReferences, (System.Collections.ICollection)result);
            _mockHttpSiteService
                .Verify(s =>
                    s.GetSite(
                        accreditationExternalId,
                        null),
                Times.Once);
        }

        [TestMethod]
        public async Task GetExemptionReferences_ShouldThrowException_WhenSiteNotFound()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            _mockHttpSiteService.Setup(s => s.GetSite(id, null)).ReturnsAsync((Site)null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => _siteService.GetExemptionReferences(id));
        }

        [TestMethod]
        public async Task GetExemptionReferences_ReturnsNull_ForNullExemptionReferences()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var site = new Site { ExemptionReferences = null };
            _mockHttpSiteService
                .Setup(x =>
                    x.GetSite(
                        accreditationExternalId,
                        null))
                .ReturnsAsync(site);

            // Act
            var result = await _siteService.GetExemptionReferences(accreditationExternalId);

            // Assert
            Assert.IsNull(result);
            _mockHttpSiteService
                .Verify(s =>
                    s.GetSite(
                        accreditationExternalId,
                        null),
                Times.Once);
        }

        [TestMethod]
        public async Task UpdateExemptionReferences_CallsUpdateSiteWithCorrectArguments_ForValidData()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var references = new List<string> { "Reference1", "Reference2" };
            var site = new Site
            {
                ExemptionReferences = references,
            };

            _mockHttpSiteService.Setup(s => s.GetSite(id, null)).ReturnsAsync(site);

            // Act
            await _siteService.UpdateExemptionReferences(id, references);

            // Assert
            _mockHttpSiteService.Verify(s =>
                s.UpdateSite(id, It.Is<Site>(s => s.ExemptionReferences == references)), Times.Once);
        }

        [TestMethod]
        public async Task UpdateExemptionReferences_CallsUpdateSiteWithNullReferencesForNullReferences()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var site = new Site();

            _mockHttpSiteService.Setup(s => s.GetSite(id, null)).ReturnsAsync(site);

            // Act
            await _siteService.UpdateExemptionReferences(id, null);

            // Assert
            _mockHttpSiteService.Verify(s =>
                s.UpdateSite(id, It.Is<Site>(s => s.ExemptionReferences == null)), Times.Once);
        }

        [TestMethod]
        public async Task UpdateExemptionReferences_CallsUpdateSiteWithEmptyReferences_ForEmptyReferences()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var references = new List<string>();
            var site = new Site
            {
                ExemptionReferences = references
            };

            _mockHttpSiteService.Setup(s => s.GetSite(id, null)).ReturnsAsync(site);

            // Act
            await _siteService.UpdateExemptionReferences(id, references);

            // Assert
            _mockHttpSiteService.Verify(s =>
                s.UpdateSite(id, It.Is<Site>(s => s.ExemptionReferences != null && !s.ExemptionReferences.Any())), Times.Once);
        }
    }
}
