namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    using EPR.Accreditation.Facade.Common.Dtos;
    using EPR.Accreditation.Facade.Common.Enums;
    using DTO = EPR.Accreditation.Facade.Common.Dtos;

    public interface IHttpAccreditationService
    {
        Task<OperatorType> GetOperatorType(Guid id);

        Task<Guid> CreateAccreditation(DTO.Accreditation accreditation);

        Task<DTO.AccreditationMaterial> GetAccreditationMaterial(
            SiteType siteType,
            Guid id,
            Guid materialId);

        Task UpdateAccreditationMaterial(
            SiteType siteType,
            Guid id,
            Guid materialId,
            DTO.AccreditationMaterial accreditationMaterial);

        Task<DTO.Accreditation> GetAccreditation(
            Guid id);

        Task UpdateAccreditation(
            Guid id,
            DTO.Accreditation accreditation);

        Task<CheckYourAnswersDto> GetCheckYourAnswers(Guid id);

        Task<List<Dtos.AccreditationTaskProgress>> GetTaskProgress(
            Guid id);

        Task<AccreditationMaterial> GetLastCalendarYearWaste(Guid id, Guid accreditationMaterialId);

        Task<OverseasReprocessingSite> GetOverseasReprocessingSite(
            Guid id,
            Guid overseasSiteId);

        Task UpdateOverseasReprocessingSite(
            Guid id,
            OverseasReprocessingSite overseasSite);

        Task SetHasOverseasAgent(Guid id, bool? hasOverseasAgent);

        Task<string> GetRandomNumber(
            int length);
    }
}
