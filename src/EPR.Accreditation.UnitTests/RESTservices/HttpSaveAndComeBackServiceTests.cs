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
    public class HttpSaveAndComeBackServiceTests
    {
        private HttpSaveAndComeBackService _httpSaveAndComeBackService;
        protected Mock<IHttpContextAccessor> _contextAccessor;
        protected Mock<IHttpClientFactory> _httpClientFactory;
        protected HttpClient _httpClient;
        protected Mock<DelegatingHandler> _clientHandlerMock;
        protected string _baseUrl = "http://baseUrl";
        protected string _endpointName = "endpointName";
        protected string _capturedUrl;
        protected string _capturedPayload;

        public HttpSaveAndComeBackServiceTests()
        {
            _clientHandlerMock = new Mock<DelegatingHandler>();

            _clientHandlerMock.As<IDisposable>().Setup(s => s.Dispose());
            _httpClient = new HttpClient(_clientHandlerMock.Object);

            _contextAccessor = new Mock<IHttpContextAccessor>();
            _httpClientFactory = new Mock<IHttpClientFactory>(MockBehavior.Strict);

            _httpClientFactory.Setup(f => f.CreateClient(string.Empty)).Returns(_httpClient).Verifiable();
            _httpSaveAndComeBackService = new HttpSaveAndComeBackService(
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
        public async Task GetSaveAndComeBack_CallsEndPointSuccesfully_WithExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedOutput = new SaveAndComeBack();
            SetClientResponse(HttpStatusCode.OK, expectedOutput);

            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}";

            // Act
            var result = await _httpSaveAndComeBackService.GetSaveAndComeBack(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        [TestMethod]
        public async Task AddSaveAndComeBack_CallsEndPointSuccesfully_WithExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid();
            var saveAndComeBackDto = new SaveAndComeBack
            {
                Area = "Area",
                Action = "Action",
                Controller = "Controller",
                Parameters = "Parameters"
            };

            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}";

            // Act
            await _httpSaveAndComeBackService.AddSaveAndComeBack(id, saveAndComeBackDto);

            // Assert
            var capturedPayload = JsonConvert.DeserializeObject<SaveAndComeBack>(_capturedPayload);
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
            Assert.IsTrue(AreObjectsEqual(saveAndComeBackDto, capturedPayload));
        }

        [TestMethod]
        public async Task DeleteSaveAndComeBack_CallsEndPointSuccesfully_WithExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedOutput = new SaveAndComeBack();
            SetClientResponse(HttpStatusCode.OK, expectedOutput);

            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}";

            // Act
            await _httpSaveAndComeBackService.DeleteSaveAndComeBack(id);

            // Assert
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        private bool AreObjectsEqual<T>(T obj1, T obj2)
        {
            var obj1Json = JsonConvert.SerializeObject(obj1);
            var obj2Json = JsonConvert.SerializeObject(obj2);

            return obj1Json == obj2Json;
        }
    }
}
