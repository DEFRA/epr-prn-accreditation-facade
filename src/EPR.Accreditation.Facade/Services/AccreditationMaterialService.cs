namespace EPR.Accreditation.Facade.Services
{
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Facade.Common.Dtos.Portal;
    using EPR.Accreditation.Facade.Common.Enums;
    using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
    using EPR.Accreditation.Facade.Services.Interfaces;

    public class AccreditationMaterialService : IAccreditationMaterialService
    {
        private readonly IHttpAccreditationService _httpAccreditationService;
        private readonly IHttpSiteService _httpSiteService;

        public AccreditationMaterialService(
            IHttpAccreditationService httpAccreditationService,
            IHttpSiteService httpSiteService)
        {
            _httpAccreditationService = httpAccreditationService ?? throw new ArgumentNullException(nameof(httpAccreditationService));
            _httpSiteService = httpSiteService ?? throw new ArgumentNullException(nameof(httpSiteService));
        }

        public async Task<bool?> GetReprocessedWasteLastYear(
            Guid accreditationExternalId,
            Guid materialExternalId)
        {
            var accreditationMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                SiteType.Site,
                accreditationExternalId,
                null,
                materialExternalId);

            if (accreditationMaterial == null)
                return null;

            return accreditationMaterial.WasteLastYear;
        }

        public async Task<MaterialReprocessorDetails> GetReprocessedWasteLastYearData(
            Guid accreditationExternalId,
            Guid materialExternalId)
        {
            var accreditationMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                SiteType.Site,
                accreditationExternalId,
                null,
                materialExternalId);

            return accreditationMaterial.MaterialReprocessorDetails;
        }

        public async Task UpdateReprocessedWasteLastYear(
            Guid accreditationExternalId,
            Guid materialExternalId,
            ReprocessedWasteLastYear reprocessedWasteLastYear)
        {
            var accreditationMaterial = new Common.Dtos.AccreditationMaterial
            {
                WasteLastYear = reprocessedWasteLastYear.HasReprocessedWasteLastYear
            };

            await _httpAccreditationService.UpdateAccreditationMaterial(
                SiteType.Site,
                accreditationExternalId,
                null,
                materialExternalId,
                accreditationMaterial);
        }

        /// <summary>
        /// Gets the waste description codes for an application and the material
        /// This is only relevant for exporters so a NotFound should be returned
        /// if the accreditation is for a reprocessor
        /// </summary>
        /// <param name="id">The accreditation id</param>
        /// <param name="materialId">The material id</param>
        /// <returns>List of strings that represent the waste description codes</returns>
        public async Task<IEnumerable<string>> GetWasteDescriptionCodes(
            Guid id,
            Guid siteId,
            Guid materialId)
        {
            var accreditationTask = _httpAccreditationService.GetAccreditation(id);
            var accreditationMaterialTask = _httpAccreditationService.GetAccreditationMaterial(
                SiteType.OverseasSite,
                id,
                siteId,
                materialId);

            await Task.WhenAll(accreditationTask, accreditationMaterialTask);

            var accreditation = accreditationTask.Result;
            var accreditationMaterial = accreditationMaterialTask.Result;

            if (accreditation == null ||
                accreditationMaterial == null ||
                accreditation.OperatorTypeId == OperatorType.Reprocessor) // Waste Description codes are not valid for a reprocessor
            {
                return null;
            }

            if (accreditationMaterial.WasteCodes == null)
            {
                return new List<string>();
            }

            return accreditationMaterial
                .WasteCodes
                .Where(wc => wc.WasteCodeTypeId == WasteCodeType.WasteDescriptionCode)
                .Select(wc => wc.Code);
        }

        /// <summary>
        /// Performs any necessary processing on the waste description codes and
        /// updates them
        /// </summary>
        /// <param name="id">The id of the accreditation</param>
        /// <param name="siteId">The id of the site (This should be an overseas site)</param>
        /// <param name="materialId">The id of the material</param>
        /// <param name="wasteDescriptionCodes">List of waste description codes</param>
        /// <returns>Async task</returns>
        public async Task UpdateWasteDescriptionCodes(
            Guid id,
            Guid siteId,
            Guid materialId,
            IEnumerable<string> wasteDescriptionCodes)
        {
            var material = new AccreditationMaterial
            {
                WasteCodes = wasteDescriptionCodes
                    .Select(c => new WasteCode
                    {
                        Code = c,
                        WasteCodeTypeId = WasteCodeType.WasteDescriptionCode
                    })
            };

            await _httpAccreditationService.UpdateAccreditationMaterial(
                SiteType.OverseasSite,
                id,
                siteId,
                materialId,
                material);
        }

        public async Task<bool?> GetHasNpwdAccreditationNumber(
            Guid accreditationExternalId,
            Guid materialExternalId)
        {
            var accreditationMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                SiteType.Site,
                accreditationExternalId,
                null,
                materialExternalId);

            if (accreditationMaterial != null)
            {
                return accreditationMaterial.HasNpwdAccreditationNumber;
            }
            else
            {
                return null;
            }
        }

        public async Task UpdateHasNpwdAccreditationNumber(
            Guid accreditationExternalId,
            Guid materialExternalId,
            bool hasNpwdAccreditationNumber)
        {
            var accreditationMaterial = new Common.Dtos.AccreditationMaterial
            {
                HasNpwdAccreditationNumber = hasNpwdAccreditationNumber
            };

            await _httpAccreditationService.UpdateAccreditationMaterial(
                SiteType.Site,
                accreditationExternalId,
                null,
                materialExternalId,
                accreditationMaterial);
        }
    }
}
