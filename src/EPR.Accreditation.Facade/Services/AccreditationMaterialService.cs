using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;

namespace EPR.Accreditation.Facade.Services
{
    public class AccreditationMaterialService : IAccreditationMaterialService
    {
        protected readonly IHttpAccreditationService _httpAccreditationService;

        public AccreditationMaterialService(IHttpAccreditationService httpAccreditationService)
        {
            _httpAccreditationService = httpAccreditationService ?? throw new ArgumentNullException(nameof(httpAccreditationService));
        }

        public async Task<bool?> GetReprocessedWasteLastYear(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId)
        {
            var accreditationMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                SiteType.Site,
                accreditationExternalId,
                siteExternalId,
                materialExternalId);

            if (accreditationMaterial.MaterialReprocessorDetails == null)
                return null;

            return accreditationMaterial.MaterialReprocessorDetails.WasteLastYear;
        }

        public async Task UpdateReprocessedWasteLastYear(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            ReprocessedWasteLastYear reprocessedWasteLastYear)
        {
            var accreditationMaterial = new Common.Dtos.AccreditationMaterial
            {
                MaterialReprocessorDetails = new MaterialReprocessorDetails
                {
                    WasteLastYear = reprocessedWasteLastYear.HasReprocessedWasteLastYear
                }
            };

            await _httpAccreditationService.UpdateAccreditationMaterial(
                SiteType.Site,
                accreditationExternalId,
                siteExternalId,
                materialExternalId,
                accreditationMaterial);
        }
    }
}
