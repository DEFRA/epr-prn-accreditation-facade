using AutoMapper;
using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.Enums;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;

namespace EPR.Accreditation.Facade.Services
{
    public class AccreditationService : IAccreditationService
    {
        protected readonly IMapper _mapper;
        protected readonly IHttpAccreditationService _httpAccreditationService;

        public AccreditationService(
            IMapper mapper,
            IHttpAccreditationService httpAccreditationService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

        public async Task<MaterialOutputsDto> GetMaterialOutputs(
            Guid accreditationExternalId, 
            Guid siteExternalId, 
            Guid materialExternalId)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                accreditationExternalId,
                siteExternalId,
                materialExternalId);

            if (siteMaterial == null) 
                return new MaterialOutputsDto();

            return new MaterialOutputsDto
            {
                TonnesContaminents = siteMaterial.MaterialReprocessorDetails?.Contaminents,
                TonnesNotProcessedOnSite = siteMaterial.MaterialReprocessorDetails?.MaterialsNotProcessedOnSite,
                TonnesProcessLoss = siteMaterial.MaterialReprocessorDetails?.ProcessLoss
            };
        }

        public async Task UpdateMaterialOutputs(
            Guid accreditationExternalId, 
            Guid siteExternalId, 
            Guid materialExternalId,
            MaterialOutputsDto materialOutputsDto)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                accreditationExternalId,
                siteExternalId,
                materialExternalId);

            if (siteMaterial == null ||
                siteMaterial.MaterialReprocessorDetails == null)
                throw new Exception(); // should end up with a not found result as we should have a SiteMaterial and MaterialReprocessorDetails by now

            siteMaterial.MaterialReprocessorDetails = _mapper.Map(materialOutputsDto, siteMaterial.MaterialReprocessorDetails);

            await _httpAccreditationService.UpdateAccreditationMaterial(
                accreditationExternalId,
                siteExternalId,
                materialExternalId,
                siteMaterial);
        }
    }
}
