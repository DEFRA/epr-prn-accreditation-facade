using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;

namespace EPR.Accreditation.Facade.Services
{
    public class AccreditationService : IAccreditationService
    {
        protected readonly IHttpAccreditationService _httpAccreditationService;

        public AccreditationService(IHttpAccreditationService httpAccreditationService)
        {
            _httpAccreditationService = httpAccreditationService ?? throw new ArgumentNullException(nameof(httpAccreditationService));
        }

        public async Task<Common.Enums.OperatorType> GetOperatorType(Guid accreditationExternalId)
        {
            var operatorTypeId = await _httpAccreditationService.GetOperatorType(accreditationExternalId);

            return operatorTypeId;
        }

        public async Task UpdateOperatorType(
            Guid accreditationExternalId,
            Common.Enums.OperatorType operatorTypeId)
        {

            await _httpAccreditationService.UpdateOperatorType(
                accreditationExternalId,
                operatorTypeId);
        }

        public async Task<string> GetWasteSource(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                accreditationExternalId,
                siteExternalId,
                materialExternalId);

            return siteMaterial.WasteSource;
        }

        public async Task UpdateWasteSource(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            string wasteSource)
        {
            var siteMaterial = new AccreditationMaterial
            {
                WasteSource = wasteSource
            };

            await _httpAccreditationService.UpdateAccreditationMaterial(
                accreditationExternalId,
                siteExternalId,
                materialExternalId,
                siteMaterial);
        }
    }
}
