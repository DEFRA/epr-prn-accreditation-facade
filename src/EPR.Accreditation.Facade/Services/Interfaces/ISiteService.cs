namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface ISiteService
    {
        public Task<IEnumerable<string>> GetExemptionReferences(Guid accreditationExternalId);

        public Task UpdateExemptionReferences(
            Guid accreditationExternalId,
            IEnumerable<string> references);
    }
}
