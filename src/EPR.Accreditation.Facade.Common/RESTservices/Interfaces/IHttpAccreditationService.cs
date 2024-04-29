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

        /// <summary>
        /// Gets a list of uploaded-file details for the given Accreditation, using an HTTP client.
        /// </summary>
        /// <param name="id">Accreditation Id.</param>
        /// <returns>A list of file records.</returns>
        Task<List<FileUpload>> GetFileRecords(Guid id);

        /// <summary>
        /// Add an uploaded-file record for the given Accreditation, using an HTTP client.
        /// </summary>
        /// <param name="id">Accreditation Id.</param>
        /// <param name="fileRecord">Uploaded file record.</param>
        /// <returns>Completed Task.</returns>
        Task AddFile(Guid id, FileUpload fileRecord);

        /// <summary>
        /// Deletes an uploaded-file record, using an HTTP client.
        /// </summary>
        /// <param name="accreditationExternalId">Accreditation Id.</param>
        /// <param name="uploadedFileId">Uploaded file Id.</param>
        /// <returns>Completed Task.</returns>
        Task DeleteFile(Guid id, Guid uploadedFileId);
    }
}
