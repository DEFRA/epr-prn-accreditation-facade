using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services;
using Moq;
using AutoMapper;

namespace EPR.Accreditation.UnitTests.Services
{
    [TestClass]
    public class AccreditationServiceTests
    {
        private AccreditationService accreditationService;
        private Mock<IHttpAccreditationService> _mockHttpAccreditationService;
        private IMapper _mapper;

        [TestInitialize]
        public void Init()
        {
            _mockHttpAccreditationService = new Mock<IHttpAccreditationService>();
            SetupAutomapper();
            accreditationService = new AccreditationService(
                _mapper, 
                _mockHttpAccreditationService.Object);
        }

        private void SetupAutomapper()
        {
            var myProfile = new EPR.Accreditation.Facade.Profiles.AccreditationProfile();
            var configuration = new MapperConfiguration(c => c.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
        }

        [TestMethod]
        public async Task GetOverseasReprocessingSiteOutputs_ReturnsOverseasReprocessingSiteOutputs_WithValidIds()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var siteExternalId = Guid.NewGuid();
            var expectedOutputs = "Test output";
            var overseasReprocessingSite = new OverseasReprocessingSite { Outputs = expectedOutputs };
             
            _mockHttpAccreditationService.Setup(s =>
                s.GetOverseasReprocessingSite(
                    accreditationExternalId,
                    siteExternalId))
                .ReturnsAsync(overseasReprocessingSite);

            // Act
            var result = await accreditationService.GetOverseasReprocessingSiteOutputs(
                accreditationExternalId,
                siteExternalId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OverseasReprocessingSiteOutputs));
            Assert.AreEqual(expectedOutputs, result.Outputs);

            _mockHttpAccreditationService.Verify(s =>
                s.GetOverseasReprocessingSite(
                    accreditationExternalId,
                    siteExternalId), Times.Once());
        }

        [TestMethod]
        public async Task UpdateOverseasReprocessingSiteOutputs_CallsUpdateOverseasReprocessingSite()
        {
            // Arrange
            var accreditationExternalId = Guid.NewGuid();
            var siteExternalId = Guid.NewGuid();
            var expectedOutputs = "Test output";
            var overseasReprocessingSite = new OverseasReprocessingSite { Outputs = expectedOutputs };
            var overseasReprocessingSiteOutputs = new OverseasReprocessingSiteOutputs { 
                Outputs = expectedOutputs };

            _mockHttpAccreditationService.Setup(s =>
                s.GetOverseasReprocessingSite(
                    accreditationExternalId,
                    siteExternalId))
                .ReturnsAsync(overseasReprocessingSite);

            _mockHttpAccreditationService.Setup(s =>
                s.UpdateOverseasReprocessingSite(
                    accreditationExternalId,
                    It.IsAny<OverseasReprocessingSite>()))
                .Returns(Task.CompletedTask);

            // Act
            await accreditationService.UpdateOverseasReprocessingSiteOutputs(
                accreditationExternalId,
                siteExternalId,
                overseasReprocessingSiteOutputs);

            // Assert
            _mockHttpAccreditationService.Verify(s =>
                s.UpdateOverseasReprocessingSite(
                    It.IsAny<Guid>(),
                    It.IsAny<OverseasReprocessingSite>()), Times.Once);
        }
    }
}
