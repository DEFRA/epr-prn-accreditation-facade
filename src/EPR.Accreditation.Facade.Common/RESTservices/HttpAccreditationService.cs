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

        public async Task<CheckYourAnswersDto> GetCheckYourAnswers(Guid id)
        {
            var accreditationDto = await Get<Dtos.Accreditation>($"{id}");

            // TODO: update [NOT MAPPED] entries later when they have been implemented. 
            // TODO: update vm.Completed later when it has been implemented.
            var vm = new CheckYourAnswersDto();
            vm.Id = id;
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

        public async Task<OperatorType> GetOperatorType(Guid id)
        {
            var accreditation = await Get<Dtos.Accreditation>($"{id}");
            return accreditation.OperatorTypeId;
        }

        public async Task<Guid> CreateAccreditation(Dtos.Accreditation accreditation)
        {
            var id = await Post<Guid>(accreditation);
            return id;
        }

        public async Task<Dtos.AccreditationMaterial> GetAccreditationMaterial(
            SiteType siteType,
            Guid id,
            Guid? siteId,
            Guid materialId)
        {
            var site = GetSiteName(siteType, siteId);
            return await Get<Dtos.AccreditationMaterial>($"{id}/{site}/Material/{materialId}");
        }

        public async Task UpdateAccreditationMaterial(
            SiteType siteType,
            Guid id,
            Guid? siteId,
            Guid materialId,
            AccreditationMaterial accreditationMaterial)
        {
            var site = GetSiteName(siteType, siteId);
            await Put($"{id}/{site}/Material/{materialId}", accreditationMaterial);
        }
        public async Task<Dtos.Accreditation> GetAccreditation(
            Guid id)
        {
            return await Get<Dtos.Accreditation>($"{id}");
        }
        public async Task UpdateAccreditation(
            Guid id,
            Dtos.Accreditation accreditation)
        {
            await Put($"{id}", accreditation);
        }

        public async Task<List<AccreditationTaskProgress>> GetTaskProgress(
            Guid id)
        {
            return await Get<List<AccreditationTaskProgress>>($"{id}/TaskProgress");
        }

        public async Task SetHasOverseasAgent(Guid id, bool? hasOverseasAgent)
        {
            await Put($"{id}/HasOverseasAgent", hasOverseasAgent);
        }

        public async Task<AccreditationMaterial> GetLastCalendarYearWaste(
            Guid id,
            Guid accreditationMaterialId)
        {
            return await Get<AccreditationMaterial>($"{id}/Site/Material/{accreditationMaterialId}");
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

        public async Task<OverseasReprocessingSite> GetOverseasReprocessingSite(
            Guid id,
            Guid overseasSiteId)
        {
            return await Get<OverseasReprocessingSite>($"{id}/OverseasSite/{overseasSiteId}");
        }

        public async Task UpdateOverseasReprocessingSite(Guid id, OverseasReprocessingSite overseasSite)
        {
            await Put($"{id}/OverseasSite", overseasSite);
        }

        private string GetSiteName(
            SiteType siteType,
            Guid? siteId) => siteType == SiteType.Site ? "Site" : $"OverseasSite/{siteId}";

    }
}