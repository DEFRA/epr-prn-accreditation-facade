using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Exceptions;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services;
using Moq;
using System.Net;

namespace EPR.Accreditation.UnitTests.Services
{
    [TestClass]
    public class SaveAndComeBackServiceTests
    {
        private SaveAndComeBackService _saveAndComeBackService;
        private Mock<IHttpSaveAndComeBackService> _mockHttpSaveAndComeBackService;

        [TestInitialize]
        public void Init()
        {
            _mockHttpSaveAndComeBackService = new Mock<IHttpSaveAndComeBackService>();
            _saveAndComeBackService = new SaveAndComeBackService(_mockHttpSaveAndComeBackService.Object);
        }

        [TestMethod]
        public async Task AddSaveAndComeBack_CallsDeleteAndAddSaveAndComeBack_ForValidData()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var saveAndComeBack = new SaveAndComeBack();

            // Act
            await _saveAndComeBackService.AddSaveAndComeBack(accreditationExternalId, saveAndComeBack);

            // Assert
            _mockHttpSaveAndComeBackService.Verify(s =>
                s.DeleteSaveAndComeBack(accreditationExternalId), Times.Once);

            _mockHttpSaveAndComeBackService.Verify(s =>
                s.AddSaveAndComeBack(
                    accreditationExternalId,
                    saveAndComeBack),
                    Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task AddSaveAndComeBack_ThrowsException_Delete()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var saveAndComeBack = new SaveAndComeBack();
            _mockHttpSaveAndComeBackService.Setup(s =>
                s.DeleteSaveAndComeBack(accreditationExternalId)).ThrowsAsync(new Exception("Test exception"));

            // Act
            await _saveAndComeBackService.AddSaveAndComeBack(accreditationExternalId, saveAndComeBack);

            // Assert
            _mockHttpSaveAndComeBackService.Verify(x => x.AddSaveAndComeBack(accreditationExternalId, saveAndComeBack), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task AddSaveAndComeBack_ThrowsException_Add()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var saveAndComeBack = new SaveAndComeBack();
            _mockHttpSaveAndComeBackService.Setup(x =>
                x.AddSaveAndComeBack(accreditationExternalId, saveAndComeBack)).ThrowsAsync(new Exception("Test exception"));

            // Act
            await _saveAndComeBackService.AddSaveAndComeBack(accreditationExternalId, saveAndComeBack);

            // Assert
            _mockHttpSaveAndComeBackService.Verify(s => s.DeleteSaveAndComeBack(accreditationExternalId), Times.Once);
        }

        [TestMethod]
        public async Task DeleteSaveAndComeBack_CallsDeleteSaveAndComeBack_ForAValidAccreditationId()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();

            // Act
            await _saveAndComeBackService.DeleteSaveAndComeBack(accreditationExternalId);

            // Assert
            _mockHttpSaveAndComeBackService.Verify(s => s.DeleteSaveAndComeBack(accreditationExternalId), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task DeleteSaveAndComeBack_ServiceThrowsException_ThrowsException()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            _mockHttpSaveAndComeBackService.Setup(x =>
                x.DeleteSaveAndComeBack(accreditationExternalId)).ThrowsAsync(new Exception("Test exception"));

            // Act
            await _saveAndComeBackService.DeleteSaveAndComeBack(accreditationExternalId);

            // Assert
            _mockHttpSaveAndComeBackService.Verify(s => s.DeleteSaveAndComeBack(accreditationExternalId), Times.Never);
        }

        [TestMethod]
        public async Task GetHasApplicationSaved_Saved_ReturnsTrue()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            _mockHttpSaveAndComeBackService.Setup(s => s.GetSaveAndComeBack(accreditationExternalId)).ReturnsAsync(new SaveAndComeBack());

            // Act
            var result = await _saveAndComeBackService.GetHasApplicationSaved(accreditationExternalId);

            // Assert
            Assert.IsTrue(result);
            _mockHttpSaveAndComeBackService.Verify(s => s.GetSaveAndComeBack(accreditationExternalId), Times.Once);
        }

        [TestMethod]
        public async Task GetHasApplicationSaved_NotFound_ReturnsFalse()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            _mockHttpSaveAndComeBackService.Setup(s =>
                s.GetSaveAndComeBack(accreditationExternalId)).ThrowsAsync(new ResponseCodeException(HttpStatusCode.NotFound));

            // Act
            var result = await _saveAndComeBackService.GetHasApplicationSaved(accreditationExternalId);

            // Assert
            Assert.IsFalse(result);
            _mockHttpSaveAndComeBackService.Verify(s => s.GetSaveAndComeBack(accreditationExternalId), Times.Once);
        }

        [TestMethod]
        public async Task GetHasApplicationSaved_InternalServerError_ThrowsException()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            _mockHttpSaveAndComeBackService.Setup(s =>
                s.GetSaveAndComeBack(accreditationExternalId)).ThrowsAsync(new ResponseCodeException(HttpStatusCode.InternalServerError));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ResponseCodeException>(async () =>
            {
                await _saveAndComeBackService.GetHasApplicationSaved(accreditationExternalId);
                _mockHttpSaveAndComeBackService.Verify(s => s.GetSaveAndComeBack(accreditationExternalId), Times.Never);
            });
        }

        [TestMethod]
        public async Task GetSaveAndComeBack_ValidAccreditationId_ReturnsSaveAndComeBack()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            var expectedSaveAndComeBack = new SaveAndComeBack();
            _mockHttpSaveAndComeBackService.Setup(s => s.GetSaveAndComeBack(accreditationExternalId)).ReturnsAsync(expectedSaveAndComeBack);

            // Act
            var result = await _saveAndComeBackService.GetSaveAndComeBack(accreditationExternalId);

            // Assert
            Assert.AreEqual(expectedSaveAndComeBack, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GetSaveAndComeBack_ServiceThrowsException_ThrowsException()
        {
            // Arrange
            Guid accreditationExternalId = Guid.NewGuid();
            _mockHttpSaveAndComeBackService.Setup(s => s.GetSaveAndComeBack(accreditationExternalId)).ThrowsAsync(new Exception("Test exception"));

            // Act
            await _saveAndComeBackService.GetSaveAndComeBack(accreditationExternalId);

            // Assert
            _mockHttpSaveAndComeBackService.Verify(s => s.GetSaveAndComeBack(accreditationExternalId), Times.Never);
        }
    }
}
