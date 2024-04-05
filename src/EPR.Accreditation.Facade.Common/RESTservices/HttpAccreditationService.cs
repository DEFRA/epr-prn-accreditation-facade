using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EPR.Accreditation.Facade.Common.RESTservices
{
    public class HttpAccreditationService : BaseHttpService, IHttpAccreditationService
    {
        public HttpAccreditationService(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string endPointName) : base(httpContextAccessor, httpClientFactory, baseUrl, endPointName)
        {
        }

        public async Task<OperatorType> GetOperatorType(Guid accreditationExternalId)
        {
            var accreditation = await Get<Dtos.Accreditation>($"{accreditationExternalId}");
            return accreditation.OperatorTypeId;
        }

        public async Task<Guid> CreateAccreditation(Dtos.Accreditation accreditation)
        {
            var externalId = await Post<Guid>(accreditation);
            return externalId;
        }

        public async Task<Dtos.AccreditationMaterial> GetAccreditationMaterial(
            SiteType siteType,
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId)
        {
            var site = GetSiteName(siteType);
            return await Get<Dtos.AccreditationMaterial>($"{accreditationExternalId}/{site}/{siteExternalId}/Material/{materialExternalId}");
        }

        public async Task UpdateAccreditationMaterial(
            SiteType siteType,
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            AccreditationMaterial accreditationMaterial)
        {
            var site = GetSiteName(siteType);
            await Put($"{accreditationExternalId}/{site}/{siteExternalId}/Material/{materialExternalId}", accreditationMaterial);
        }
        public async Task<Dtos.Accreditation> GetAccreditation(
            Guid accreditationExternalId)
        {
            return await Get<Dtos.Accreditation>($"{accreditationExternalId}");
        }
        public async Task UpdateAccreditation(
            Guid accreditationExternalId,
            Dtos.Accreditation accreditation)
        {
            await Put($"{accreditationExternalId}", accreditation);
        }

        private string GetSiteName(SiteType siteType) => siteType == SiteType.Site ? "Site" : "OverseasSite";

        public async Task<List<AccreditationTaskProgress>> GetTaskProgress(
            Guid accreditationExternalId)
        {
            return await Get<List<AccreditationTaskProgress>>($"{accreditationExternalId}/TaskProgress");
        }
    }
}