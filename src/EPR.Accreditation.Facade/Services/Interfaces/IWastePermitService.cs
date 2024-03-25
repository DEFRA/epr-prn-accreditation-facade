namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface IWastePermitService
    {
        public Task<bool?> GetHasPermitExemption(Guid accreditationExternalId);

        public Task UpdatePermitExemption(Guid accreditationExternalId, bool hasPermitExemption);
    }
}
