using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Controllers;
using EPR.Accreditation.Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPR.Accreditation.UnitTests.Controllers
{
    [TestClass]
    public class OverseasSiteControllerTests
    {
        private OverseasSiteController _overseasSiteController;
        private Mock<IAccreditationService> _mockAccreditationService;

        [TestInitialize]
        public void Init()
        {
            _mockAccreditationService = new Mock<IAccreditationService>();
            _overseasSiteController = new OverseasSiteController(_mockAccreditationService.Object);
        }

        [TestMethod]
        public async Task GetOverseasSiteOutputs_ReturnsOk_ForValidIds()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            Guid siteExternalId = Guid.NewGuid();

            var overseasReprocessingSiteOutputs = new OverseasReprocessingSiteOutputs();

            _mockAccreditationService.Setup(s => s.GetOverseasReprocessingSiteOutputs(
                accreditationExternalId, 
                siteExternalId))
                .ReturnsAsync(overseasReprocessingSiteOutputs);

            // Act
            var result = await _overseasSiteController.GetOverseasSiteOutputs(
                accreditationExternalId,
                siteExternalId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            _mockAccreditationService.Verify(s => s.GetOverseasReprocessingSiteOutputs(
                accreditationExternalId,
                siteExternalId), Times.Once);
        }
    }
}
