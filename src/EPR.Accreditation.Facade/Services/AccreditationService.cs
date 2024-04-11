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

        public async Task<CheckYourAnswersDto> GetCheckYourAnswers(Guid accreditationExternalId)
        {
            var checkYourAnswersDto = await _httpAccreditationService.GetCheckYourAnswers(accreditationExternalId);

            return checkYourAnswersDto;
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
            SiteType siteType,
            Guid accreditationExternalId,
            Guid? siteExternalId,
            Guid materialExternalId)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                siteType,
                accreditationExternalId,
                siteExternalId,
                materialExternalId);

            return siteMaterial.WasteSource;
        }

        public async Task UpdateWasteSource(
            SiteType siteType,
            Guid accreditationExternalId,
            Guid? siteExternalId,
            Guid materialExternalId,
            string wasteSource)
        {
            var siteMaterial = new AccreditationMaterial
            {
                WasteSource = wasteSource
            };

            await _httpAccreditationService.UpdateAccreditationMaterial(
                siteType,
                accreditationExternalId,
                siteExternalId,
                materialExternalId,
                siteMaterial);
        }

        public async Task<string> GetWasteMaterialName(
            SiteType siteType,
            Guid accreditationExternalId, 
            Guid? siteExternalId, 
            Guid materialExternalId, 
            Language language)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                siteType,
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

        public async Task<NonWasteInputsDto> GetNonWasteInputs(Guid accreditationExternalId, Guid materialExternalId)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                SiteType.Site,
                accreditationExternalId,
                null,
                materialExternalId);

            if (siteMaterial == null)
                return new NonWasteInputsDto();

            return new NonWasteInputsDto
            {
                //WasteLastYear = siteMaterial.WasteLastYear,
                NonWasteInputRecords = siteMaterial.MaterialReprocessorDetails?
                    .ReprocessorSupportingInformation?
                    .Where(rsi => rsi.ReprocessorSupportingInformationTypeId == ReprocessorSupportingInformationType.NonWasteInputs)
                    .Select(rsi => new NonWasteInputRecordDto
                    {
                        Type = rsi.Type,
                        Tonnes = rsi.Tonnes
                    })
            };
        }

        public async Task UpdateNonWasteInputs(
            Guid accreditationExternalId, 
            Guid materialExternalId, 
            NonWasteInputsDto nonWasteInputsDto)
        {
            if (nonWasteInputsDto != null &&
                nonWasteInputsDto.NonWasteInputRecords != null &&
                nonWasteInputsDto.NonWasteInputRecords.Any())
            {
                var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    null,
                    materialExternalId);

                if (siteMaterial == null)
                    throw new Exception(); // should end up with a not found result as we should have a SiteMaterial and MaterialReprocessorDetails by now

                siteMaterial.MaterialReprocessorDetails.ReprocessorSupportingInformation = _mapper.Map(
                    nonWasteInputsDto.NonWasteInputRecords,
                    siteMaterial.MaterialReprocessorDetails.ReprocessorSupportingInformation);//, items => items["type"] = ReprocessorSupportingInformationType.NonWasteInputs);

                await _httpAccreditationService.UpdateAccreditationMaterial(
                    SiteType.Site,
                    accreditationExternalId,
                    null,
                    materialExternalId,
                    siteMaterial);
            }
        }

        public async Task<MaterialOutputsDto> GetMaterialOutputs(
            Guid accreditationExternalId, 
            Guid materialExternalId)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                SiteType.Site,
                accreditationExternalId,
                null,
                materialExternalId);

            if (siteMaterial == null) 
                return new MaterialOutputsDto();

            return new MaterialOutputsDto
            {
                //WasteLastYear = siteMaterial.WasteLastYear,
                TonnesContaminents = siteMaterial.MaterialReprocessorDetails?.Contaminents,
                TonnesNotProcessedOnSite = siteMaterial.MaterialReprocessorDetails?.MaterialsNotProcessedOnSite,
                TonnesProcessLoss = siteMaterial.MaterialReprocessorDetails?.ProcessLoss
            };
        }

        public async Task UpdateMaterialOutputs(
            Guid accreditationExternalId,
            Guid materialExternalId,
            MaterialOutputsDto materialOutputsDto)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                SiteType.Site,
                accreditationExternalId,
                null,
                materialExternalId);

            if (siteMaterial == null)
                throw new Exception(); // should end up with a not found result as we should have a SiteMaterial and MaterialReprocessorDetails by now

            siteMaterial.MaterialReprocessorDetails = _mapper.Map(
                materialOutputsDto, 
                siteMaterial.MaterialReprocessorDetails);

            await _httpAccreditationService.UpdateAccreditationMaterial(
                SiteType.Site,
                accreditationExternalId,
                null,
                materialExternalId,
                siteMaterial);
        }

        public async Task<List<AccreditationTaskProgress>> GetTaskProgress(
                Guid accreditationExternalId)
        {
            var taskProgress = await _httpAccreditationService.GetTaskProgress(
                accreditationExternalId);

            return taskProgress;
        }
    }
}
