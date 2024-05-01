namespace EPR.Accreditation.Facade.Services.Interfaces
{
    using EPR.Accreditation.Facade.Common.Dtos.Portal;

    public interface ISiteService
    {
        public Task<IEnumerable<string>> GetExemptionReferences(Guid accreditationExternalId);

        public Task UpdateExemptionReferences(
            Guid accreditationExternalId,
            IEnumerable<string> references);

        Task<AddressDto> GetSite(
            Guid id);

        public Task<Guid> CreateSite(
            Guid accreditationExternalId,
            AddressDto site);

        public Task UpdateSite(
            Guid accreditationExternalId,
            AddressDto site);
    }
}