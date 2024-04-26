namespace EPR.Accreditation.Facade.Services.Interfaces
{
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Facade.Common.Dtos.Portal;
    using EPR.Accreditation.Facade.Common.Enums;

    public interface IAccreditationService
    {
        Task<OperatorType> GetOperatorType(Guid id);
        Task<Guid> CreateAccreditation(Common.Dtos.Accreditation accreditation);

        Task<string> GetWasteSource(
            SiteType siteType,
            Guid id,
            Guid materialId);

        Task UpdateWasteSource(
            SiteType siteType,
            Guid id,
            Guid materialId,
            string wasteSource);

        Task<string> GetWasteMaterialName(
            SiteType siteType,
            Guid id,
            Guid materialId,
            Language language);

        Task CreateWastePermit(
            Guid accreditationExternalId,
            WastePermit workPermit);

        Task<WastePermit> GetWastePermit(
            Guid id);

        Task<ReprocessingSupportingInformationDto> GetReprocessorSupportingInformation(
            Guid id,
            Guid materialId,
            ReprocessorSupportingInformationType reprocessorSupportingInformationType);

        Task UpdateReprocessorSupportingInformation(
            Guid id,
            Guid materialId,
            ReprocessingSupportingInformationDto materialOutputsDto,
            ReprocessorSupportingInformationType reprocessorSupportingInformationType);

        Task<MaterialOutputsDto> GetMaterialOutputs(
            Guid id,
            Guid materialId);

        Task UpdateMaterialOutputs(
            Guid id,
            Guid materialId,
            MaterialOutputsDto materialOutputsDto);

        Task<MaterialWasteInputsDto> GetMaterialWasteInputs(
            Guid id,
            Guid materialId);

        Task UpdateMaterialWasteInputs(
            Guid id,
            Guid materialId,
            MaterialWasteInputsDto materialWasteInputsDto);

        Task<CheckYourAnswersDto> GetCheckYourAnswers(Guid id);

        Task<List<AccreditationTaskProgress>> GetTaskProgress(
            Guid id);

        Task<OverseasReprocessingSiteOutputs> GetOverseasReprocessingSiteOutputs(
            Guid id,
            Guid overseasSiteId);
        Task UpdateOverseasReprocessingSiteOutputs(
            Guid id,
            Guid overseasSiteId,
            OverseasReprocessingSiteOutputs overseasSiteOutputs);

        Task<HasOverseasAgentDto> GetHasOverseasAgent(Guid id);

        Task SetHasOverseasAgent(Guid id, bool? hasOverseasAgent);

        Task<PrnTonnesPlannedDto> GetPrnTonnesPlanned(
            Guid accreditationExternalId);

        Task UpdatePrnTonnesPlanned(
            Guid accreditationExternalId,
            PrnTonnesPlannedDto prnTonnesPlannedDto);
    }
}
