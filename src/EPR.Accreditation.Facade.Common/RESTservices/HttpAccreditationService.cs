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

        public async Task<CheckYourAnswersDto> GetCheckYourAnswers(Guid accreditationExternalId)
        {
            var accreditationDto = await Get<Dtos.Accreditation>($"{accreditationExternalId}");

            // TODO: update [NOT MAPPED] entries later when they have been implemented. 
            // TODO: update vm.Completed later when it has been implemented.
            var vm = new CheckYourAnswersDto();
            vm.Id = accreditationExternalId;
            vm.Completed = false;
            vm.SiteAddress = GetAdressAsSingleLine(accreditationDto.Site);
            vm.WasteCarrierRegistrationNumber = "NOT MAPPED";
            vm.WasteManagementLicenceNumber = "NOT MAPPED";
            vm.PartAReferenceNumber = accreditationDto.WastePermit?.PartAActivityReferenceNumber;
            vm.PartBReferenceNumber = accreditationDto.WastePermit?.PartBActivityReferenceNumber;
            vm.DischargeConsentNumber = accreditationDto.WastePermit?.DischargeConsentNumber;
            vm.ExemptionReferenceNumber = "NOT MAPPED";

            return vm;
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
            Guid? siteExternalId,
            Guid materialExternalId)
        {
            var site = GetSiteName(siteType, siteExternalId);
            return await Get<Dtos.AccreditationMaterial>($"{accreditationExternalId}/{site}/Material/{materialExternalId}");
        }

        public async Task UpdateAccreditationMaterial(
            SiteType siteType,
            Guid accreditationExternalId,
            Guid? siteExternalId,
            Guid materialExternalId,
            AccreditationMaterial accreditationMaterial)
        {
            var site = GetSiteName(siteType, siteExternalId);
            await Put($"{accreditationExternalId}/{site}/Material/{materialExternalId}", accreditationMaterial);
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
        public async Task<Dtos.OverseasReprocessingSite> GetOverseasSite(
            Guid accreditationExternalId,
            Guid siteExternalId)
        {
            return await Get<Dtos.OverseasReprocessingSite>($"{accreditationExternalId}/OverseasSite/{siteExternalId}");
        }

        private string GetSiteName(
            SiteType siteType, 
            Guid? siteExternalId) => siteType == SiteType.Site ? "Site" : $"OverseasSite/{siteExternalId}";
        private string GetSiteName(SiteType siteType) => siteType == SiteType.Site ? "Site" : "OverseasSite";

        public async Task<List<AccreditationTaskProgress>> GetTaskProgress(
            Guid accreditationExternalId)
        {
            return await Get<List<AccreditationTaskProgress>>($"{accreditationExternalId}/TaskProgress");
        }

        private string GetAdressAsSingleLine(Dtos.Site dto)
        {
            if (dto == null)
            {
                return string.Empty;
            }

            var address = $"{dto.Address1}, {dto.Address2}, {dto.Town}, {dto.County}, {dto.Postcode}";

            return address;
        }

        public async Task<bool?> GetHasOverseasAgent(Guid accreditationExternalId)
        {
            var accreditation = await Get<Dtos.Accreditation>($"{accreditationExternalId}");
            return accreditation.HasOverseasAgent;
        }
    }
}