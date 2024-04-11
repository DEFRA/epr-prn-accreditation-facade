using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.RESTservices;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;

namespace EPR.Accreditation.UnitTests.RESTservices
{
    [TestClass]
    public class HttpAccreditationServiceTests
    {
        private HttpAccreditationService _httpAccreditationService;
        protected Mock<IHttpContextAccessor> _contextAccessor;
        protected Mock<IHttpClientFactory> _httpClientFactory;
        protected HttpClient _httpClient;
        protected Mock<DelegatingHandler> _clientHandlerMock;
        protected string _baseUrl = "http://baseUrl";
        protected string _endpointName = "endpointName";
        protected string _capturedUrl;
        protected string _capturedPayload;

        public HttpAccreditationServiceTests()
        {
            _clientHandlerMock = new Mock<DelegatingHandler>();

            _clientHandlerMock.As<IDisposable>().Setup(s => s.Dispose());
            _httpClient = new HttpClient(_clientHandlerMock.Object);

            _contextAccessor = new Mock<IHttpContextAccessor>();
            _httpClientFactory = new Mock<IHttpClientFactory>(MockBehavior.Strict);

            _httpClientFactory.Setup(f => f.CreateClient(string.Empty)).Returns(_httpClient).Verifiable();
            _httpAccreditationService = new HttpAccreditationService(
                _contextAccessor.Object,
                _httpClientFactory.Object,
                _baseUrl,
                _endpointName);

            SetClientResponse();
        }

        private void SetClientResponse(
            HttpStatusCode httpStatusCode = HttpStatusCode.OK,
            object content = null)
        {
            var response = new HttpResponseMessage(httpStatusCode);

            if (content != null)
            {
                response.Content = new StringContent(JsonConvert.SerializeObject(content));
            }

            _clientHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((request, cancellationToken) =>
                {
                    _capturedUrl = request.RequestUri.ToString().TrimEnd('/');
                    _capturedPayload = request.Content?.ReadAsStringAsync().Result; // Read the content as string
                })
                .ReturnsAsync(response)
                .Verifiable();
        }

        [TestMethod]
        public async Task GetAccreditation_CallsEndPointSuccesfully_WithExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedOutput = new Facade.Common.Dtos.Accreditation();
            SetClientResponse(HttpStatusCode.OK, expectedOutput);

            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}";

            // Act
            var result = await _httpAccreditationService.GetAccreditation(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        [TestMethod]
        public async Task UpdateAccreditation_CallsEndPointSuccesfully_WithExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid();
            var accreditationDto = new Facade.Common.Dtos.Accreditation
            {
                ExternalId = Guid.NewGuid(),
                OperatorTypeId = Facade.Common.Enums.OperatorType.Reprocessor,
                ReferenceNumber = "ABC123",
                OrganisationId = Guid.NewGuid(),
                Large = true,
                AccreditationStatusId = Facade.Common.Enums.AccreditationStatus.Updated,
                SiteId = 1,
                CreatedBy = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                UpdatedBy = null,
                UpdatedOn = null,
                Site = new Site(),
                OverseasReprocessingSites = new List<OverseasReprocessingSite>(),
                WastePermit = new WastePermit()
            };

            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}";

            // Act
            await _httpAccreditationService.UpdateAccreditation(id, accreditationDto);

            // Assert
            var capturedPayload = JsonConvert.DeserializeObject<Facade.Common.Dtos.Accreditation>(_capturedPayload);
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
            Assert.IsTrue(AreObjectsEqual(accreditationDto, capturedPayload));
        }

        private bool AreObjectsEqual<T>(T obj1, T obj2)
        {
            var obj1Json = JsonConvert.SerializeObject(obj1);
            var obj2Json = JsonConvert.SerializeObject(obj2);

            return obj1Json == obj2Json;
        }
    }
}
