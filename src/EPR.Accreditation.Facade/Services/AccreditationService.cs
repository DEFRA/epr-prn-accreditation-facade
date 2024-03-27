using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Enums;
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

        public async Task<OperatorType> GetOperatorType(Guid accreditationExternalId)
        {
            var operatorTypeId = await _httpAccreditationService.GetOperatorType(accreditationExternalId);

            return operatorTypeId;
        }

        public async Task<Guid> CreateAccreditation(Common.Dtos.Accreditation accreditation)
        {
            return await _httpAccreditationService.CreateAccreditation(accreditation);
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

        public async Task<string> GetWasteMaterialName(
            Guid accreditationExternalId, 
            Guid siteExternalId, 
            Guid materialExternalId, 
            Language language)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                accreditationExternalId,
                siteExternalId,
                materialExternalId);

            return language == Language.English ? siteMaterial.Material.English : siteMaterial.Material.Welsh;
        }

        public async Task<WastePermit> GetWastePermit(
            Guid accreditationExternalId)
        {
            var accreditation = await _httpAccreditationService.GetAccreditation(accreditationExternalId);

            return accreditation.WastePermit;
        }

        public async Task CreateWastePermit(
            Guid accreditationExternalId,
            WastePermit wastePermit)
        {
            var accreditation = await _httpAccreditationService.GetAccreditation(accreditationExternalId);
            accreditation.WastePermit = wastePermit;
            await _httpAccreditationService.UpdateAccreditation(accreditationExternalId, accreditation);
        }

        private Common.Dtos.Accreditation CreateNewAccreditationDto(OperatorType operatorTypeId)
        {
            var dto = new Common.Dtos.Accreditation();
            dto.OperatorTypeId = operatorTypeId;

            return dto;
        }
    }
}
