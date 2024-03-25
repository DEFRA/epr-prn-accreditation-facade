using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EPR.Accreditation.Facade.Common.RESTservices
{
    public class HttpSaveAndContinueService : BaseHttpService, IHttpSaveAndContinueService
    {
        public HttpSaveAndContinueService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName) : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task AddSaveAndContinue(
            Guid accreditationExternalId,
            SaveAndComeBack saveAndContinue)
        {
            await Post($"{accreditationExternalId}", saveAndContinue);
        }

        public async Task DeleteSaveAndContinue(Guid accreditationExternalId)
        {
            await Delete($"{accreditationExternalId}");
        }

        public async Task<SaveAndComeBack> GetSaveAndContinue(Guid accreditationExternalId)
        {
            return await Get<SaveAndComeBack>($"{accreditationExternalId}");
        }
    }
}
