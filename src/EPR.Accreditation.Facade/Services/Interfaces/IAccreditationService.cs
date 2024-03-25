namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface IAccreditationService
    {
        public Task<string> GetWasteSource(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId);

        public Task UpdateWasteSource(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            string wasteSource);

        Task CreateAccreditation(
            Guid accreditationExternalId,
            Common.Dtos.Accreditation accreditation);
    }
}
