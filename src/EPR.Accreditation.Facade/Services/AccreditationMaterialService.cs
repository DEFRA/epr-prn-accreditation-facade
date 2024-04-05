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
            Guid materialExternalId)
        {
            var accreditationMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                SiteType.Site,
                accreditationExternalId,
                null,
                materialExternalId);

            return accreditationMaterial.WasteLastYear;
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
    }
}
