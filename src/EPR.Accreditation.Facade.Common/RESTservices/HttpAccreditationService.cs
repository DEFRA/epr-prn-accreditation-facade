namespace EPR.Accreditation.Facade.Common.RESTservices
{
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Facade.Common.Enums;
    using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
    using Microsoft.AspNetCore.Http;

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
            Guid materialId)
        {
            var site = GetSiteName(siteType);
            return await Get<Dtos.AccreditationMaterial>($"{id}/{site}/{materialId}");
        }

        public async Task UpdateAccreditationMaterial(
            SiteType siteType,
            Guid id,
            Guid materialId,
            AccreditationMaterial accreditationMaterial)
        {
            var site = GetSiteName(siteType);
            await Put($"{id}/{site}/{materialId}", accreditationMaterial);
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
            return await Get<AccreditationMaterial>($"{id}/Material/{accreditationMaterialId}");
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

        /// <summary>
        /// Gets a list of uploaded-file details for the given Accreditation, using an HTTP client.
        /// </summary>
        /// <param name="id">Accreditation Id.</param>
        /// <returns>A list of file records view models.</returns>
        public async Task<List<FileUpload>> GetFileRecords(Guid id)
        {
            return await Get<List<FileUpload>>($"{id}/Files");
        }

        /// <summary>
        /// Add an uploaded-file record for the given Accreditation, using an HTTP client.
        /// </summary>
        /// <param name="id">Accreditation Id.</param>
        /// <param name="fileRecord">Uploaded file record.</param>
        /// <returns>Completed Task.</returns>
        public async Task AddFile(Guid id, FileUpload fileRecord)
        {
            await Post($"{id}/Files", fileRecord);
        }

        /// <summary>
        /// Deletes an uploaded-file record, using an HTTP client.
        /// </summary>
        /// <param name="accreditationExternalId">Accreditation Id.</param>
        /// <param name="uploadedFileId">Uploaded file Id.</param>
        /// <returns>Completed Task.</returns>
        public async Task DeleteFile(Guid id, Guid uploadedFileId)
        {
            await Delete($"{id}/Files/{uploadedFileId}");
        }

        private string GetSiteName(
            SiteType siteType) => siteType == SiteType.Site ? "Material" : $"OverseasMaterial";
    }
}