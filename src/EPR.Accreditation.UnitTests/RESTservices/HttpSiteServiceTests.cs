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
    public class HttpSiteServiceTests
    {
        private HttpSiteService _httpSiteService;
        protected Mock<IHttpContextAccessor> _contextAccessor;
        protected Mock<IHttpClientFactory> _httpClientFactory;
        protected HttpClient _httpClient;
        protected Mock<DelegatingHandler> _clientHandlerMock;
        protected string _baseUrl = "http://baseUrl";
        protected string _endpointName = "endpointName";
        protected string _capturedUrl;
        protected string _capturedPayload;

        public HttpSiteServiceTests()
        {
            _clientHandlerMock = new Mock<DelegatingHandler>();

            _clientHandlerMock.As<IDisposable>().Setup(s => s.Dispose());
            _httpClient = new HttpClient(_clientHandlerMock.Object);

            _contextAccessor = new Mock<IHttpContextAccessor>();
            _httpClientFactory = new Mock<IHttpClientFactory>(MockBehavior.Strict);

            _httpClientFactory.Setup(f => f.CreateClient(string.Empty)).Returns(_httpClient).Verifiable();
            _httpSiteService = new HttpSiteService(
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
        public async Task GetSite_CallsEndPointSuccesfully_WithExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedOutput = new Site();
            SetClientResponse(HttpStatusCode.OK, expectedOutput);

            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/Site";

            // Act
            var result = await _httpSiteService.GetSite(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
        }

        [TestMethod]
        public async Task UpdateSite_CallsEndPointSuccesfully_WithExpectedOutput()
        {
            // Arrange
            var id = Guid.NewGuid();
            var siteDto = new Site
            {
                ExternalId = Guid.NewGuid(),
                Address = new Address 
                {
                    Address1 = "123 Main St",
                    Address2 = "Apt 2",
                    Town = "Springfield",
                    County = "Shelby",
                    Postcode = "12345"
                }
            };

            var expectedUrl = $"{_baseUrl}/{_endpointName}/{id}/Site";

            // Act
            await _httpSiteService.UpdateSite(id, siteDto);

            // Assert
            var capturedPayload = JsonConvert.DeserializeObject<Site>(_capturedPayload);
            Assert.AreEqual(expectedUrl.ToLower(), _capturedUrl.ToLower());
            Assert.IsTrue(AreObjectsEqual(siteDto, capturedPayload));
        }

        private bool AreObjectsEqual<T>(T obj1, T obj2)
        {
            var obj1Json = JsonConvert.SerializeObject(obj1);
            var obj2Json = JsonConvert.SerializeObject(obj2);

            return obj1Json == obj2Json;
        }
    }
}
