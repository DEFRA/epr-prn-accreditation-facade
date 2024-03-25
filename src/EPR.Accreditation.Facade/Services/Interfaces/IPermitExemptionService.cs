namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface IPermitExemptionService
    {
        public Task<bool?> GetHasPermitExemption(Guid accreditationExternalId);
    }
}
