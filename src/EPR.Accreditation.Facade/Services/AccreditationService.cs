namespace EPR.Accreditation.Facade.Services
{
    using AutoMapper;
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Facade.Common.Dtos.Portal;
    using EPR.Accreditation.Facade.Common.Enums;
    using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
    using EPR.Accreditation.Facade.Services.Interfaces;
    using System;
    using System.Net;

    public class AccreditationService : IAccreditationService
    {
        private static Random random = new Random();
        protected readonly IMapper _mapper;
        protected readonly IHttpAccreditationService _httpAccreditationService;

        public AccreditationService(
            IMapper mapper,
            IHttpAccreditationService httpAccreditationService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpAccreditationService = httpAccreditationService ?? throw new ArgumentNullException(nameof(httpAccreditationService));
        }

        public async Task<CheckYourAnswersDto> GetCheckYourAnswers(Guid id)
        {
            var checkYourAnswersDto = await _httpAccreditationService.GetCheckYourAnswers(id);

            return checkYourAnswersDto;
        }

        public async Task<OperatorType> GetOperatorType(Guid id)
        {
            var operatorTypeId = await _httpAccreditationService.GetOperatorType(id);

            return operatorTypeId;
        }

        public async Task<Guid> CreateAccreditation(Common.Dtos.Accreditation accreditation)
        {
            return await _httpAccreditationService.CreateAccreditation(accreditation);
        }

        public async Task<string> GetWasteSource(
            SiteType siteType,
            Guid id,
            Guid materialId)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                siteType,
                id,
                materialId);

            return siteMaterial.WasteSource;
        }

        public async Task UpdateWasteSource(
            SiteType siteType,
            Guid id,
            Guid materialId,
            string wasteSource)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                siteType,
                id,
                materialId);
            siteMaterial.WasteSource = wasteSource;

            await _httpAccreditationService.UpdateAccreditationMaterial(
                siteType,
                id,
                materialId,
                siteMaterial);
        }

        public async Task<string> GetWasteMaterialName(
            SiteType siteType,
            Guid id,
            Guid materialId,
            Language language)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                siteType,
                id,
                materialId);

            return language == Language.English ? siteMaterial.Material.English : siteMaterial.Material.Welsh;
        }

        public async Task<WastePermit> GetWastePermit(
            Guid id)
        {
            var accreditation = await _httpAccreditationService.GetAccreditation(id);

            return accreditation.WastePermit;
        }

        public async Task CreateWastePermit(
            Guid id,
            WastePermit wastePermit)
        {
            var accreditation = await _httpAccreditationService.GetAccreditation(id);
            accreditation.WastePermit = wastePermit;
            await _httpAccreditationService.UpdateAccreditation(id, accreditation);
        }

        private Common.Dtos.Accreditation CreateNewAccreditationDto(OperatorType operatorTypeId)
        {
            var dto = new Common.Dtos.Accreditation();
            dto.OperatorTypeId = operatorTypeId;

            return dto;
        }

        public async Task<ReprocessingSupportingInformationDto> GetReprocessorSupportingInformation(
            Guid id,
            Guid materialId,
            ReprocessorSupportingInformationType reprocessorSupportingInformationType)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                SiteType.Site,
                id,
                materialId);

            if (siteMaterial == null)
                return new ReprocessingSupportingInformationDto();

            return new ReprocessingSupportingInformationDto
            {
                WasteLastYear = siteMaterial.WasteLastYear,
                Records = siteMaterial.MaterialReprocessorDetails?
                    .ReprocessorSupportingInformation?
                    .Where(rsi => rsi.ReprocessorSupportingInformationTypeId == reprocessorSupportingInformationType)
                    .Select(rsi => new ReprocessingSupportingInformationRecordDto
                    {
                        Type = rsi.Type,
                        Tonnes = rsi.Tonnes
                    })
            };
        }

        public async Task UpdateReprocessorSupportingInformation(
            Guid id,
            Guid materialId,
            ReprocessingSupportingInformationDto nonWasteInputsDto,
            ReprocessorSupportingInformationType reprocessorSupportingInformationType)
        {
            if (nonWasteInputsDto != null &&
                nonWasteInputsDto.Records != null &&
                nonWasteInputsDto.Records.Any())
            {
                var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                    SiteType.Site,
                    id,
                    materialId);

                if (siteMaterial.MaterialReprocessorDetails == null)
                {
                    siteMaterial.MaterialReprocessorDetails = new MaterialReprocessorDetails();
                }

                if (siteMaterial.MaterialReprocessorDetails.ReprocessorSupportingInformation == null)
                {
                    siteMaterial.MaterialReprocessorDetails.ReprocessorSupportingInformation = new List<ReprocessorSupportingInformation>();
                }

                // remove the items that represent the update we are currently making
                siteMaterial.MaterialReprocessorDetails.ReprocessorSupportingInformation = 
                    siteMaterial.
                    MaterialReprocessorDetails.
                    ReprocessorSupportingInformation.
                    Where(
                        rsi =>
                            rsi.ReprocessorSupportingInformationTypeId != reprocessorSupportingInformationType);

                // map the incoming items to dto variant
                var dtoMapperList = _mapper.Map<IEnumerable<ReprocessorSupportingInformation>>(
                    nonWasteInputsDto.Records,
                    opt => opt.Items["ReprocessorSupportingInformationType"] = reprocessorSupportingInformationType);

                // union the two lists together
                siteMaterial.MaterialReprocessorDetails.ReprocessorSupportingInformation =
                    siteMaterial.MaterialReprocessorDetails.ReprocessorSupportingInformation.Union(dtoMapperList);

                await _httpAccreditationService.UpdateAccreditationMaterial(
                    SiteType.Site,
                    id,
                    materialId,
                    siteMaterial);
            }
        }

        public async Task<MaterialOutputsDto> GetMaterialOutputs(
            Guid id,
            Guid materialId)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                SiteType.Site,
                id,
                materialId);

            return siteMaterial == null ? new MaterialOutputsDto() : _mapper.Map<MaterialOutputsDto>(siteMaterial);
        }

        public async Task UpdateMaterialOutputs(
            Guid id,
            Guid materialId,
            MaterialOutputsDto materialOutputsDto)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(SiteType.Site, id, materialId);
            
            if (siteMaterial.MaterialReprocessorDetails == null)
            {
                siteMaterial.MaterialReprocessorDetails = new MaterialReprocessorDetails();
            }

            _mapper.Map(materialOutputsDto, siteMaterial.MaterialReprocessorDetails);

            await _httpAccreditationService.UpdateAccreditationMaterial(
                SiteType.Site,
                id,
                materialId,
                siteMaterial);
        }

        public async Task<MaterialWasteInputsDto> GetMaterialWasteInputs(
            Guid id,
            Guid materialId)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(
                SiteType.Site,
                id,
                materialId);

            if (siteMaterial == null)
            {
                return new MaterialWasteInputsDto();
            }

            var materialWasteInputsDto = _mapper.Map<MaterialWasteInputsDto>(siteMaterial);

            return materialWasteInputsDto;
        }

        public async Task UpdateMaterialWasteInputs(
            Guid id,
            Guid materialId,
            MaterialWasteInputsDto materialWasteInputsDto)
        {
            var siteMaterial = await _httpAccreditationService.GetAccreditationMaterial(SiteType.Site, id, materialId);

            if (siteMaterial.MaterialReprocessorDetails == null)
            {
                siteMaterial.MaterialReprocessorDetails = new MaterialReprocessorDetails();
            }

            _mapper.Map(materialWasteInputsDto, siteMaterial.MaterialReprocessorDetails);

            await _httpAccreditationService.UpdateAccreditationMaterial(
                SiteType.Site,
                id,
                materialId,
                siteMaterial);
        }

        public async Task<List<AccreditationTaskProgress>> GetTaskProgress(
                Guid accreditationExternalId)
        {
            var taskProgress = await _httpAccreditationService.GetTaskProgress(
                accreditationExternalId);

            return taskProgress;
        }

        public async Task<OverseasReprocessingSiteOutputs> GetOverseasReprocessingSiteOutputs(
            Guid id,
            Guid overseasSiteId)
        {
            var overseasSite = await _httpAccreditationService.GetOverseasReprocessingSite(id, overseasSiteId);
            var overseasSiteOutputs = _mapper.Map<OverseasReprocessingSiteOutputs>(overseasSite);
            return overseasSiteOutputs;
        }

        public async Task UpdateOverseasReprocessingSiteOutputs(
            Guid id,
            Guid overseasSiteId,
            OverseasReprocessingSiteOutputs overseasSiteOutputs)
        {
            var overseasSite = await _httpAccreditationService.GetOverseasReprocessingSite(
                id,
                overseasSiteId);

            overseasSite.Outputs = overseasSiteOutputs.Outputs;
            await _httpAccreditationService.UpdateOverseasReprocessingSite(id, overseasSite);
        }

        public async Task<HasOverseasAgentDto> GetHasOverseasAgent(Guid id)
        {
            var accreditation = await _httpAccreditationService.GetAccreditation(id);

            return accreditation == null
                ? new HasOverseasAgentDto()
                : new HasOverseasAgentDto
                {
                    HasOverseasAgent = accreditation.HasOverseasAgent
                };
        }

        public async Task SetHasOverseasAgent(
            Guid id,
            bool? hasOverseasAgent)
        {
            var accreditation = await _httpAccreditationService.GetAccreditation(id);
            accreditation.HasOverseasAgent = hasOverseasAgent;

            await _httpAccreditationService.UpdateAccreditation(id, accreditation);
        }

        /// <summary>
        /// Gets PRN tonnage data for the given accreditation.
        /// </summary>
        /// <param name="accreditationExternalId">Accreditation Id.</param>
        /// <returns>DTO containing PRN tonnage data.</returns>
        public async Task<PrnTonnesPlannedDto> GetPrnTonnesPlanned(Guid accreditationExternalId)
        {
            var accreditation = await _httpAccreditationService.GetAccreditation(accreditationExternalId);
            var dto = _mapper.Map<PrnTonnesPlannedDto>(accreditation);
            dto.PrnPlannedTonnesType = accreditation.Large == null ? null : accreditation.Large.Value ? PrnPlannedTonnesType.Over : PrnPlannedTonnesType.Upto;
            return dto;
        }

        /// <summary>
        /// Updates PRN tonnage data for the given accreditation.
        /// </summary>
        /// <param name="accreditationExternalId">Accreditation Id.</param>
        /// <param name="prnTonnesPlannedDto">DTO containing PRN tonnage data.</param>
        /// <returns>Completed Task.</returns>
        public async Task UpdatePrnTonnesPlanned(
            Guid accreditationExternalId,
            PrnTonnesPlannedDto prnTonnesPlannedDto)
        {
            var accreditation = await _httpAccreditationService.GetAccreditation(accreditationExternalId);
            accreditation.LargeFee = prnTonnesPlannedDto.PrnPlannedTonnesFee;

            if (prnTonnesPlannedDto.PrnPlannedTonnesType.HasValue)
            {
                accreditation.Large = prnTonnesPlannedDto.PrnPlannedTonnesType != PrnPlannedTonnesType.Upto;
            }

            await _httpAccreditationService.UpdateAccreditation(accreditationExternalId, accreditation);
        }

        public async Task<AddressDto> GetLegalDocumentsAddress(Guid id)
        {
            var accreditation = await _httpAccreditationService.GetAccreditation(id);

            return _mapper.Map<AddressDto>(accreditation.LegalAddress);
        }

        public async Task UpdateLegalDocumentsAddress(
            Guid id,
            AddressDto address)
        {
            var accreditation = await _httpAccreditationService.GetAccreditation(id);
            accreditation.LegalAddress = _mapper.Map<Address>(address);

            await _httpAccreditationService.UpdateAccreditation(
                id,
                accreditation);
        }

        /// <summary>
        /// Update the referernce number.
        /// </summary>
        /// <param name="id">The accrediation id.</param>
        /// <returns></returns>
        public async Task UpdateReferenceNumber(
            Guid id)
        {
            var randomNumber = await _httpAccreditationService.GetRandomNumber(12);
            var accreditation = new Common.Dtos.Accreditation
            {
                ReferenceNumber = randomNumber
            };
            
            await _httpAccreditationService.UpdateAccreditation(id, accreditation);
        }

        /// <summary>
        /// Returns the accrediation object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task <Accreditation> GetAccrediation(Guid id)
        {
            return await _httpAccreditationService.GetAccreditation(id);
        }
    }
}
