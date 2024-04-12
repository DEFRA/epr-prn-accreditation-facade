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

        [TestInitialize]
        public void Init()
        {
            _mockHttpSiteService = new Mock<IHttpSiteService>();
            _siteService = new SiteService(_mockHttpSiteService.Object);
        }

        [TestMethod]
        public async Task GetExemptionReferences_ReturnsExemptionReferences_ForValidAccreditationId()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var site = new Site { ExemptionReferences = new List<string> { "Reference1", "Reference2" } };
            _mockHttpSiteService.Setup(s => s.GetSite(accreditationExternalId)).ReturnsAsync(site);

            // Act
            var result = await _siteService.GetExemptionReferences(accreditationExternalId);

            // Assert
            CollectionAssert.AreEqual((System.Collections.ICollection)site.ExemptionReferences, (System.Collections.ICollection)result);
            _mockHttpSiteService.Verify(s => s.GetSite(accreditationExternalId), Times.Once);
        }

        [TestMethod]
        public async Task GetExemptionReferences_ReturnsNull_ForNullExemptionReferences()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var site = new Site { ExemptionReferences = null };
            _mockHttpSiteService.Setup(x => x.GetSite(accreditationExternalId)).ReturnsAsync(site);

            // Act
            var result = await _siteService.GetExemptionReferences(accreditationExternalId);

            // Assert
            Assert.IsNull(result);
            _mockHttpSiteService.Verify(s => s.GetSite(accreditationExternalId), Times.Once);
        }

        [TestMethod]
        public async Task UpdateExemptionReferences_CallsUpdateSiteWithCorrectArguments_ForValidData()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var references = new List<string> { "Reference1", "Reference2" };

            // Act
            await _siteService.UpdateExemptionReferences(accreditationExternalId, references);

            // Assert
            _mockHttpSiteService.Verify(s =>
                s.UpdateSite(accreditationExternalId, It.Is<Site>(s => s.ExemptionReferences == references)), Times.Once);
        }

        [TestMethod]
        public async Task UpdateExemptionReferences_CallsUpdateSiteWithNullReferencesForNullReferences()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();

            // Act
            await _siteService.UpdateExemptionReferences(accreditationExternalId, null);

            // Assert
            _mockHttpSiteService.Verify(s =>
                s.UpdateSite(accreditationExternalId, It.Is<Site>(s => s.ExemptionReferences == null)), Times.Once);
        }

        [TestMethod]
        public async Task UpdateExemptionReferences_CallsUpdateSiteWithEmptyReferences_ForEmptyReferences()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var references = new List<string>();

            // Act
            await _siteService.UpdateExemptionReferences(accreditationExternalId, references);

            // Assert
            _mockHttpSiteService.Verify(s =>
                s.UpdateSite(accreditationExternalId, It.Is<Site>(s => s.ExemptionReferences != null && !s.ExemptionReferences.Any())), Times.Once);
        }
    }
}
