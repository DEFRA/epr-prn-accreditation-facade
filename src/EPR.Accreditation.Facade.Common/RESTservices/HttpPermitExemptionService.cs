using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EPR.Accreditation.Facade.Common.RESTservices
{
    public class HttpPermitExemptionService : BaseHttpService, IHttpPermitExemptionService
    {
        public HttpPermitExemptionService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName) : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task<PermitExemption> GetHasPermitExemption(Guid accreditationExternalId)
        {
            return await Get<PermitExemption>($"{accreditationExternalId}");
        }
    }
}
