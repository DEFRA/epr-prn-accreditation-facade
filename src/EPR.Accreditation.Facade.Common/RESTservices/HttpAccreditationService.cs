using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;
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

        public async Task<CheckYourAnswersDto> GetCheckYourAnswers(Guid accreditationId)
        {
            var accreditationDto = await Get<Dtos.Accreditation>($"{accreditationId}");

            // TODO: update [NOT MAPPED] entries later when they have been implemented. 
            // TODO: update vm.Completed later when it has been implemented.
            var vm = new CheckYourAnswersDto();
            vm.Id = accreditationId;
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

        public async Task<OperatorType> GetOperatorType(Guid accreditationId)
        {
            var accreditation = await Get<Dtos.Accreditation>($"{accreditationId}");
            return accreditation.OperatorTypeId;
        }

        public async Task<Guid> CreateAccreditation(Dtos.Accreditation accreditation)
        {
            var Id = await Post<Guid>(accreditation);
            return Id;
        }

        public async Task<Dtos.AccreditationMaterial> GetAccreditationMaterial(
            SiteType siteType,
            Guid accreditationId,
            Guid materialId)
        {
            var site = GetSiteName(siteType);
            return await Get<Dtos.AccreditationMaterial>($"{accreditationId}/{site}/{materialId}");
        }

        public async Task UpdateAccreditationMaterial(
            SiteType siteType,
            Guid accreditationId,
            Guid materialId,
            AccreditationMaterial accreditationMaterial)
        {
            var site = GetSiteName(siteType);
            await Put($"{accreditationId}/{site}/{materialId}", accreditationMaterial);
        }
        public async Task<Dtos.Accreditation> GetAccreditation(
            Guid accreditationId)
        {
            return await Get<Dtos.Accreditation>($"{accreditationId}");
        }
        public async Task UpdateAccreditation(
            Guid accreditationId,
            Dtos.Accreditation accreditation)
        {
            await Put($"{accreditationId}", accreditation);
        }

        public async Task<List<AccreditationTaskProgress>> GetTaskProgress(
            Guid accreditationId)
        {
            return await Get<List<AccreditationTaskProgress>>($"{accreditationId}/TaskProgress");
        }

        public async Task SetHasOverseasAgent(Guid accreditationId, bool? hasOverseasAgent)
        {
            await Put($"{accreditationId}/HasOverseasAgent", hasOverseasAgent);
        }

        public async Task<AccreditationMaterial> GetLastCalendarYearWaste(
            Guid accreditationId, 
            Guid accreditationMaterialId)
        {
            return await Get<AccreditationMaterial>($"{accreditationId}/Site/Material/{accreditationMaterialId}");
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

        private string GetSiteName(
            SiteType siteType) => siteType == SiteType.Site ? "Material" : $"OverseasMaterial";

    }
}